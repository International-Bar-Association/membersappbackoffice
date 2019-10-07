using System.Collections.Generic;
using IBAMembersApp.Models;

namespace IBAMembersApp.API.Models.Response
{
    public class ProfileResponseModel:BaseResponseModel
    {
        public string FirstName { get; set; }
        public decimal Id { get; set; }
        public bool Public { get; set; }
        public string LastName { get; set; }
        public string FirmName { get; set; }
        public string JobPosition { get; set; }
        public string ProfilePicture { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        public string Phone { get; set; }
        public List<decimal> AreasOfPractice { get; set; }
        public List<decimal> Committees { get; set; }
        public ProfileAddress Address { get; set; }
        public ProfileAccess Access { get; set; }
        public decimal? CurrentlyAttendingConference { get; set; }

        public ProfileResponseModel()
        {
            Committees = new List<decimal>();
            AreasOfPractice = new List<decimal>();
            Access = new ProfileAccess();
        }
    }

    public class ProfileAccess
    {
        public decimal Class { get; set; }
        public bool CanSearchDirectory { get; set; }
    }
}