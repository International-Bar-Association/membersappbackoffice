using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using IBA_Common;
using IBA_Common.Models;

namespace IBAMembersApp.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {


            var conf = IbaDb.conf_conference.SingleOrDefault(r => r.id == AppSettings.ConferenceId);


            var model = new HomeDashboardModel
            {
                PendingMessages = Db.CmsMessages.Count(m => m.Status == CmsMessageStatus.Draft),
                OutBox = Db.CmsMessages.Count(m => m.Status == CmsMessageStatus.Outbox),
                UnRead = Db.AppUserMessages.Count(a => a.Read == null),
                PendingContent = Db.ContentLibraries.Count(c => c.Status == ContentStatus.Pending),
                ActiveContent = Db.ContentLibraries.Count(c => c.Status == ContentStatus.Active),
                DeviceOwners = Db.DeviceOwners.Count(),
                ShouldShowConferenceDetails = AppSettings.ShowConferenceDetails,
                ShowConferenceDetails = AppSettings.ShowConferenceDetails? "Yes":"No",
                Url = AppSettings.ConferenceUrl,
                ConferenceName = conf != null ? conf.title : "",
                Start = conf != null ? ((DateTime)conf.start_date).ToString("yyyy-MM-dd HH:mm:ss") : "",
                Finish = conf != null ? ((DateTime)conf.start_date).ToString("yyyy-MM-dd HH:mm:ss") : ""

            };
            var dMonth = DateTime.UtcNow.AddDays(-28);
            model.DeviceOwnersMonth = Db.DeviceOwners.Count(o => o.LastDeviceUpdate !=null && o.LastDeviceUpdate>=dMonth);
            model.RefreshInterval = 30000;

            return View(model);
        }
    }

    public class HomeDashboardModel
    {
        [Display(Name="Messages Being Edited")]
        public int PendingMessages { get; set; }
        [Display(Name = "Messages Still to Be Processed")]
        public int OutBox { get; set; }
        [Display(Name = "Messages in Push Queue")]
        public int OutstandingPushMessages{ get; set; }

        [Display(Name = "Unread Messages")]
        public int UnRead { get; set; }

        [Display(Name = "Content Being Edited")]
        public int PendingContent { get; set; }

        [Display(Name = "Active Content")]
        public int ActiveContent { get; set; }

        [Display(Name = "V2 Device Owners Active")]
        public int DeviceOwners { get; set; }

        [Display(Name = "V2 Device Owners Active in Past Month")]
        public int DeviceOwnersMonth { get; set; }

        public int RefreshInterval { get; set; }

        [Display(Name = "Conference Name")]
        public string ConferenceName { get; set; }

        [Display(Name = "Show Conference Details")]
        public string ShowConferenceDetails { get; set; }
        public bool ShouldShowConferenceDetails { get; set; }

        [Display(Name = "Conference Url")]
        public string Url { get; set; }

        [Display(Name = "Conference Start")]
        public string Start { get; set; }

        [Display(Name = "Conference End")]
        public string Finish { get; set; }

    }
}