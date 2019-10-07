using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBAMembersApp.Controllers
{
    public class AppLinksController : Controller
    {
        // GET: AppLinks
        public ActionResult ViewMessage()
        {
            var useragent = Request.UserAgent;

            if (useragent.Contains("Android")) {
                //NOTE: This is probably an Android Device
            return Redirect("https://play.google.com/store/apps/details?id=com.ibamembers&hl=en_GB");
            }

            return Redirect("https://itunes.apple.com/gb/app/iba-members-directory/id1034755558?mt=8");

        }
    }
}