using System;
using System.Collections.Generic;
using IBA_Common.Models;

namespace IBAMembersApp.BusinessLayer.Models
{
    public class StoreMessageModel
    {
        public CmsMessageType MessageType { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public bool UrlOnly { get; set; }

        public string Recipients { get; set; } 

        public string UserId { get; set; }

        public long NewId { get; set; }

        public DateTime? SendDate { get; set; }

        public string TimeZone { get; set; }
    }

    public class StoreMessageRecipients
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
    }

    public class MessageRequestModel
    {
        public DateTimeOffset? From { get; set; }

        public long AppUserMessageId { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }
    }

    public class MessageResponseModel
    {
        public List<AppUserMessagesResponseItem> Messages { get; set; }

        public int TotalRecords { get; set; }

        public MessageResponseModel()
        {
            Messages=new List<AppUserMessagesResponseItem>();
        }
    }

    public enum MessageLoggingTypes
    {
        HeartBeat = 0,
        Apns = 1,
        Gcm = 2,
        ProcessMessage = 3,
        PushMessage = 4
    }
}
