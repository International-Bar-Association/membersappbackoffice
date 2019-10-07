using System;
using System.Collections.Generic;
using IBA_Common.Models;

namespace IBAMembersApp.BusinessLayer.Models
{
    public class ContentLibraryResponseModel
    {
        public int TotalRecords { get; set; }

        public List<ContentLibraryItemsResponseModel> Items { get; set; }

        public ContentLibraryResponseModel()
        {
            Items = new List<ContentLibraryItemsResponseModel>();
        }
    }

    public class ContentLibraryItemsResponseModel
    {
        public long Id { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Title { get; set; }

        public string Precis { get; set; }

        public ContentLibraryType ContentType { get; set; }

        public string Url { get; set; }

        public bool Featured { get; set; }

        [Obsolete("2016/10/12 this has been kept for backwards compatibility with clients depending on the old date format. Use CreatedDate")]
        public string Created => CreatedDate?.ToString(@"yyyy-MM-ddTHH\:mm\:ss.fff");

        public DateTimeOffset? CreatedDate { get; set; }

    }
}
