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
    
    public partial class conf_paper_contributor_listByPaper_Result
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public decimal conference_id { get; set; }
        public decimal paper_id { get; set; }
        public Nullable<decimal> contributor_type_id { get; set; }
        public string contributor_type_title { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string contributor_name { get; set; }
        public bool is_member { get; set; }
        public string is_member_text { get; set; }
        public System.DateTime date_created { get; set; }
        public decimal administrator_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<decimal> administrator_modified { get; set; }
    }
}
