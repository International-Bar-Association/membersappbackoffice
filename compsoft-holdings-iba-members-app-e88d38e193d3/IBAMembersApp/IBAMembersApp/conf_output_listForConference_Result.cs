//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IBAMembersApp
{
    using System;
    
    public partial class conf_output_listForConference_Result
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public string title { get; set; }
        public string file_name { get; set; }
        public string file_type { get; set; }
        public string mime_type { get; set; }
        public Nullable<decimal> file_size { get; set; }
        public bool publish { get; set; }
        public decimal type_id { get; set; }
        public string type_name { get; set; }
        public decimal status_id { get; set; }
        public decimal access_id { get; set; }
        public System.DateTime date_created { get; set; }
        public decimal administrator_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<decimal> administrator_modified { get; set; }
        public Nullable<System.DateTime> date_printed_full { get; set; }
    }
}