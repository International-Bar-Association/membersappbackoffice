using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBAMembersApp.API.Models.Response
{
    public class ConnectionResponseModel
    {
        public int UserId { get; set; }
        public string UserProfileImageUrl { get; set; }
        public string Name { get; set; }
        public P2PMessageResponseModel LastMessage { get; set; }
    }
}