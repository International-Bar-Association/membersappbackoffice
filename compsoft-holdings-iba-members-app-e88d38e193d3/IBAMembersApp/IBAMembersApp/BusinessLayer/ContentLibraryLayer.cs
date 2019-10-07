using System;
using System.Linq;
using IBAMembersApp.BusinessLayer.Models;
using IBA_Common;
using IBA_Common.Models;

namespace IBAMembersApp.BusinessLayer
{
    public class ContentLibraryLayer
    {
        private const int DefaultPageSize = 20;

        public static ContentLibraryResponseModel RequestContent(IbaCmsDbContext db, ContentLibraryRequestModel model)
        {
            var response = new ContentLibraryResponseModel();
            CheckTimedContent(db);
            var dbContent = db.ContentLibraries.Where(c => c.Status == ContentStatus.Active).OrderByDescending(c=>c.Created).AsQueryable();
            response.TotalRecords = dbContent.Count();
            if (model.Start > 0)
            {
                dbContent = dbContent.Skip(model.Start);
            }
            if (model.Length <= 0)
            {
                model.Length = DefaultPageSize;
            }
            dbContent = dbContent.Take(model.Length);
            foreach (var content in dbContent)
            {
                var item = new ContentLibraryItemsResponseModel()
                {
                    Id = content.Id,
                    ThumbnailUrl = content.ImageUrl??"",
                    Title = content.Title,
                    Precis = content.Precis??"",
                    ContentType = content.ContentType,
                    Url = content.Url,
                    Featured = content.Featured,
                    CreatedDate = content.Created.HasValue ? DateTime.SpecifyKind(content.Created.Value, DateTimeKind.Utc) : content.Created
                };
                response.Items.Add(item);
            }
            return response;
        }

        public static void CheckTimedContent(IbaCmsDbContext db)
        {
            var dbContent = db.ContentLibraries.Where(c => c.Status == ContentStatus.AwaitingDate).OrderByDescending(c => c.Created).AsQueryable();
            foreach(var content in dbContent)
            {
                if (DateTime.Compare(DateTime.UtcNow, (DateTime)content.AvailableFromDate) > 0)
                {
                    content.Status = ContentStatus.Active;
                    content.Created = content.AvailableFromDate;
                }
            }
            db.SaveChanges();
        }
    }
}
