using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using IBAMembersApp.BusinessLayer.Models;
using IBA_Common;
using IBA_Common.Models;
using Newtonsoft.Json.Linq;
using UrbanAirSharp;
using UrbanAirSharp.Dto;
using System.Threading.Tasks;

namespace IBAMembersApp.BusinessLayer
{
    public class Push
    {
        private static Push _instance;

        public static Push Instance
        {
            get
            {
                _instance = _instance ?? new Push();
                return _instance;
            }
        }
        private static string appKey = AppSettings.UrbanAirshipKey;
        private static string appSecret = AppSettings.UrbanAirshipAppSecret;

        private static UrbanAirSharpGateway client = new UrbanAirSharpGateway(Push.appKey, Push.appSecret);

        public async Task<bool> SendTest(string userId)
        {
            
            var result = await client.PushAsync("TEST PUSH", new List<UrbanAirSharp.Type.DeviceType>() { UrbanAirSharp.Type.DeviceType.Ios, UrbanAirSharp.Type.DeviceType.Android }, null, new List<BaseAlert>()
                    {
                        new AndroidAlert()
                        {
                            Alert = "STRESS TEST PUSH",
                            DelayWhileIdle = true,
                            GcmTimeToLive = 3600
                        },
                        new IosAlert()
                        {
                            Alert = "TEST PUSH",
                            ContentAvailable = true,
                            ApnsTimeToLive = 3600
                        }
                    }, new Audience(UrbanAirSharp.Type.AudienceType.NamedUser, userId));
            return result.Ok;
        }

        public bool GetScheduledPushes()
        {
            var first = client.ListSchedules();
            //var result = await client.DeleteSchedule(new Guid("af014ac6-6a48-4b4b-890a-019afbec3f84"));
            return true;
        }

        public async Task<bool> Send(string namedUserId, MessagePush pushData, long queueId)
        {
            var jData = JObject.FromObject(pushData);
            var paramaterDictionary = new Dictionary<string, string>();
            paramaterDictionary.Add("Id", pushData.Id.ToString());
            paramaterDictionary.Add("Type", pushData.Type);

            var result = await client.PushAsync(pushData.Title, new List<UrbanAirSharp.Type.DeviceType>() { UrbanAirSharp.Type.DeviceType.Ios, UrbanAirSharp.Type.DeviceType.Android }, null, new List<BaseAlert>()
                    {
                        new AndroidAlert()
                        {
                            Alert = pushData.Title,
                            DelayWhileIdle = true,
                            GcmTimeToLive = 3600,
                            Extras = paramaterDictionary
                        },
                        new IosAlert()
                        {
                            Alert = pushData.Title,
                            ContentAvailable = true,
                            ApnsTimeToLive = 3600,
                            Extras = paramaterDictionary
                        }
                    }, new Audience(UrbanAirSharp.Type.AudienceType.NamedUser, namedUserId));
            return result.Ok;
        }


        public async Task<bool> SendTimed(string namedUserId, MessagePush pushData, long queueId, DateTime time)
        {
            var jData = JObject.FromObject(pushData);
            var paramaterDictionary = new Dictionary<string, string>();
            paramaterDictionary.Add("Id", pushData.Id.ToString());
            paramaterDictionary.Add("Type", pushData.PushType.ToString());
            var push = UrbanAirSharpGateway.CreatePush(pushData.Title, new List<UrbanAirSharp.Type.DeviceType>() { UrbanAirSharp.Type.DeviceType.Ios, UrbanAirSharp.Type.DeviceType.Android }, null, new List<BaseAlert>()
                    {
                        new AndroidAlert()
                        {
                            Alert = pushData.Title,
                            DelayWhileIdle = true,
                            GcmTimeToLive = 3600,
                            Extras = paramaterDictionary
                        },
                        new IosAlert()
                        {
                            Alert = pushData.Title,
                            ContentAvailable = true,
                            ApnsTimeToLive = 3600,
                            Extras = paramaterDictionary
                        }
                    }, new Audience(UrbanAirSharp.Type.AudienceType.NamedUser, namedUserId));

            var result = await client.CreateScheduleAsync(new Schedule()
            {
                Push = push,
                ScheduleInfo = new ScheduleInfo()
                {
                    ScheduleTime = time
                }
            });
            return result.Ok;
        }

        /// <summary>
        /// Updates the database table <see cref="Device"/> by setting to
        /// null the push tokens of each device matching the specified push
        /// token and the specified type.
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="pushToken"></param>
        private static void NullifyDevicePushToken(DeviceType deviceType, string pushToken)
        {
            using (var db = new IbaCmsDbContext())
            {
                var devices = db.Devices
                    .Where(device => deviceType.Equals(device.DeviceType))
                    .Where(device => pushToken.Equals(device.PushToken));
                foreach (var device in devices)
                {
                    device.PushToken = null;
                    device.UpdatedOn = DateTime.UtcNow;
                }
                db.SaveChanges();
            }
        }

        private static string GetNestedExceptions(Exception ex)
        {
            var result = ex.Message;
            if (ex.InnerException != null)
            {
                result = result + " | " + GetNestedExceptions(ex.InnerException);
            }
            return result;
        }

        private static string GetErr(string from, AggregateException exception)
        {
            return "Error sending " + from + " Message | " + exception.Message + (exception.InnerException == null
                ? ""
                : " | " + GetNestedExceptions(exception.InnerException));
        }


        private static void GenerateError(string msg, Exception ex)
        {
            throw new Exception(msg + ": " + ex.Message + (ex.InnerException == null ? "" : " | " + ex.InnerException.Message));
        }

        // ReSharper disable once InconsistentNaming
        private static bool IsiOSPriorityLowFor(PushTypeEnum pushType)
        {
            switch (pushType)
            {
                case PushTypeEnum.NewMessageNotification:
                    return false;
                default:
                    return true;
            }
        }
    }

    public abstract class PushData
    {
        public PushTypeEnum PushType { get; protected set; }
    }

    public class MessagePush : PushData
    {

        public static MessagePush MessageData(AppUserMessage appMessage)
        {
            return new MessagePush()
            {
                PushType = PushTypeEnum.NewMessageNotification,
                Id = appMessage.Id,
                Title = appMessage.Message.Title,
                Type = appMessage.Message.MessageType.ToString()
            };
        }

        public static MessagePush MessageData(AppP2PMessage p2pMessage)
        {
            return new MessagePush()
            {
                PushType = PushTypeEnum.NewMessageNotification,
                Id =  p2pMessage.Message.SenderId,
                Title = p2pMessage.Message.GetPushTitleForMessage(),
                Type = "P2P_MESSAGE"
            };
        }

        //public static MessagePush MessageData(PushMessageQueue item)
        //{
        //    if (item.AppP2PMessage != null)
        //    {

        //        return new MessagePush()
        //        {
        //            PushType = PushTypeEnum.NewMessageNotification,
        //            Id = item.AppP2PMessage.Message.Thread.InitialSenderId,
        //            Title = item.AppP2PMessage.Message.GetPushTitleForMessage(),
        //            Type = "P2P_MESSAGE"
        //        };
        //    }
        //    else
        //    {
        //        return new MessagePush()
        //        {
        //            PushType = PushTypeEnum.NewMessageNotification,
        //            Id = item.AppUserMessage.Id,
        //            Title = item.AppUserMessage.Message.Title,
        //            Type = item.AppUserMessage.Message.MessageType.ToString()
        //        };
        //    }

        //}

        private MessagePush() { }

        public long Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

    }

    public enum PushTypeEnum
    {
        NewMessageNotification = 0 // could be more types of push messages later - future proofing
    }
}
