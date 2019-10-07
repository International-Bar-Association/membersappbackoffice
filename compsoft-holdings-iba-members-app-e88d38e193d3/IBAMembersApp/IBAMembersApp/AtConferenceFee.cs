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
    
    public partial class AtConferenceFee
    {
        public int ID { get; set; }
        public System.Guid Uid { get; set; }
        public int ConferenceID { get; set; }
        public int FeeID { get; set; }
        public string FeeCode { get; set; }
        public string FeeTitle { get; set; }
        public int FeeTypeID { get; set; }
        public decimal GBPNet { get; set; }
        public decimal GBPVat { get; set; }
        public decimal USDNet { get; set; }
        public decimal USDVat { get; set; }
        public decimal EURNet { get; set; }
        public decimal EURVat { get; set; }
        public Nullable<decimal> CCYNet { get; set; }
        public Nullable<decimal> CCYVat { get; set; }
        public int NominalCodeID { get; set; }
        public string NominalCode { get; set; }
        public bool Published { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime DateModified { get; set; }
        public int ModifiedBy { get; set; }
        public bool Hide { get; set; }
        public Nullable<int> DailyFeeNumberOfDays { get; set; }
    }
}
