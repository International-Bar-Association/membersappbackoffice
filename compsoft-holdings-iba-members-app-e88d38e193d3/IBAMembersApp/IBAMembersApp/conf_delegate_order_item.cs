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
    using System.Collections.Generic;
    
    public partial class conf_delegate_order_item
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public decimal delegate_id { get; set; }
        public decimal invoice_type_id { get; set; }
        public decimal transaction_status_id { get; set; }
        public decimal product { get; set; }
        public string description { get; set; }
        public decimal currency_id { get; set; }
        public decimal quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal item_net { get; set; }
        public decimal item_vat { get; set; }
        public decimal vat_code_id { get; set; }
        public decimal vat_percentage { get; set; }
        public string vat_description { get; set; }
        public decimal nominal_id { get; set; }
        public string nominal_code { get; set; }
        public bool complimentary { get; set; }
        public string type { get; set; }
        public System.DateTime date_ordered { get; set; }
        public System.DateTime date_created { get; set; }
        public decimal administrator_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<decimal> administrator_modified { get; set; }
        public decimal item_net_paid { get; set; }
        public decimal item_vat_paid { get; set; }
        public Nullable<decimal> unit_vat { get; set; }
        public int DelegateSponsorQuantity { get; set; }
        public int ExhibitorQuantity { get; set; }
        public int FreeSocialFunctionQuantity { get; set; }
    }
}