using System.Linq;
using System.Web.Mvc;
using IBAMembersApp.BusinessLayer;
using IBAMembersApp.BusinessLayer.Models;
using IBAMembersApp.Models;
using IBAMembersApp.Utilities;
using IBA_Common.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using IBA_Common;
using Compsoft.EmailLib;
using System.Threading.Tasks;

namespace IBAMembersApp.Controllers
{
    public class MessagesController : BaseController
    {
        // GET: Messages
        public ActionResult Index()
        {
            var msg = MessageLayer.ViewAllMessages(Db);
            return View(msg);
        }

        private ActionResult DoCreate(ModifyMessageModel model)
        {
            ModelState.Remove(model.UrlOnly ? "Text" : "Url");
            ProcessRecipientsCsvUpload csvData = new ProcessRecipientsCsvUpload
            {
                PostedFile = model.UploadedFile,
            };
            if (!csvData.CheckCsvOk()) // check csv upload before modelstate so can process irrespectively
            {
                ModelState.AddModelError("UploadedFile", csvData.Error);
                model.RecipientList = MessageLayer.FieldToRecipientList(model.RecipientsField);
                return View("Create", model);
            }
            if (csvData.Empty && !ModelState.IsValid)
            {
                model.RecipientList = MessageLayer.FieldToRecipientList(model.RecipientsField);
                return View("Create", model);
            }
            var store = MessageLayer.ConvertModifyMessageModelToStoreMessage(model);
            store.UserId = User.Identity.GetUserId();
            if (!csvData.Empty)
            {
                store.Recipients = csvData.Recipients;
            }
            //model.Recipients;
            if (!MessageLayer.AddMessage(Db, store))
            {
                model.RecipientList = MessageLayer.FieldToRecipientList(model.RecipientsField);
                return View("Create", model);
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = store.NewId });
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreateUrl()
        {
            ModifyMessageModel model = new ModifyMessageModel() { UrlOnly = true };
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult CreateUrl(ModifyMessageModel model)
        {
            return DoCreate(model);
        }

        public ActionResult CreateText()
        {
            ModifyMessageModel model = new ModifyMessageModel() { UrlOnly = false };
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult CreateText(ModifyMessageModel model)
        {
            return DoCreate(model);
        }

        private ModifyMessageModel MessageToModifyMessageModel(CmsMessage msg)
        {

            var result = new ModifyMessageModel
            {
                UrlOnly = msg.UrlOnly,
                Text = msg.Text,
                Url = msg.Url,
                Id = msg.Id,
                MessageType = msg.MessageType,
                Title = msg.Title,
                RecipientsField = msg.Recipients,
                SendDateTime = msg.SendDate,
                TimeZone = msg.TimeZone,
                RecipientList = MessageLayer.FieldToRecipientList(msg.Recipients)
            };
            return result;
        }

        public ActionResult Edit(long id)
        {
            var msg = Db.CmsMessages.FirstOrDefault(m => m.Id == id);
            if (msg == null)
            {
                return new HttpNotFoundResult("Message item was not found");
            }
            var model = MessageToModifyMessageModel(msg);
            if (model.Title.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Title", "Title cannot be left empty");
            }
            if (model.UrlOnly && model.Url.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Url", "Url cannot be left empty");
            }
            if (!model.UrlOnly && model.Text.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Text", "Text cannot be left empty");
            }
            return View(model);
        }

        public ActionResult TempEmailView()
        {
            var model = new P2PEmailViewModel()
            {
                SenderName = "Gary Kendall",
                MessageContents = "I am such a plum-sauce it is unbelievable. I wish I could be better at snooker and win design awards but being mayor of plum town just takes up too much of my time.P.S I miss my Xbox.",
                ProfileImageUrl = String.Format("{0}/{1}", AppSettings.ProfileImageAddress, "68_120926.jpg"),
                SentDate = DateTime.UtcNow.ToString("dd MMMM yyyy hh:mm")
            };
            EmailHelper.SendEmail("creative@compsoft.co.uk", "IBA Members App Notification", model, this.ControllerContext, "P2PMessageEmail");
            return View("P2PMessageEmail", model);
        }

        [HttpPost]
        public ActionResult Edit(ModifyMessageModel model)
        {
            var updateRecipients = false;
            ModelState.Remove(model.UrlOnly ? "Text" : "Url");
            ProcessRecipientsCsvUpload csvData = new ProcessRecipientsCsvUpload
            {
                PostedFile = model.UploadedFile,
            };
            if (!csvData.CheckCsvOk()) // check csv upload before modelstate so can process irrespectively
            {
                ModelState.AddModelError("UploadedFile", csvData.Error);
                model.RecipientList = MessageLayer.FieldToRecipientList(model.RecipientsField);
                return View(model);
            }
            if (csvData.Empty && !model.DeleteRecipients && !ModelState.IsValid)
            {
                model.RecipientList = MessageLayer.FieldToRecipientList(model.RecipientsField);
                return View(model);
            }
            var msg = Db.CmsMessages.FirstOrDefault(m => m.Id == model.Id);
            if (msg == null)
            {
                return new HttpNotFoundResult("Message item was not found");
            }
            var store = MessageLayer.ConvertModifyMessageModelToStoreMessage(model);
            if (!csvData.Empty)
            {
                updateRecipients = true;
                store.Recipients = csvData.Recipients;
            }
            else if (model.DeleteRecipients)
            {
                updateRecipients = true;
                store.Recipients = "";
            }
            else
            {
                store.Recipients = model.RecipientsField;
            }
            try
            {
                MessageLayer.UpdateMessage(Db, store, msg);
            }
            catch (Exception)
            {
                //FIXME: add logging here
                model.RecipientList = MessageLayer.FieldToRecipientList(model.RecipientsField);
                return View(model);
            }
            return updateRecipients ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {
            var msg = Db.CmsMessages.FirstOrDefault(m => m.Id == id);
            if (msg == null)
            {
                return new HttpNotFoundResult("Message item was not found");
            }
            var model = MessageLayer.MsgToMessageViewModel(msg, true);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(MessageViewModel model)
        {
            try
            {
                var msg = Db.CmsMessages.FirstOrDefault(m => m.Id == model.Id);
                if (msg == null)
                {
                    return new HttpNotFoundResult("Message item was not found");
                }
                Db.CmsMessages.Remove(msg);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Details(long id)
        {
            var msg = Db.CmsMessages.FirstOrDefault(m => m.Id == id);
            if (msg == null)
            {
                return new HttpNotFoundResult("Message item was not found");
            }
            var model = MessageLayer.MsgToMessageViewModel(msg, true);
            return View(model);
        }

        public ActionResult Send(long id)
        {
            var msg = Db.CmsMessages.FirstOrDefault(m => m.Id == id);
            if (msg == null)
            {
                return new HttpNotFoundResult("Message item was not found");
            }
            if (msg.Title.IsNullOrWhiteSpace() || (msg.UrlOnly && msg.Url.IsNullOrWhiteSpace()) ||
                (!msg.UrlOnly && msg.Text.IsNullOrWhiteSpace()))
            {
                return RedirectToAction("Edit", new { id = msg.Id });
            }
            var model = MessageLayer.MsgToMessageViewModel(msg, true);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Send(MessageViewModel model)
        {
            try
            {
                var msg = Db.CmsMessages.FirstOrDefault(m => m.Id == model.Id);
                if (msg == null)
                {
                    return new HttpNotFoundResult("Message item was not found");
                }
                msg.Status = CmsMessageStatus.Outbox;
                MessageLayer.ProcessOneMessage(Db, msg);
                await MessageLayer.SendMessage(Db, msg.Id, msg.SendDate);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult SendTestPush(string environment)
        {
            MessageLayer.SendTestPush();
            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public ActionResult SendTestEmail(string email)
        {
            var controllerContext = this.ControllerContext;
            EmailHelper.SendEmail("creative@compsoft.co.uk", "IBA Members App Notification", new P2PEmailViewModel() { SenderName = "Test Email Send", ProfileImageUrl = null, MessageContents = "This is a test to ensure that the email client is working correctly." }, controllerContext, "~/Views/Messages/P2PMessageEmail.cshtml");
            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public ActionResult GetScheduledPushes()
        {
            MessageLayer.GetSchedulePushes();
            return new HttpStatusCodeResult(200);
        }

        public ActionResult Archive(long id)
        {
            var msg = Db.CmsMessages.FirstOrDefault(m => m.Id == id);
            if (msg == null)
            {
                return new HttpNotFoundResult("Message item was not found");
            }
            var model = MessageLayer.MsgToMessageViewModel(msg, true);
            return View(model);
        }

        [HttpPost]
        public ActionResult Archive(MessageViewModel model)
        {
            try
            {
                var msg = Db.CmsMessages.FirstOrDefault(m => m.Id == model.Id);
                if (msg == null)
                {
                    return new HttpNotFoundResult("Message item was not found");
                }
                msg.Status = CmsMessageStatus.Archived;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        public FileContentResult ExportToCsv(long id)
        {
            var result = MessageLayer.ExportSingleLogToByteArray(id, Db, IbaDb);
            return File(result, System.Net.Mime.MediaTypeNames.Text.Plain, "export.csv");
        }

        public ActionResult ExportLog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ExportLog(ExportByDateParametersModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.From.HasValue && model.To.HasValue && model.From.Value > model.To.Value)
            {
                ModelState.AddModelError("To", "The 'From' cannot be greater than the 'To' Date");
                return View(model);
            }

            var result = MessageLayer.ExportLogByDateToByteArray(Db, IbaDb, model.From, model.To);
            return File(result, System.Net.Mime.MediaTypeNames.Text.Plain, "export.csv");
        }
    }
}
