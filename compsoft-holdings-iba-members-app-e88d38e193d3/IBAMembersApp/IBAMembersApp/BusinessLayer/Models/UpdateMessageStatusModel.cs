using System;

namespace IBAMembersApp.BusinessLayer.Models
{
    public class UpdateMessageStatusModel
    {
        public long AppUserMessageId { get; set; }

        public DateTime? Read { get; set; } 

        public DateTime? Received { get; set; }

        public DateTime? Deleted { get; set; }

    }
}