namespace IBAMembersApp.API.Models.Request
{
    public class ProfileSearchRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirmName { get; set; }
        public string City { get; set; }
        public int? Country { get; set; }
        public int? Committee { get; set; }
        public int? AreaOfPractice { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 100;
        public bool OnlyConferenceAttendees { get; set; }
    }
}
