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
    
    public partial class pub_subscription_getByUid_Result
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public decimal status_id { get; set; }
        public string status_title { get; set; }
        public decimal category_id { get; set; }
        public string category_title { get; set; }
        public Nullable<decimal> size_id { get; set; }
        public decimal format_id { get; set; }
        public Nullable<decimal> abbreviation_id { get; set; }
        public Nullable<decimal> stock_id { get; set; }
        public decimal currency_id { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string date_published { get; set; }
        public Nullable<System.DateTime> date_published_noformat { get; set; }
        public bool is_restricted { get; set; }
        public string issn { get; set; }
        public Nullable<bool> is_myiba { get; set; }
        public string volume { get; set; }
        public string subscription_start { get; set; }
        public string subscription_end { get; set; }
        public string url_further_details { get; set; }
        public string ContentURL { get; set; }
        public string notes { get; set; }
        public string file_name { get; set; }
        public string file_type { get; set; }
        public Nullable<decimal> file_size { get; set; }
        public string file_mime_type { get; set; }
        public string image_name { get; set; }
        public string image_type { get; set; }
        public Nullable<decimal> image_size { get; set; }
        public string image_mime_type { get; set; }
        public Nullable<decimal> issues_per_period { get; set; }
        public decimal latest_issue_number { get; set; }
        public System.DateTime date_created { get; set; }
        public decimal administrator_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<decimal> administrator_modified { get; set; }
        public string contributors { get; set; }
        public string currency_symbol { get; set; }
        public Nullable<decimal> non_member_price { get; set; }
        public Nullable<decimal> member_price { get; set; }
        public Nullable<decimal> unit_price { get; set; }
        public Nullable<decimal> delegate_price { get; set; }
        public Nullable<bool> price_discretionary_allowed { get; set; }
        public Nullable<decimal> vat_percentage { get; set; }
        public string promotion_code { get; set; }
        public Nullable<decimal> promotion_price { get; set; }
        public decimal postage_uk { get; set; }
        public decimal postage_europe { get; set; }
        public decimal postage_world { get; set; }
        public Nullable<bool> postage_discretionary_allowed { get; set; }
        public int IsDonation { get; set; }
    }
}
