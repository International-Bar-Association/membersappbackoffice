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
    
    public partial class New_AtConferenceTicketsReturned_ListForDelegate_Result
    {
        public int ID { get; set; }
        public System.Guid Uid { get; set; }
        public int ConferenceID { get; set; }
        public int MemberID { get; set; }
        public int DelegateID { get; set; }
        public int FunctionID { get; set; }
        public int FunctionTypeID { get; set; }
        public string FunctionType { get; set; }
        public string FunctionCode { get; set; }
        public string FunctionTitle { get; set; }
        public Nullable<int> TotalTickets { get; set; }
        public bool Complimentary { get; set; }
        public Nullable<int> TicketsReclaimed { get; set; }
        public Nullable<int> TicketsSold { get; set; }
        public int NominalCodeID { get; set; }
        public string NominalCode { get; set; }
        public bool Hide { get; set; }
        public bool Published { get; set; }
    }
}
