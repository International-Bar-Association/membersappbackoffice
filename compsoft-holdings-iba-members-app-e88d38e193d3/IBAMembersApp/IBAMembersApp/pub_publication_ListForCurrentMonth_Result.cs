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
    
    public partial class pub_publication_ListForCurrentMonth_Result
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public string title { get; set; }
        public int type_id { get; set; }
        public decimal status_id { get; set; }
        public string category_id_title { get; set; }
        public string category_folder { get; set; }
        public string image_folder { get; set; }
        public string image_name { get; set; }
        public Nullable<System.DateTime> date_published { get; set; }
        public string code { get; set; }
        public bool is_hard_copy { get; set; }
        public string contributors { get; set; }
        public string currency_symbol { get; set; }
        public Nullable<decimal> non_member_price { get; set; }
        public Nullable<decimal> member_price { get; set; }
        public Nullable<decimal> stock { get; set; }
        public string no_stock_message { get; set; }
    }
}