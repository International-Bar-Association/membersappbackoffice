using System.Collections.Generic;

namespace IBAMembersApp.BusinessLayer.Models
{
    public class ExportMessageLog
    {
        public long Id { get; set; }

        public string SentDate { get; set; }

        public string MessageType { get; set; }

        public string Title { get; set; }

        public int TotalRecipients { get; set; }

        public List<ExportRecipients> Recipients { get; set; }

        public List<decimal> RecipientIds { get; set; }

        public ExportMessageLog()
        {
            Recipients = new List<ExportRecipients>();
            RecipientIds = new List<decimal>();
        }

    }

    public class ExportRecipients
    {
        public decimal IbaId { get; set; }

        public string Received { get; set; }

        public string Read { get; set; }

        public string Deleted { get; set; }

        public string FamilyName { get; set; }

        public string GivenName { get; set; }

    }
}