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
    
    public partial class conf_speakerFunction_search_Result
    {
        public Nullable<decimal> id { get; set; }
        public Nullable<System.Guid> uid { get; set; }
        public Nullable<decimal> conference_id { get; set; }
        public string title { get; set; }
        public Nullable<System.DateTime> function_start_date_full { get; set; }
        public string function_start_date { get; set; }
        public string function_end_date { get; set; }
        public string function_start_time { get; set; }
        public string function_end_time { get; set; }
        public string location { get; set; }
        public Nullable<decimal> type_id { get; set; }
        public string type { get; set; }
        public Nullable<decimal> status_id { get; set; }
        public string status { get; set; }
        public Nullable<decimal> role_id { get; set; }
        public string role { get; set; }
        public Nullable<bool> paper_submitted { get; set; }
        public Nullable<bool> abstract_submitted { get; set; }
        public Nullable<bool> biography_submitted { get; set; }
        public Nullable<bool> keywords_submitted { get; set; }
        public Nullable<bool> license_to_publish { get; set; }
        public Nullable<bool> publish { get; set; }
        public Nullable<int> total_results { get; set; }
        public Nullable<int> total_pages { get; set; }
    }
}
