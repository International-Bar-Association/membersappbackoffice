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
    
    public partial class accounts_receipts_get_Result
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public decimal record { get; set; }
        public decimal invoice { get; set; }
        public decimal transaction_type { get; set; }
        public string description { get; set; }
        public decimal invoice_type { get; set; }
        public decimal allocation_status { get; set; }
        public decimal payment_type { get; set; }
        public decimal bank { get; set; }
        public decimal receipt_currency { get; set; }
        public decimal amount_receipt_currency { get; set; }
        public decimal bankcharges_receipt_currency { get; set; }
        public decimal difference_receipt_currency { get; set; }
        public decimal exchange_rate { get; set; }
        public decimal exchange_diff_gbp { get; set; }
        public decimal difference_gbp { get; set; }
        public decimal reconciliation { get; set; }
        public string st_reference { get; set; }
        public string st_authcode { get; set; }
        public Nullable<decimal> payment_expiry_month_cc { get; set; }
        public Nullable<decimal> payment_expiry_year_cc { get; set; }
        public Nullable<decimal> servebase_epac { get; set; }
        public string servebase_esta { get; set; }
        public string servebase_eaut { get; set; }
        public string servebase_eeam_authans { get; set; }
        public string servebase_ecod { get; set; }
        public string servebase_eeam_confans { get; set; }
        public System.DateTime date_transaction { get; set; }
        public System.DateTime date_created { get; set; }
        public decimal administrator_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<decimal> administrator_modified { get; set; }
        public string TransactionTypeCode { get; set; }
    }
}
