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
    
    public partial class accounts_currency
    {
        public decimal id { get; set; }
        public string symbol { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public decimal gbp_exchange_rate { get; set; }
        public decimal gbp_spot_rate_upper_limit { get; set; }
        public decimal gbp_spot_rate { get; set; }
        public decimal gbp_spot_rate_lower_limit { get; set; }
        public System.DateTime gbp_spot_rate_date { get; set; }
        public string servebase_merchant { get; set; }
        public string servebase_login { get; set; }
        public string servebase_pwd { get; set; }
        public bool active { get; set; }
        public System.DateTime date_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public long SAGE200_Currency_Code { get; set; }
    }
}
