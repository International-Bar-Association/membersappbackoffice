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
    
    public partial class New_AtConferenceTicketsReturned_ListTicketsToBeSold_Result
    {
        public int ID { get; set; }
        public System.Guid Uid { get; set; }
        public int ReturnedAtConferenceBasketID { get; set; }
        public Nullable<int> ReclaimedAtConferenceBasketID { get; set; }
        public int ConferenceID { get; set; }
        public int MemberID { get; set; }
        public int DelegateID { get; set; }
        public int FunctionID { get; set; }
        public int FunctionTypeID { get; set; }
        public string FunctionType { get; set; }
        public string FunctionCode { get; set; }
        public string FunctionTitle { get; set; }
        public int Tickets { get; set; }
        public bool Complimentary { get; set; }
        public Nullable<int> TicketsReclaimed { get; set; }
        public Nullable<int> TicketsSold { get; set; }
        public int NominalCodeID { get; set; }
        public string NominalCode { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime DateModified { get; set; }
        public int ModifiedBy { get; set; }
        public bool Hide { get; set; }
    }
}
