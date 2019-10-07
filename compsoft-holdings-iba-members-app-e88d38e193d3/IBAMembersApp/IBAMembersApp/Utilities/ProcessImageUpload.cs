using System;
using System.Drawing;
using System.Web;
using IBA_Common;

namespace IBAMembersApp.Utilities
{
    public class ProcessImageUpload
    {
        private bool Checked { get; set; }

        private Image ImageFile { get; set; }

        private int ImageHeight { get; set; }

        private int ImageWidth { get; set; }

        public HttpPostedFileBase PostedFile { get; set; }

        public int MaxWidth { get; set; }

        public int MaxHeight { get; set; }

        public int MinWidth { get; set; }

        public int MinHeight { get; set; }

        public string Error { get; set; }

        public bool Empty { get; set; }

        public string SavedName { get; set; }

        private int MaxFileSize { get; set; }

        public ProcessImageUpload()
        {
            MaxHeight = AppSettings.MaxThumbNailHeight;
            MaxWidth = AppSettings.MaxThumbNailWidth;
            MinHeight = AppSettings.MinThumbNailHeight;
            MinWidth = AppSettings.MinThumbNailWidth;
            MaxFileSize = 2*1024*1024;  //2mb
            Error = "";
        }

        public bool CheckImageOk()
        {
            Checked = true;
            if (PostedFile == null || PostedFile.FileName == null)
            {
                Empty = true;
                return true;
            }
            var extension = System.IO.Path.GetExtension(PostedFile.FileName).ToLower();
            if (extension != ".png" && extension != ".jpg")
            {
                Error="Only .PNG and .JPG files are allowed";
                return false;
            }
            if (PostedFile.ContentLength > MaxFileSize)
            {
                Error = "File is too large, please reduce it to 2mb.";
                return false;
            }
            try
            {
                ImageFile = Image.FromStream(PostedFile.InputStream);
                ImageHeight = ImageFile.Height;
                ImageWidth = ImageFile.Width;

                if (ImageHeight < MinHeight)
                {
                    Error = "Image Height =" + ImageHeight + "px, it must be a minimum of " + MinHeight + " px";
                }
                if (ImageWidth < MinWidth)
                {
                    Error = Error + (Error == "" ? "" : ", ") + "Image Width =" + ImageWidth + "px, it must be a minimum of " + MinWidth + " px";
                }
            }
            catch (Exception e)
            {
                Error = "Error processing Image | " + e.Message;
            }
            return Error == "";
        }

        public bool PostedFileSaveAndResize(string outputFilePath,string outputFilename)
        {
            if (!Checked)
            {
                if (!CheckImageOk())
                {
                    return false;
                }
            }
            if (Empty)
            {
                return true;
            }

            try
            {
                if (MaxWidth !=0 && ImageWidth > MaxWidth)
                {
                    ImageHeight = (ImageHeight * MaxWidth) / ImageWidth;
                    ImageWidth = MaxWidth;
                }
                if (MaxHeight!=0 && (ImageHeight > MaxHeight))
                {
                    ImageWidth = (ImageWidth * MaxHeight) / ImageHeight;
                    ImageHeight = MaxHeight;
                }
                var bitmapFile = new Bitmap(ImageFile, ImageWidth, ImageHeight);
                SavedName = outputFilename + ".png";
                var path = System.IO.Path.Combine(outputFilePath, SavedName);
                bitmapFile.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

    }
}