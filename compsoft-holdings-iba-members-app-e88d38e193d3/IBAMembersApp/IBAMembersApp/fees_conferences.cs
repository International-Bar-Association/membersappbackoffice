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
    
    public partial class fees_conferences
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public decimal conference { get; set; }
        public string description { get; set; }
        public decimal record_type { get; set; }
        public decimal discount_type { get; set; }
        public decimal country { get; set; }
        public Nullable<System.DateTime> earlybird_date { get; set; }
        public Nullable<decimal> earlybird_cost { get; set; }
        public Nullable<System.DateTime> second_date { get; set; }
        public Nullable<decimal> second_cost { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<decimal> start_cost { get; set; }
        public decimal vat_code { get; set; }
        public decimal nominal_code { get; set; }
        public bool guest_cost { get; set; }
        public bool publish { get; set; }
        public System.DateTime date_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
    }
}
