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
    
    public partial class New_Country_Get_Result
    {
        public decimal id { get; set; }
        public System.Guid guid { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public decimal regionId { get; set; }
        public decimal postalZone { get; set; }
        public decimal currencyId { get; set; }
        public string dialCode { get; set; }
        public bool hasLowGdp { get; set; }
        public bool isEC { get; set; }
        public bool isUK { get; set; }
        public string vatFormat { get; set; }
        public decimal charityCommitteeId { get; set; }
        public bool isBarred { get; set; }
        public int OnlinePaymentTypeId { get; set; }
        public Nullable<decimal> postage { get; set; }
        public decimal vatId { get; set; }
        public bool doNotChargeVatToLocals { get; set; }
        public bool doNotChargeVatToEU { get; set; }
        public bool doNotChargeVatToNonEU { get; set; }
        public bool doNotChargeSponsorshipVatToLocals { get; set; }
        public bool doNotChargeSponsorshipVatToEU { get; set; }
        public bool doNotChargeSponsorshipVatToNonEU { get; set; }
        public System.DateTime createdOn { get; set; }
        public string createdOnFormatted { get; set; }
        public System.DateTime modifiedOn { get; set; }
        public string modifiedOnFormatted { get; set; }
    }
}
