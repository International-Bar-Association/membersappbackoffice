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
    
    public partial class conf_delegate_guest
    {
        public decimal delegate_id { get; set; }
        public decimal guest_id { get; set; }
        public Nullable<decimal> fee_id { get; set; }
        public string fee_type { get; set; }
        public bool complimentary { get; set; }
    
        public virtual conf_delegate conf_delegate { get; set; }
        public virtual conf_guest conf_guest { get; set; }
    }
}