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
    
    public partial class New_AtConferenceTickets_GetForTicketTransfer_Result
    {
        public int FunctionID { get; set; }
        public int Tickets { get; set; }
        public int TotalTicketsEscrow { get; set; }
        public int TotalTicketsReturned { get; set; }
        public int TotalTicketsReclaimed { get; set; }
        public int TotalTicketsSold { get; set; }
        public decimal TotalTicketsCurrent { get; set; }
        public decimal TotalComplimentaryTicketsCurrent { get; set; }
        public bool AllowGuests { get; set; }
    }
}