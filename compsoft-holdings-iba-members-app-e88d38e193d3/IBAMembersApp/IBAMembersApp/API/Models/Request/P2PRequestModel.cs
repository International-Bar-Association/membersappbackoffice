using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBAMembersApp.API.Models.Request
{
    public class P2PRequestModel
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public string UUID { get; set; }
    }

    public class P2PThreadHideRequestModel
    {
        public int ThreadId { get; set; }
    }
}