using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CsvHelper;
using IBAMembersApp.BusinessLayer.Models;
using IBAMembersApp.Models;
using IBA_Common;
using IBA_Common.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace IBAMembersApp.BusinessLayer
{
    public class MessageLayer
    {
        private const int DefaultPageSize = 20;

        public static readonly string LogSource = "Iba Members Messaging Service";
        private static EventLog _serviceEventLog;

        public static EventLog ServiceEventLog
        {
            get
            {
                _serviceEventLog = _serviceEventLog ?? new EventLog() {Source = LogSource,Log = ""};
                return _serviceEventLog;
            }
        }

        public static StoreMessageModel ConvertModifyMessageModelToStoreMessage(ModifyMessageModel model)
        {
            //NOTE: model.sendDateTime will be in local time. We need it in the timezone the user has selected
            var correctDateTime = model.SendDateTime;
            
            if (correctDateTime.HasValue)
            {
                var timezone = System.TimeZoneInfo.FindSystemTimeZoneById(model.TimeZone);
                var newDate = System.TimeZoneInfo.ConvertTimeToUtc(model.SendDateTime.Value, timezone);


                correctDateTime = newDate;
            }

            return new StoreMessageModel
            {
                UrlOnly = model.UrlOnly,
                Text = model.Text,
                Url = model.Url,
                MessageType = model.MessageType,
                Title = model.Title,
                Recipients = "",
                SendDate = correctDateTime,
                TimeZone = model.TimeZone
            };
        }

        public static List<StoreMessageRecipients> RecipientFieldToList(string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return null;
            }
            try
            {
                return JsonConvert.DeserializeObject<List<StoreMessageRecipients>>(value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static SelectList FieldToRecipientList(string value)
        {
            var recipientList = RecipientFieldToList(value);

            if (recipientList == null)
            {
                return null;
            }
            var list = recipientList.Select(recip => new SelectListItem() { Text = recip.Name, Value = recip.Name }).ToList();
            return new SelectList(list, "Value", "Text");
        }

        public static MessageViewModel MsgToMessageViewModel(CmsMessage msg, bool viewRecipientsAsListBox)
        {
            var result = new MessageViewModel
            {
                Id = msg.Id,
                Created = msg.SendDate.HasValue ? msg.SendDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : msg.Created.ToString("yyyy-MM-dd HH:mm:ss"),
                MessageType = msg.MessageType,
                Title = msg.Title,
                Sender = msg.Sender,
                Text = msg.Text,
                Url = msg.Url,
                UrlOnly = msg.UrlOnly,
                Status = msg.Status
            };
            if (msg.Recipients.IsNullOrWhiteSpace())
            {
                result.Recipients = "Everyone";
            }
            else if (!viewRecipientsAsListBox)
            {
                var recipientList = RecipientFieldToList(msg.Recipients);
                result.Recipients = recipientList[0].Name;
                if (recipientList.Count > 1)
                {
                    result.Recipients = $"{result.Recipients} + {recipientList.Count - 1} Others";
                }
            }
            else
            {
                result.RecipientList = FieldToRecipientList(msg.Recipients);
            }
            return result;
        }

        public static List<MessageViewModel> ViewPendingMessages(IbaCmsDbContext cmsDb)
        {
            var msgs = cmsDb.CmsMessages.Where(m => m.Status == CmsMessageStatus.Draft).OrderByDescending(m => m.Created).ToList();
            return msgs.Select(msg => MsgToMessageViewModel(msg, false)).ToList();
        }

        public static List<MessageViewModel> ViewAllMessages(IbaCmsDbContext cmsDb)
        {
            var msgs = cmsDb.CmsMessages.OrderByDescending(m => m.Created).Where(m => m.Status != CmsMessageStatus.Archived).ToList();
            return msgs.Select(msg => MsgToMessageViewModel(msg, false)).ToList();
        }

        public static async Task SendMessage(IbaCmsDbContext db, long messageId, DateTime? time = null)
        {
            var msg = db.CmsMessages.FirstOrDefault(m => m.Id == messageId);
            if (msg != null)
            {
                msg.SendDate = time ?? DateTime.UtcNow;
                msg.Status = CmsMessageStatus.Outbox;
                db.SaveChanges();

                foreach (var appMessage in msg.AppUserMessages)
                {
                    if (appMessage.DeviceOwner.NamedUserId != null)
                    {
                        await SendPushMessage(appMessage, time);
                    }
                }
            }
        }

        public static void UpdateMessage(IbaCmsDbContext db, StoreMessageModel model, CmsMessage msg)
        {
            msg.Url = model.Url ?? "";
            msg.MessageType = model.MessageType;
            msg.Text = model.Text ?? "";
            msg.Title = model.Title;
            msg.Recipients = model.Recipients;
            msg.UrlOnly = model.UrlOnly;
            msg.SendDate = model.SendDate.HasValue ? model.SendDate.Value.ToUniversalTime() : (DateTime?)null;
            msg.Status = CmsMessageStatus.Draft;
            db.SaveChanges();
        }

        public static bool AddMessage(IbaCmsDbContext db, StoreMessageModel model)
        {
            try
            {
                var user = db.Users.FirstOrDefault(u => u.Id == model.UserId);
                var dbMessage = new CmsMessage
                {
                    Url = model.Url ?? "",
                    Created = DateTime.UtcNow,
                    MessageType = model.MessageType,
                    Text = model.Text ?? "",
                    Title = model.Title,
                    Sender = user,
                    Recipients = model.Recipients,
                    Status = CmsMessageStatus.Draft,
                    UrlOnly = model.UrlOnly,
                    TimeZone = model.TimeZone,
                    SendDate = model.SendDate.HasValue ? model.SendDate.Value.ToUniversalTime() : (DateTime?)null
                };
                db.CmsMessages.Add(dbMessage);

                db.SaveChanges();

                model.NewId = dbMessage.Id;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool ProcessOneMessage(IbaCmsDbContext cmsDb, CmsMessage msg)
        {
            List<DeviceOwner> deviceOwners;
            if (msg.Recipients.IsNullOrWhiteSpace())
            {
                deviceOwners = cmsDb.DeviceOwners.ToList();
            }
            else
            {
                var recipients = JsonConvert.DeserializeObject<List<StoreMessageRecipients>>(msg.Recipients);
                var recipientList = recipients.Select(recipient => recipient.Id).ToList();
                deviceOwners = cmsDb.DeviceOwners.Where(d => recipientList.Contains(d.IbaId)).ToList();
            }
            var count = 0;

            foreach (var owner in deviceOwners)
            {
                var appuserMessage = new AppUserMessage
                {
                    Created = DateTime.UtcNow,
                    DeviceOwner = owner,
                    Message = msg
                };

                count++;
                cmsDb.AppUserMessages.Add(appuserMessage);
            }
            msg.Status = CmsMessageStatus.Distributed;
            msg.Created = DateTime.UtcNow;
            msg.TotalRecipients = count;
            return count > 0;
        }

        public static bool LogMessageStatus(IbaCmsDbContext cmsDb, UpdateMessageStatusModel model, decimal ibaId)
        {
            var msg = cmsDb.AppUserMessages.FirstOrDefault(a => a.Id == model.AppUserMessageId && a.DeviceOwner.IbaId == ibaId); // make sure that msg updated by correct device
            if (msg == null)
            {
                return true; // should never happen
            }
            bool updated = false;
            if (model.Read != null && msg.Read == null)
            {
                updated = true;
                msg.Read = model.Read.Value;
            }
            if (model.Deleted != null && msg.Deleted == null)
            {
                updated = true;
                msg.Deleted = model.Deleted.Value;
            }
            if (model.Received != null && msg.Received == null)
            {
                updated = true;
                msg.Received = model.Received.Value;
            }
            if (!updated) return true;
            try
            {
                cmsDb.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static MessageResponseModel GetMessagesFromData(IbaCmsDbContext cmsDb, decimal ibaUserId, MessageRequestModel model)
        {
            var result = new MessageResponseModel();
            IQueryable<AppUserMessage> msgs;
            if (model.AppUserMessageId > 0)
            {
                msgs = cmsDb.AppUserMessages.Where(a => a.Id == model.AppUserMessageId).AsQueryable();
                result.TotalRecords = msgs.Count();
            }
            else
            {
                msgs = cmsDb.AppUserMessages.Where(a => a.DeviceOwner.IbaId == ibaUserId && a.Deleted == null && a.Message.SendDate < DateTime.UtcNow)
                    .Where(a => (model.From.HasValue && a.Created > model.From.Value) || !model.From.HasValue)
                    .OrderByDescending(a => a.Created)
                    .AsQueryable();
                result.TotalRecords = msgs.Count();
                if (model.Start > 0)
                {
                    msgs = msgs.Skip(model.Start);
                }
                if (model.Length <= 0)
                {
                    model.Length = DefaultPageSize;
                }
                msgs = msgs.Take(model.Length);

            }
            foreach (var msg in msgs.ToList())
            {
                var item = new AppUserMessagesResponseItem
                {
                    AppUserMessageId = msg.Id,
                    Title = msg.Message.Title,
                    MessageType = msg.Message.MessageType,
                    Text = msg.Message.Text,
                    Url = msg.Message.Url ?? "",
                    FormattedDate = msg.Message.SendDate.HasValue ? DateTime.SpecifyKind(msg.Message.SendDate.Value, DateTimeKind.Utc) :  DateTime.SpecifyKind(msg.Created, DateTimeKind.Utc)
                };
                if (msg.Deleted != null)
                {
                    item.Status = AppUserMessagesResponseStatus.Deleted;
                }
                else if (msg.Read !=null)
                {
                    item.Status = AppUserMessagesResponseStatus.Read;
                }
                result.Messages.Add(item);
            }
            return result;
        }

        public static async Task SendPushMessage(AppUserMessage item, DateTime? time = null)
        {
            if(time.HasValue)
            {
                await Push.Instance.SendTimed(item.DeviceOwner.NamedUserId, MessagePush.MessageData(item), item.Id, time.Value);
            } else
            {
                await Push.Instance.Send(item.DeviceOwner.NamedUserId, MessagePush.MessageData(item), item.Id);
            }
            
        }

        public static void SendTestPush()
        {
            for(int i = 0; i < 1; i++)
            {
               // Push.Instance.SendTest("1093096");

            }
        }

        public static void GetSchedulePushes()
        {
            Push.Instance.GetScheduledPushes();
        }

        private static string DateText(DateTime? date)
        {
            return date?.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") ?? "";
        }

        private static ExportMessageLog MsgToExportLog(CmsMessage msg,IBAEntities1 ibaDb)
        {
            var result = new ExportMessageLog
            {
                Id = msg.Id,
                MessageType = msg.MessageType.ToString(),
                SentDate = msg.Created.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                Title = msg.Title,
                TotalRecipients = msg.TotalRecipients
            };
            foreach (var person in msg.AppUserMessages)
            {
                result.Recipients.Add(new ExportRecipients() { Deleted = DateText(person.Deleted), Read = DateText(person.Read), Received = DateText(person.Received), IbaId = person.DeviceOwner.IbaId });
                result.RecipientIds.Add(person.DeviceOwner.IbaId);
                // need to read c_record
            }
            var ibausers = ibaDb.C_records.Where(r => result.RecipientIds.Contains(r.id)).Select(x => new { x.id, x.given_name,x.family_name });
            foreach (var person in result.Recipients)
            {
                var found = ibausers.FirstOrDefault(i => i.id == person.IbaId);
                if (found != null)
                {
                    person.GivenName = found.given_name;
                    person.FamilyName = found.family_name;
                }
            }

            return result;
        }

        private static void WriteMessage(CsvWriter csv, ExportMessageLog log)
        {
            csv.WriteField("MessageId");
            csv.WriteField("SentDate");
            csv.WriteField("Title");
            csv.WriteField("MessageType");
            csv.WriteField("TotalRecipients");
            csv.NextRecord();
            csv.WriteField(log.Id);
            csv.WriteField(log.SentDate);
            csv.WriteField(log.Title);
            csv.WriteField(log.MessageType);
            csv.WriteField(log.TotalRecipients);
            csv.NextRecord();

            csv.WriteField("Records_Id");
            csv.WriteField("Given_Name");
            csv.WriteField("Family_Name");
            csv.WriteField("Received");
            csv.WriteField("Read");
            csv.WriteField("Deleted");
            csv.NextRecord();

            foreach (var line in log.Recipients)
            {
                csv.WriteField(line.IbaId);
                csv.WriteField(line.GivenName);
                csv.WriteField(line.FamilyName);
                csv.WriteField(line.Received);
                csv.WriteField(line.Read);
                csv.WriteField(line.Deleted);
                csv.NextRecord();
            }
        }

        public static byte[] ExportSingleLogToByteArray(long messageId, IbaCmsDbContext cmsDb,IBAEntities1 ibaDb)
        {
            var msg = cmsDb.CmsMessages.FirstOrDefault(m => m.Id == messageId);
            if (msg == null || msg.Status <= CmsMessageStatus.Outbox)
            {
                return null;
            }
            var export = MsgToExportLog(msg,ibaDb);
            using (var memoryStream = new MemoryStream())
            {
                var streamWriter = new StreamWriter(memoryStream);
                var csvWriter = new CsvWriter(streamWriter);
                WriteMessage(csvWriter, export);

                streamWriter.Flush();
                memoryStream.Position = 0;

                return memoryStream.ToArray();
            }
        }

        public static byte[] ExportLogByDateToByteArray(IbaCmsDbContext cmsDb,IBAEntities1 ibaDb ,DateTime? from, DateTime? to)
        {
            var msgs = cmsDb.CmsMessages.Where(m => m.Status > CmsMessageStatus.Outbox).AsQueryable();
            if (from.HasValue)
            {
                msgs = msgs.Where(m => m.Created >= from.Value);
            }
            if (to.HasValue)
            {
                msgs = msgs.Where(m => m.Created <= to.Value);
            }
            using (var memoryStream = new MemoryStream())
            {
                var streamWriter = new StreamWriter(memoryStream);
                var csvWriter = new CsvWriter(streamWriter);
                foreach (var msg in msgs.ToList())
                {
                    var export = MsgToExportLog(msg,ibaDb);
                    WriteMessage(csvWriter, export);
                }
                streamWriter.Flush();
                memoryStream.Position = 0;
                return memoryStream.ToArray();
            }
        }
    }
}
