using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBAMembersApp.API.Models.Response
{
    public class GetP2PThreadResponseModel
    {
        public int ThreadId { get; set; }
        public List<P2PMessageResponseModel> Messages { get; set; }
        public int RecipientId { get; set; }
        public DateTimeOffset? OtherParticipantLastSeenDateTime { get; set; }
    }

    public class P2PMessageResponseModel
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public bool SentByMe { get; set; }
        public DateTimeOffset? SentTime { get; set; }
        public DateTimeOffset? DeliveredTime { get; set; }
        public DateTimeOffset? ReadTime { get; set; }
        public string UUID { get; set; }
    }

    public class P2pMessageSendResponseModel
    {
        public bool Success { get; set; }
        public P2PMessageResponseModel Message { get; set; }
    }
}