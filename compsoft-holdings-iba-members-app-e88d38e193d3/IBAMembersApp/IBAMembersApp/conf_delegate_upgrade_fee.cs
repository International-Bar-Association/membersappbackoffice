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
    
    public partial class conf_delegate_upgrade_fee
    {
        public int id { get; set; }
        public int conference_id { get; set; }
        public int delegate_id { get; set; }
        public int daily_fee_id { get; set; }
        public string daily_fee_type { get; set; }
        public int number_of_days { get; set; }
        public decimal cost_net { get; set; }
        public decimal cost_vat { get; set; }
        public decimal paid_net { get; set; }
        public decimal paid_vat { get; set; }
        public int paid_currency_id { get; set; }
        public bool complimentary { get; set; }
    }
}
