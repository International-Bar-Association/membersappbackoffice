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
    
    public partial class f_cfc_payment_getInvoice_Result
    {
        public decimal id { get; set; }
        public decimal total_net { get; set; }
        public decimal total_vat { get; set; }
        public System.DateTime date_created { get; set; }
        public Nullable<System.DateTime> membership_year { get; set; }
        public Nullable<decimal> conference_id { get; set; }
        public Nullable<int> DelegateID { get; set; }
        public Nullable<decimal> order_id { get; set; }
        public string description { get; set; }
        public decimal gbp_exchange_rate { get; set; }
        public decimal invoice_type { get; set; }
        public string vat_number { get; set; }
        public decimal record { get; set; }
        public string status { get; set; }
        public decimal currency { get; set; }
        public string symbol { get; set; }
        public string currencyCode { get; set; }
        public string code { get; set; }
        public string originatorsCode { get; set; }
    }
}