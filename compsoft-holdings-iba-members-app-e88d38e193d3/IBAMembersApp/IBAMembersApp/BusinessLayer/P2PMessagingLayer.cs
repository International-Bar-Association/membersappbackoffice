using IBA_Common;
using IBA_Common.Models;
using IBAMembersApp.API.Models.Response;
using Compsoft.EmailLib;
using System;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using IBAMembersApp.Models;
using System.Web.Mvc;

namespace IBAMembersApp.BusinessLayer
{
    public class P2PMessagingLayer
    {
        public static GetP2PThreadResponseModel GetMessagesBetweenUsers(IbaCmsDbContext cmsDb, int user1, int user2, int? take, int skip = 0)
        {
            var thread = cmsDb.UserMessageThreads.SingleOrDefault(r => r.InitialSenderId == user1 && r.InitialRecipientId == user2);
            if (thread == null)
            {
                thread = cmsDb.UserMessageThreads.SingleOrDefault(r => r.InitialRecipientId == user1 && r.InitialSenderId == user2);
            }
            if (thread == null)
            {
                throw new HttpException(404, "No chat history found");
            }
            var response = new GetP2PThreadResponseModel();
            response.RecipientId = user2;
            response.ThreadId = thread.P2PMessageThreadId;
            response.Messages = new List<P2PMessageResponseModel>();
            var otherUserActivity = cmsDb.UserMessageThreads.Where(r => r.InitialSenderId == user2 || r.InitialRecipientId == user2).SelectMany(r => r.Messages).Where(r => r.SenderId == user2)
                .OrderByDescending(r => r.P2PMessageId).FirstOrDefault();
            if (otherUserActivity != null)
            {
                response.OtherParticipantLastSeenDateTime = DateTime.SpecifyKind((DateTime)otherUserActivity.SentTime, DateTimeKind.Utc);
            }
            int messagesToTake = 0;
            if (take != null)
            {
                messagesToTake = (int)take;
            }
            else
            {
                take = thread.Messages.Count;
            }
            foreach (var message in thread.Messages.OrderByDescending(r => r.SentTime).Skip(skip).Take(messagesToTake))
            {
                response.Messages.Add(new P2PMessageResponseModel()
                {
                    SentTime = DateTime.SpecifyKind((DateTime)message.SentTime, DateTimeKind.Utc),
                    SentByMe = message.SenderId == user1,
                    DeliveredTime = message.DeliveredTime != null ? DateTime.SpecifyKind((DateTime)message.DeliveredTime, DateTimeKind.Utc) : message.DeliveredTime,
                    Message = DecryptMessage(message.Message),
                    MessageId = message.P2PMessageId,
                    ReadTime = message.ReadTime != null ? DateTime.SpecifyKind((DateTime)message.ReadTime, DateTimeKind.Utc) : message.ReadTime,

                });
            }
            return response;
        }

        public static List<ConnectionResponseModel> GetconnectedUserIds(IbaCmsDbContext cmsDb, IBAEntities1 db, decimal myId)
        {
            List<ConnectionResponseModel> response = new List<ConnectionResponseModel>();


            var ids = cmsDb.UserMessageThreads.Where(r => r.InitialSenderId == (int)myId).Select(t => new Connections() { UserId = t.InitialRecipientId, P2PMessage = t.Messages.OrderByDescending(r => r.SentTime).FirstOrDefault() }).ToList();
            ids.AddRange(cmsDb.UserMessageThreads.Where(r => r.InitialRecipientId == (int)myId).Select(t => new Connections() { UserId = t.InitialSenderId, P2PMessage = t.Messages.OrderByDescending(r => r.SentTime).FirstOrDefault() }).ToList());
            var distinct = ids.Distinct().ToList();

            foreach (var message in distinct)
            {
                if ((message.P2PMessage.Thread.InitialRecipientId == myId && message.P2PMessage.Thread.InitialRecipientDeletionDate == null) ||
                    (message.P2PMessage.Thread.InitialSenderId == myId && message.P2PMessage.Thread.InitialSenderDeletionDate == null))
                {
                    var user = db.C_records.SingleOrDefault(r => r.id == message.UserId);
                    var connection = new ConnectionResponseModel()
                    {
                        UserId = message.UserId,
                        Name = user != null ? String.Format("{0} {1}", user.given_name, user.family_name) : null,
                        UserProfileImageUrl = user != null ? user.ProfileImageName : null,
                        LastMessage = message.P2PMessage == null ? null : new P2PMessageResponseModel()
                        {
                            DeliveredTime = message.P2PMessage.DeliveredTime,
                            SentByMe = message.P2PMessage.SenderId == myId,
                            SentTime = DateTime.SpecifyKind((DateTime)message.P2PMessage.SentTime, DateTimeKind.Utc),
                            Message = DecryptMessage(message.P2PMessage.Message),
                            MessageId = message.P2PMessage.P2PMessageId,
                            ReadTime = message.P2PMessage.ReadTime != null ? DateTime.SpecifyKind((DateTime)message.P2PMessage.ReadTime, DateTimeKind.Utc) : message.P2PMessage.ReadTime
                        }
                    };
                    response.Add(connection);
                }

            }
            return response;
        }

