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
    
    public partial class accounts_payment_types
    {
        public decimal id { get; set; }
        public decimal bank { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public bool cc_payment { get; set; }
        public bool self_service { get; set; }
        public bool receipt_invoice { get; set; }
        public bool receipt_moa { get; set; }
        public bool refund_moa { get; set; }
        public bool active { get; set; }
        public System.DateTime date_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public decimal currencyId { get; set; }
        public bool isWriteOff { get; set; }
        public bool alreadyBanked { get; set; }
        public bool keep_separate_postings { get; set; }
    }
}