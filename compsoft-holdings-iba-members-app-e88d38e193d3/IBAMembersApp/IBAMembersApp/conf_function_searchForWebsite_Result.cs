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
    
    public partial class conf_function_searchForWebsite_Result
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public decimal conference_id { get; set; }
        public string title { get; set; }
        public string location { get; set; }
        public bool DisplayLocation { get; set; }
        public System.DateTime function_start { get; set; }
        public System.DateTime function_end { get; set; }
        public decimal type_id { get; set; }
        public decimal status_id { get; set; }
        public string description { get; set; }
        public string notes { get; set; }
        public bool showcase { get; set; }
        public string dress_code { get; set; }
        public decimal warning_level { get; set; }
        public decimal TotalTickets { get; set; }
        public decimal RegisteredTickets { get; set; }
        public decimal WaitListedTickets { get; set; }
        public decimal ReturnedTickets { get; set; }
        public int PassesTotal { get; set; }
        public decimal PassesRegistered { get; set; }
        public Nullable<decimal> PassesNotUsed { get; set; }
        public Nullable<decimal> TotalRegistered { get; set; }
        public Nullable<decimal> InProgress { get; set; }
    }
}
