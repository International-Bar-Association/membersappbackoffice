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
    
    public partial class conf_conference_hotel
    {
        public decimal conference_id { get; set; }
        public decimal hotel_id { get; set; }
        public bool venue { get; set; }
    
        public virtual conf_conference conf_conference { get; set; }
        public virtual conf_hotel conf_hotel { get; set; }
    }
}