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
    
    public partial class conf_conference_search_Result
    {
        public Nullable<decimal> id { get; set; }
        public Nullable<System.Guid> uid { get; set; }
        public string title { get; set; }
        public string city { get; set; }
        public string start_date { get; set; }
        public Nullable<decimal> places { get; set; }
        public Nullable<decimal> warning_level { get; set; }
        public string country { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public Nullable<int> total_results { get; set; }
        public Nullable<int> total_pages { get; set; }
        public int Registered { get; set; }
        public int WaitListed { get; set; }
        public int InProgress { get; set; }
        public int PassesTotal { get; set; }
        public int PassesRegistered { get; set; }
        public Nullable<int> PassesNotUsed { get; set; }
        public Nullable<int> TotalRegistered { get; set; }
        public string highlight { get; set; }
    }
}