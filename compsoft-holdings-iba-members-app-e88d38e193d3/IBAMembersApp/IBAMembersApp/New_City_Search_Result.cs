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
    
    public partial class New_City_Search_Result
    {
        public decimal id { get; set; }
        public System.Guid guid { get; set; }
        public string title { get; set; }
        public decimal stateId { get; set; }
        public string stateTitle { get; set; }
        public decimal countryId { get; set; }
        public string countryTitle { get; set; }
        public bool IsAdditional { get; set; }
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
