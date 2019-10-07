using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CsvHelper;
using IBAMembersApp.BusinessLayer.Models;
using IBAMembersApp.Models;
using Newtonsoft.Json;

namespace IBAMembersApp.Utilities
{
    public class ProcessRecipientsCsvUpload
    {
        public HttpPostedFileBase PostedFile { get; set; }

        public string Error { get; set; }

        public bool Empty { get; set; }

        public string Recipients { get; set; }
    
        public ProcessRecipientsCsvUpload()
        {
           Error = "";
        }

        public bool CheckCsvOk()
        {
            if (PostedFile == null || PostedFile.FileName == null)
            {
                Empty = true;
                return true;
            }
            List<CsvRecipientModel> recipients;
            try
            {
                using (var reader = new StreamReader(PostedFile.InputStream))
                {
                    var csv = new CsvReader(reader);
                    var recips = csv.GetRecords<CsvRecipientModel>();
                    recipients = recips.ToList();
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
                if (e.InnerException != null)
                {
                    Error = Error + " | " + e.InnerException.Message;
                }
                return false;
            }
            if (recipients.Count==0)
            {
                Error = "Invalid data in upload file";
                return false;
            }
            var recipientsModel = new List<StoreMessageRecipients>();
            foreach (var recip in recipients)
            {
                recipientsModel.Add(new StoreMessageRecipients() {Id=recip.id,Name=recip.family_name+","+recip.given_name});
            }
            Recipients = JsonConvert.SerializeObject(recipientsModel);
            return true;
        }

    }
}