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
    
    public partial class pub_stock_getBySubscriptionUid_Result
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public decimal id1 { get; set; }
        public Nullable<decimal> original_quantity { get; set; }
        public Nullable<decimal> current_quantity { get; set; }
        public Nullable<decimal> sold_quantity { get; set; }
        public Nullable<decimal> web_quantity { get; set; }
        public Nullable<decimal> low_warn { get; set; }
        public string location { get; set; }
        public string supplier_title { get; set; }
        public string no_stock_message { get; set; }
        public string date_stock_adjustment { get; set; }
        public System.DateTime date_created { get; set; }
        public decimal administrator_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<decimal> administrator_modified { get; set; }
    }
}
