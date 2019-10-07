using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBAMembersApp.Models
{
    public class P2PEmailViewModel
    {
        public string MessageContents { get; set; }
        public string SenderName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string SentDate { get; set; }
    }
}