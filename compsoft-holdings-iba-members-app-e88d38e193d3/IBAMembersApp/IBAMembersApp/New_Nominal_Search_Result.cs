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
    
    public partial class New_Nominal_Search_Result
    {
        public decimal id { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public bool isPayment { get; set; }
        public bool isAdminCharge { get; set; }
        public bool isGoodwill { get; set; }
        public bool isWriteOn { get; set; }
        public bool isWriteOff { get; set; }
        public bool isRemainder { get; set; }
        public bool isMembership { get; set; }
        public bool isConference { get; set; }
        public bool isPublication { get; set; }
        public bool isMerchandise { get; set; }
        public bool isLibrary { get; set; }
        public bool isOther { get; set; }
        public bool isGeneral { get; set; }
        public bool isActive { get; set; }
        public System.DateTime createdOn { get; set; }
        public string createdOnFormatted { get; set; }
        public System.DateTime modifiedOn { get; set; }
        public string modifiedOnFormatted { get; set; }
        public Nullable<int> pageNumber { get; set; }
        public Nullable<int> resultsPerPage { get; set; }
        public Nullable<int> totalResults { get; set; }
        public Nullable<int> totalPages { get; set; }
    }
}