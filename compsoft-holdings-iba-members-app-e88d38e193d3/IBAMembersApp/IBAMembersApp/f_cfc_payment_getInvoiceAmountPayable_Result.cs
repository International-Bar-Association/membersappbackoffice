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
    
    public partial class f_cfc_payment_getInvoiceAmountPayable_Result
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public decimal invoice { get; set; }
        public decimal credit_invoice { get; set; }
        public decimal product { get; set; }
        public decimal transaction_type { get; set; }
        public decimal invoice_type { get; set; }
        public decimal allocation_status { get; set; }
        public string description { get; set; }
        public decimal invoice_currency { get; set; }
        public decimal quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal item_net { get; set; }
        public decimal item_vat { get; set; }
        public decimal vat_code { get; set; }
        public decimal vat_percentage { get; set; }
        public string vat_description { get; set; }
        public decimal nominal { get; set; }
        public string nominal_code { get; set; }
        public Nullable<decimal> item_total { get; set; }
        public Nullable<decimal> item_vat_paid { get; set; }
        public string allocation { get; set; }
    }
}