using System.ComponentModel.DataAnnotations;
using System.Web;
using IBA_Common.Models;
using System;

namespace IBAMembersApp.Models
{
    public class ContentLibraryViewModel
    {
        public long Id { get; set; }

        [Display(Name="Thumbnail")]
        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Precis { get; set; }

        public string ContentType  { get; set; }

        [UIHint("NewWindowUrl"), DataType(DataType.Url)]
        public string Url { get; set; }

        public bool Featured { get; set; }

        public ContentStatus Status { get; set; }

        public string Created { get; set; }

        [Display(Name = "Type")]
        public string ContentTypeText { get; set; }

        [Display(Name = "Status")]
        public string StatusText { get; set; }

        public DateTime? AvailableFromDate { get; set; }

    }

    public class ContentLibraryAddModel
    {
        [Display(Name = "Thumbnail")]
        public string ImageUrl { get; set; }

        [Required]
        public string Title { get; set; }

        public string Precis { get; set; }

        public ContentLibraryType ContentType { get; set; }

        [Url(ErrorMessage = "Please enter a fully qualified URL eg 'http://www.google.com'")]
        [Required(ErrorMessage = "Please enter a fully qualified Url eg 'http://www.google.com'")]
        [UIHint("NewWindowUrl"), DataType(DataType.Url)]
        public string Url { get; set; }

        public bool Featured { get; set; }

        public HttpPostedFileBase UploadedFile { get; set; }

        public DateTime? AvailableFromDate { get; set; }

        public string TimeZone { get; set; }

    }

    public class ContentLibraryEditModel : ContentLibraryAddModel
    {
        public long Id { get; set; }

        public ContentStatus Status { get; set; }

        public bool DeleteImage { get; set; }

        public bool CanDeleteImage { get; set; }

    }

}