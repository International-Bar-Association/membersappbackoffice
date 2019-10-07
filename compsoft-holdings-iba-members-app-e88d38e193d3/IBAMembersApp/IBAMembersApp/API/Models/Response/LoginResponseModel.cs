using System;

namespace IBAMembersApp.API.Models.Response
{
    public class LoginResponseModel : BaseResponseModel
    {
        public Guid SessionToken { get; set; }
        public ProfileResponseModel Profile { get; set; }
    }
}