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
    
    public partial class AtConferenceBasketItem
    {
        public int ID { get; set; }
        public System.Guid Guid { get; set; }
        public int BasketID { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int CurrencyID { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public int Quantity { get; set; }
        public int Complimentary { get; set; }
        public decimal Net { get; set; }
        public decimal Vat { get; set; }
        public decimal Total { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int AdministratorCreated { get; set; }
        public string AdministratorCreatedInitials { get; set; }
    }
}
