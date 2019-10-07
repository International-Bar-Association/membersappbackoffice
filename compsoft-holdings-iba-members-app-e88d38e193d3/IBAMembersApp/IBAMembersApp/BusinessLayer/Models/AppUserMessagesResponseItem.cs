using System;
using IBA_Common.Models;

namespace IBAMembersApp.BusinessLayer.Models
{
    public class AppUserMessagesResponseItem
    {
        [Obsolete("2016/10/12 this has been kept for backwards compatibility with clients depending on the old date format. Use FormattedDate")]
        public string Date => FormattedDate.ToString(@"yyyy-MM-ddTHH\:mm\:ss.fff");

        public DateTimeOffset FormattedDate { get; set; }

        public long AppUserMessageId { get; set; }

        public CmsMessageType MessageType { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public AppUserMessagesResponseStatus Status { get; set; }
    }

    public enum AppUserMessagesResponseStatus
    {
        Unread = 0,
        Read = 1,
        Deleted = 2
    }
}
