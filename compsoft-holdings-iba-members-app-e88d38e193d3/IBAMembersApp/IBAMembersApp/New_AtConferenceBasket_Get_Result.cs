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
    
    public partial class New_AtConferenceBasket_Get_Result
    {
        public int ID { get; set; }
        public System.Guid Guid { get; set; }
        public int ConferenceID { get; set; }
        public int MemberID { get; set; }
        public int DelegateID { get; set; }
        public int InvoiceID { get; set; }
        public Nullable<int> ReceiptID { get; set; }
        public string Title { get; set; }
        public string PaymentType { get; set; }
        public int CurrencyID { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal TotalNet { get; set; }
        public Nullable<decimal> TotalVat { get; set; }
        public decimal GrandTotal { get; set; }
        public string LawFirm { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string PC_ZIP { get; set; }
        public string Country { get; set; }
        public System.DateTime ConfirmationDate { get; set; }
        public int ConfirmationPrints { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int AdministratorCreated { get; set; }
        public string AdministratorCreatedInitials { get; set; }
    }
}
