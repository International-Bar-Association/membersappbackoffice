using System;
using System.Linq;
using System.Web.Mvc;
using IBAMembersApp.Models;
using IBAMembersApp.Utilities;
using IBA_Common;
using IBA_Common.Models;
using Microsoft.Ajax.Utilities;

namespace IBAMembersApp.Controllers
{
    public class ContentLibraryController : BaseController
    {

        private string MakeImageUrl(string imageName,bool gridView)
        {
            if (imageName.IsNullOrWhiteSpace())
            {
                return Url.Content(gridView ? AppSettings.GridImagePlaceHolder : AppSettings.ImagePlaceHolder);
            }

            var path = Server.MapPath(AppSettings.ContentLibraryImagePath);
            var imageFile = System.IO.Path.Combine(path, imageName);
            var created = System.IO.File.GetLastWriteTimeUtc(imageFile);
            var result = Url.Content(AppSettings.ContentLibraryImagePath + imageName) + "?time="+created.ToFileTimeUtc();
            return result;
        }

        private ContentLibraryViewModel ContentToGridViewModel(ContentLibrary content)
        {
            return ContentToViewModel(content,true);
        }


        private ContentLibraryViewModel ContentToViewModel(ContentLibrary content,bool gridView)
        {
            var result = new ContentLibraryViewModel
            {
                ContentType = content.ContentType.ToString(),
                Status = content.Status,
                Id = content.Id,
                Created = content.Created?.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                Featured = content.Featured,
                Precis = content.Precis,
                ImageUrl = MakeImageUrl(content.ImageUrl, gridView),
                Title = content.Title,
                Url = content.Url,
                ContentTypeText = content.ContentType.ToString(),
                StatusText = content.Status.ToString()
            };
            return result;
        }

        public ActionResult Index()
        {
            var dbContents = Db.ContentLibraries.Where(c => c.Status != ContentStatus.InActive).OrderByDescending(c => c.Created).ToList();
            var model = dbContents.Select(ContentToGridViewModel).ToList();

            return View(model);
        }