        public static bool SetMessageToRead(IbaCmsDbContext cmsDb, int myId, int messageId)
        {
            try
            {
                var message = cmsDb.UserMessages.SingleOrDefault(r => r.P2PMessageId == (long)messageId);


                if (message != null)
                {
                    if (message.Thread.isInvolvedInThread(myId) && message.SenderId != myId)
                    {
                        message.ReadTime = DateTime.UtcNow;
                        if (message.DeliveredTime == null)
                        {
                            message.ReadTime = message.ReadTime;
                        }
                        cmsDb.SaveChanges();
                        return true;
                    }

                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        public static P2pMessageSendResponseModel SendMessageToUser(IbaCmsDbContext cmsDb, IBAEntities1 Db, int user1, int user2, string messageToSend, ControllerContext context, string uuid = null)
        {
            var response = new P2pMessageSendResponseModel();
            response.Success = true;
            var thread = cmsDb.UserMessageThreads.SingleOrDefault(r => r.InitialSenderId == user1 && r.InitialRecipientId == user2);
            if (thread == null)
            {
                thread = cmsDb.UserMessageThreads.SingleOrDefault(r => r.InitialRecipientId == user1 && r.InitialSenderId == user2);

            }
            //NOTE: Users can only send messages to another user if they are both visiting the SAME conference. 
            //var conferenceOnNow = IBA_Common.Models.conf_conference.ConferenceOnNow(cmsDb);
            //if (conferenceOnNow == null)
            //{
            //    response.Success = false;
            //}


            //var delegateConf = Db.conf_delegate.SingleOrDefault(r => r.conference_id == 673 && r.id == user1); // Sender is at conference
            //if (delegateConf == null)
            //{
            //    response.Success = false;
            //}

            //var delegateConf2 = Db.conf_delegate.SingleOrDefault(r => r.conference_id == 673 && r.id == user2);// Receiver is at conference
            //if (delegateConf2 == null)
            //{
            //    response.Success = false;
            //}

            if (response.Success)
            {
                var sender = Db.C_records.SingleOrDefault(r => r.id == user1);

                if (thread == null)
                {
                    //NOTE: If no thread exists this is the first contact the user has made with the other person. Check recepient device usage if last use > X days send email.
                    //var recipientDevices = cmsDb.Devices.Where(r => r.DeviceOwner.IbaId == user2);
                    //var user = Db.conf_delegate.Where(t => t.conference_id == AppSettings.ConferenceId).FirstOrDefault(r => r.record_id == user2);
                    //if (user != null)
                    //{
                    //    if(AppSettings.ShouldSendEmails)
                    //    {
                    //        if (recipientDevices.Count() == 0)
                    //        {

                    //            EmailHelper.SendEmail(user.conf_email, "IBA Members App Notification", new P2PEmailViewModel() { SenderName = sender.given_name, ProfileImageUrl = sender.ProfileImageName, MessageContents = messageToSend }, context, "~/Views/Messages/P2PMessageEmail.cshtml");
                    //        }
                    //        else
                    //        {
                    //            var lastUsed = recipientDevices.OrderByDescending(r => r.UpdatedOn).First();
                    //            if ((lastUsed.UpdatedOn - DateTime.UtcNow).Days > 10)
                    //            {
                    //                EmailHelper.SendEmail(user.conf_email, "IBA Members App Notification", new P2PEmailViewModel() { SenderName = sender.given_name, ProfileImageUrl = sender.ProfileImageName, MessageContents = messageToSend }, context, "~/Views/Messages/P2PMessageEmail.cshtml");
                    //            }
                    //        }
                    //    }
 
                    //}

                    thread = new P2PMessageThread()
                    {
                        Messages = new List<P2PMessage>(),
                        InitialSenderId = user1,
                        InitialRecipientId = user2,
                    };
                    cmsDb.UserMessageThreads.Add(thread);
                }
                thread.InitialRecipientDeletionDate = null;
                thread.InitialSenderDeletionDate = null;
                var message = new P2PMessage()
                {
                    Message = EncryptMessage(messageToSend),
                    SenderName = string.Format("{0} {1}", sender.given_name, sender.family_name),
                    SenderId = (int)sender.id,
                    SentTime = DateTime.UtcNow
                };
                thread.Messages.Add(message);
                cmsDb.SaveChanges();
                response.Message = new P2PMessageResponseModel()
                {
                    SentTime = message.SentTime,
                    SentByMe = message.SenderId == user1,
                    DeliveredTime = message.DeliveredTime,
                    Message = messageToSend,
                    MessageId = message.P2PMessageId,
                    ReadTime = message.ReadTime,
                    UUID = uuid //NOTE: Used by the client to tie up sent messages.
                };

                ProcessOneMessage(cmsDb, message);
            }

            return response;
        }

        public static bool ProcessOneMessage(IbaCmsDbContext cmsDb, P2PMessage msg)
        {
            var senderId = msg.SenderId;
            var idToSendTo = msg.Thread.InitialRecipientId == senderId ? msg.Thread.InitialSenderId : msg.Thread.InitialRecipientId;
            var deviceOwners = cmsDb.DeviceOwners.Where(r => r.IbaId == idToSendTo);
            var count = 0;
            foreach (var owner in deviceOwners)
            {
                var appuserMessage = new AppP2PMessage
                {
                    Created = DateTime.UtcNow,
                    DeviceOwner = owner,
                    Message = msg
                };
                foreach (var device in owner.Devices.Where(d => !d.PushToken.IsNullOrWhiteSpace()))
                {
                    //var push = new PushMessageQueue
                    //{
                    //    Created = DateTime.UtcNow,
                    //    Device = device,
                    //    AppP2PMessage = appuserMessage
                    //};
                    //appuserMessage.Queued.Add(push);
                }
                count++;
                cmsDb.AppP2PMessages.Add(appuserMessage);
                SendPushMessage(appuserMessage);

            }
            msg.DeliveredTime = DateTime.UtcNow;
            return count > 0;
        }

        public static void SendPushMessage(AppP2PMessage item)
        {
            Push.Instance.Send(item.DeviceOwner.NamedUserId, MessagePush.MessageData(item), item.Id);
        }

        public static bool HideMessageThread(IbaCmsDbContext cmsDb, int threadId, int myId)
        {
            var thread = cmsDb.UserMessageThreads.SingleOrDefault(r => r.InitialSenderId == threadId && r.InitialRecipientId == myId);
            if (thread == null)
            {
                thread = cmsDb.UserMessageThreads.SingleOrDefault(r => r.InitialRecipientId == threadId && r.InitialSenderId == myId);
            }
            if (thread == null)
            {
                throw new HttpException(404, "No chat history found");
            }
            if (thread.InitialSenderId == myId)
            {
                thread.InitialSenderDeletionDate = DateTime.UtcNow;
            }
            else if (thread.InitialRecipientId == myId)
            {
                thread.InitialRecipientDeletionDate = DateTime.UtcNow;
            }
            else
            {
                throw new HttpException(401, "User cannot delete this thread.");
            }
            try
            {
                cmsDb.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static string EncryptMessage(string message)
        {
            AesCryptoServiceProvider aesCSP = new AesCryptoServiceProvider();
            byte[] encQuote = EncryptString(aesCSP, message);

            return Convert.ToBase64String(encQuote);
        }
        public static byte[] EncryptString(AesCryptoServiceProvider symAlg, string inString)
        {
            byte[] inBlock = UnicodeEncoding.Unicode.GetBytes(inString);
            ICryptoTransform xfrm = symAlg.CreateEncryptor(AppSettings.MessageKey, AppSettings.MessageIV);
            byte[] outBlock = xfrm.TransformFinalBlock(inBlock, 0, inBlock.Length);

            return outBlock;
        }

        public static string DecryptBytes(AesCryptoServiceProvider symAlg, byte[] inBytes)
        {
            ICryptoTransform xfrm = symAlg.CreateDecryptor(AppSettings.MessageKey, AppSettings.MessageIV);
            byte[] outBlock = xfrm.TransformFinalBlock(inBytes, 0, inBytes.Length);

            return UnicodeEncoding.Unicode.GetString(outBlock);
        }


        public static string DecryptMessage(string message)
        {
            byte[] data = Convert.FromBase64String(message);
            AesCryptoServiceProvider aesCSP = new AesCryptoServiceProvider();
            string messageOut = DecryptBytes(aesCSP, data);

            return messageOut;
        }
    }


    class Connections
    {
        public int UserId { get; set; }
        public P2PMessage P2PMessage { get; set; }
    }
}