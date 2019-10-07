using System;
using System.Collections.Generic;

namespace IBAMembersApp.API.Models.Response
{
    public class RefreshDataResponseModel
    {
        public IDictionary<decimal, string> AreasOfPracticeDict { get; set; }
        public IDictionary<decimal, string> CommitteesDict { get; set; }
        public Conference Conference { get; set; }

        public RefreshDataResponseModel()
        {
            AreasOfPracticeDict = new Dictionary<decimal, string>();
            CommitteesDict = new Dictionary<decimal, string>();
        }

    }

    public class Conference
    {
        public bool ShowDetails { get; set; }
    
        public decimal ConferenceId { get; set; }

        public string Url { get; set; }

        [Obsolete("2016/10/12 this has been kept for backwards compatibility with clients depending on the old date format. Use StartDate")]
        public string Start => StartDate?.ToString(@"yyyy-MM-ddTHH\:mm\:ss.fff");

        public DateTimeOffset? StartDate { get; set; }

        [Obsolete("2016/10/12 this has been kept for backwards compatibility with clients depending on the old date format. Use FinishDate")]
        public string Finish => FinishDate?.ToString(@"yyyy-MM-ddTHH\:mm\:ss.fff");

        public DateTimeOffset? FinishDate { get; set; }

    }

    public class ConferenceAttendee
    {
        public decimal Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirmName { get; set; }
        public string ProfilePicture { get; set; }
    }
}
