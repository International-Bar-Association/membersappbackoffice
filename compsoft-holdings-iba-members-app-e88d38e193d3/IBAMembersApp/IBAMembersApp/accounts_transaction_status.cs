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
    
    public partial class accounts_transaction_status
    {
        public decimal id { get; set; }
        public string description { get; set; }
        public bool header { get; set; }
        public bool line { get; set; }
        public bool payment_data { get; set; }
        public System.DateTime date_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
    }
}
