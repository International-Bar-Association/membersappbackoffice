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
    
    public partial class New_PaymentType_Get_Result
    {
        public decimal id { get; set; }
        public decimal bankId { get; set; }
        public decimal currencyId { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public bool isCreditCardPayment { get; set; }
        public bool isSelfService { get; set; }
        public bool isReceiptInvoice { get; set; }
        public bool isReceiptMOA { get; set; }
        public bool isRefundMOA { get; set; }
        public bool isWriteOff { get; set; }
        public bool isAlreadyBanked { get; set; }
        public bool keepSeparatePostings { get; set; }
        public bool isActive { get; set; }
        public System.DateTime createdOn { get; set; }
        public string createdOnFormatted { get; set; }
        public System.DateTime modifiedOn { get; set; }
        public string modifiedOnFormatted { get; set; }
    }
}
