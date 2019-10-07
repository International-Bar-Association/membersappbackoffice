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
    
    public partial class HRITravelInsuranceDetailsIncoming
    {
        public int ID { get; set; }
        public System.Guid Uid { get; set; }
        public int HRIContactIncomingID { get; set; }
        public string PassportGivenName { get; set; }
        public string PassportFamilyName { get; set; }
        public string PassportNumber { get; set; }
        public System.DateTime PassportDateOfIssue { get; set; }
        public System.DateTime PassportExpiryDate { get; set; }
        public string PassportCountryOfIssue { get; set; }
        public string PassportPlaceOfBirth { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public Nullable<System.DateTime> TravalDateFrom { get; set; }
        public Nullable<System.DateTime> TravelDateTo { get; set; }
        public string BestContactNumber { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinAddress { get; set; }
        public string NextOfKinTelephone { get; set; }
        public string NextOfKinRelationship { get; set; }
        public string PreferredAirportDeparture { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public System.DateTime DateModified { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string PassportNationality { get; set; }
        public string BestContactNumberCountryDialCode { get; set; }
        public string NextOfKinTelephoneCountryDialCode { get; set; }
    
        public virtual HRIContactIncoming HRIContactIncoming { get; set; }
    }
}
