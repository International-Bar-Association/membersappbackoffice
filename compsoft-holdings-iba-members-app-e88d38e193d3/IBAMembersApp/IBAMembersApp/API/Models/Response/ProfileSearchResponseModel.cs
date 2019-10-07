using IBAMembersApp.Models;

namespace IBAMembersApp.API.Models.Response
{
    public class ProfileSearchResponseModel : BaseResponseModel
    {
        public decimal UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirmName { get; set; }
        public string JobPosition { get; set; }
        public string ProfilePicture { get; set; }
        public decimal? CurrentlyAttendingConference { get; set; }
        public ProfileAddress Address { get; set; }
    }
}