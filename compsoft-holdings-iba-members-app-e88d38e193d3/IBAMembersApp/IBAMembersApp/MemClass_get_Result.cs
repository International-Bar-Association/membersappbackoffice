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
    
    public partial class MemClass_get_Result
    {
        public decimal id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string text { get; set; }
        public string self_service_text { get; set; }
        public string proforma_text { get; set; }
        public decimal parent { get; set; }
        public bool individual { get; set; }
        public decimal default_status { get; set; }
        public bool charge_fees { get; set; }
        public bool set_joined_expiry_dates { get; set; }
        public bool member_committee_fees { get; set; }
        public decimal committee { get; set; }
        public string restricted_committees { get; set; }
        public bool stage1 { get; set; }
        public bool stage2 { get; set; }
        public bool stage3 { get; set; }
        public bool stage4 { get; set; }
        public bool stage5 { get; set; }
        public bool stage6 { get; set; }
        public bool self_service { get; set; }
        public bool ss_edit_membership { get; set; }
        public bool ss_members_directory { get; set; }
        public bool ss_officers_directory { get; set; }
        public bool ss_committee_pages { get; set; }
        public bool issue_proforma { get; set; }
        public decimal rank { get; set; }
        public bool conf_fee_member { get; set; }
        public bool active { get; set; }
        public System.DateTime date_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public string excluded_committees { get; set; }
    }
}
