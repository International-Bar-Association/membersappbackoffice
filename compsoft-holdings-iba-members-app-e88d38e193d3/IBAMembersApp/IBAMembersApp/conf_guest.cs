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
    
    public partial class conf_guest
    {
        public conf_guest()
        {
            this.conf_delegate_guest = new HashSet<conf_delegate_guest>();
        }
    
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public decimal record_id { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string badge_name { get; set; }
        public string dietary_requirements { get; set; }
        public decimal country_id { get; set; }
        public System.DateTime date_created { get; set; }
        public decimal administrator_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<decimal> administrator_modified { get; set; }
        public bool publish { get; set; }
        public bool HasAllergens { get; set; }
        public string Allergens { get; set; }
    
        public virtual C_records C_records { get; set; }
        public virtual ICollection<conf_delegate_guest> conf_delegate_guest { get; set; }
    }
}