        public ActionResult History()
        {
            var dbContents = Db.ContentLibraries.Where(c => c.Status == ContentStatus.InActive).OrderByDescending(c => c.Created).ToList();
            var model = dbContents.Select(ContentToGridViewModel).ToList();

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var content = Db.ContentLibraries.FirstOrDefault(c => c.Id == id);
            if (content == null)
            {
                return new HttpNotFoundResult("Content item was not found");
            }
            return View(ContentToViewModel(content,false));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ContentLibraryAddModel model)
        {
            ProcessImageUpload imageData = new ProcessImageUpload
            {
                PostedFile = model.UploadedFile,
            };
            if (!imageData.CheckImageOk())
            {
                ModelState.AddModelError("UploadedFile", imageData.Error);
                return View(model);
            }

            if (imageData.Empty && !ModelState.IsValid)
            {
                return View(model);
            }

            //NOTE: model.sendDateTime will be in local time. We need it in the timezone the user has selected
            var correctDateTime = model.AvailableFromDate;

            if (correctDateTime.HasValue)
            {
                var timezone = System.TimeZoneInfo.FindSystemTimeZoneById(model.TimeZone);
                var newDate = System.TimeZoneInfo.ConvertTimeToUtc(model.AvailableFromDate.Value, timezone);

                correctDateTime = newDate;
            }

            var record = new ContentLibrary
            {
                Featured = model.Featured,
                Title = model.Title,
                Precis = model.Precis,
                Url = model.Url,
                Status = ContentStatus.AwaitingDate,
                ContentType = model.ContentType,
                Created = DateTime.UtcNow,
                AvailableFromDate = model.AvailableFromDate != null? correctDateTime : DateTime.UtcNow,
                TimeZone = model.TimeZone
            };
            try
            {
                Db.ContentLibraries.Add(record);
                Db.SaveChanges();
                if (!imageData.Empty && imageData.PostedFileSaveAndResize(Server.MapPath(AppSettings.ContentLibraryImagePath), "item" + record.Id))
                {
                    record.ImageUrl = imageData.SavedName;
                    Db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return View(model);
            }

            if (!ModelState.IsValid || !imageData.Empty)
            {
                return RedirectToAction("Edit", new { id = record.Id });
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var content = Db.ContentLibraries.FirstOrDefault(c => c.Id == id);
            if (content == null)
            {
                return new HttpNotFoundResult("Content item was not found");
            }
            var model = new ContentLibraryEditModel
            {
                ContentType = content.ContentType,
                Featured = content.Featured,
                Id = content.Id,
                CanDeleteImage = !content.ImageUrl.IsNullOrWhiteSpace(),
                ImageUrl = MakeImageUrl(content.ImageUrl,false),
                Precis = content.Precis,
                Title = content.Title,
                Url = content.Url,
                Status = content.Status,
                AvailableFromDate = content.AvailableFromDate,
                TimeZone = content.TimeZone
            };
            if (model.Title.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Title", "Title cannot be left empty");
            }
            if (model.Url.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Url", "Url cannot be left empty");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ContentLibraryEditModel model)
        {
            ProcessImageUpload imageData = new ProcessImageUpload
            {
                PostedFile = model.UploadedFile,
            };
            if (!imageData.CheckImageOk())
            {
                ModelState.AddModelError("UploadedFile", imageData.Error);
                return View(model);
            }

            if (imageData.Empty && !model.DeleteImage && !ModelState.IsValid)
            {
                return View(model);
            }

            var content = Db.ContentLibraries.FirstOrDefault(c => c.Id == model.Id);
            if (content == null)
            {
                return new HttpNotFoundResult("Content item was not found");
            }
            if (model.DeleteImage)
            {
                if (DeleteImage(content.ImageUrl))
                {
                    content.ImageUrl = "";
                }
            }
            else
            {
                if (!imageData.Empty && imageData.PostedFileSaveAndResize(Server.MapPath(AppSettings.ContentLibraryImagePath), "item" + model.Id))
                {
                    content.ImageUrl = imageData.SavedName;
                }
            }
            //NOTE: model.sendDateTime will be in local time. We need it in the timezone the user has selected
            var correctDateTime = model.AvailableFromDate;

            if (correctDateTime.HasValue)
            {
                var timezone = System.TimeZoneInfo.FindSystemTimeZoneById(model.TimeZone);
                var newDate = System.TimeZoneInfo.ConvertTimeToUtc(model.AvailableFromDate.Value, timezone);

                correctDateTime = newDate;
            }


            content.Title = model.Title;
            content.ContentType = model.ContentType;
            content.Precis = model.Precis;
            content.Url = model.Url;
            content.Featured = model.Featured;
            content.AvailableFromDate = model.AvailableFromDate != null ? correctDateTime : DateTime.UtcNow;
            Db.SaveChanges();
            return !imageData.Empty || model.DeleteImage ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("Index");
        }

        public bool DeleteImage(string image)
        {
            var path = System.IO.Path.Combine(Server.MapPath(AppSettings.ContentLibraryImagePath), image);

            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public ActionResult Delete(int id)
        {
            var content = Db.ContentLibraries.FirstOrDefault(c => c.Id == id);
            if (content == null)
            {
                return new HttpNotFoundResult("Content item was not found");
            }
            return View(ContentToViewModel(content,false));
        }

        [HttpPost]
        public ActionResult Delete(ContentLibraryViewModel model)
        {
            try
            {
                var content = Db.ContentLibraries.FirstOrDefault(c => c.Id == model.Id);
                if (content == null)
                {
                    return new HttpNotFoundResult("Content item was not found");
                }
                Db.ContentLibraries.Remove(content);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Deactivate(int id)
        {
            var content = Db.ContentLibraries.FirstOrDefault(c => c.Id == id);
            if (content == null)
            {
                return new HttpNotFoundResult("Content item was not found");
            }
            return View(ContentToViewModel(content,false));
        }

        [HttpPost]
        public ActionResult Deactivate(ContentLibraryViewModel model)
        {
            try
            {
                var content = Db.ContentLibraries.FirstOrDefault(c => c.Id == model.Id);
                if (content == null)
                {
                    return new HttpNotFoundResult("Content item was not found");
                }
                content.Status = ContentStatus.InActive;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Activate(int id)
        {
            var content = Db.ContentLibraries.FirstOrDefault(c => c.Id == id);
            if (content == null)
            {
                return new HttpNotFoundResult("Content item was not found");
            }
            if (content.Title.IsNullOrWhiteSpace() || content.Url.IsNullOrWhiteSpace())
            {
                return RedirectToAction("Edit", new { id = content.Id });
            }

            return View(ContentToViewModel(content,false));
        }

        [HttpPost]
        public ActionResult Activate(ContentLibraryViewModel model)
        {
            try
            {
                var content = Db.ContentLibraries.FirstOrDefault(c => c.Id == model.Id);
                if (content == null)
                {
                    return new HttpNotFoundResult("Content item was not found");
                }
                content.Status = ContentStatus.Active;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}
