using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBA_Common;
using IBA_Common.Models;
using System;

namespace IBAMembersApp.Models
{
    public class MessageViewModel
    {
        public long Id { get; set; }

        public CmsMessageType MessageType { get; set; }

        public virtual IbaCmsUser Sender { get; set; } // single user maybe become multiple later

        public string Title { get; set; }

        public string Text { get; set; }

        [Url(ErrorMessage = "Please enter a fully qualified URL eg 'http://www.google.com'")]
        [Required(ErrorMessage = "Please enter a fully qualified Url eg 'http://www.google.com'")]
        [UIHint("NewWindowUrl"), DataType(DataType.Url)]
        public string Url { get; set; }

        public string Recipients { get; set; } // ""= all or list of IBA record_ids

        public CmsMessageStatus Status { get; set; }

        [Display(Name = "Send Date (UTC)")]
        public string Created { get; set; }

        [Display(Name = "Status")]
        public string StatusText => Status.ToString();

        [Display(Name = "Message Type")]
        public string MessageTypeText => MessageType.ToString();

        public bool UrlOnly { get; set; }

        [Display(Name = "Recipients")]
        public IEnumerable<SelectListItem> RecipientList { get; set; }

    }

    public class ModifyMessageModel
    {
        public long Id { get; set; }

        public bool UrlOnly { get; set; }

        [Required]
        public string Title { get; set; }

        public CmsMessageType MessageType { get; set; }

        [Required]
        public string Text { get; set; }

        [Url(ErrorMessage = "Please enter a fully qualified URL eg 'http://www.google.com'")]
        [Required(ErrorMessage = "Please enter a fully qualified Url eg 'http://www.google.com'")]
        [UIHint("NewWindowUrl"), DataType(DataType.Url)]
        public string Url { get; set; }

        public HttpPostedFileBase UploadedFile { get; set; }

        public bool ToAll => RecipientList==null || !RecipientList.Any();

        [Display(Name = "Recipients")]
        public IEnumerable<SelectListItem> RecipientList { get; set; }

        public string RecipientsField { get; set; }

        public bool DeleteRecipients { get; set; }

        public DateTime? SendDateTime { get; set; }

        public string TimeZone { get; set; }

        public ModifyMessageModel()
        {
                RecipientList = new List<SelectListItem>();
        }

    }

}