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
    
    public partial class app_country_list_Result
    {
        public decimal id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public decimal region { get; set; }
        public bool low_gdp { get; set; }
        public bool ec { get; set; }
        public bool isUK { get; set; }
        public bool doNotChargeVatToLocals { get; set; }
        public bool doNotChargeVatToEU { get; set; }
        public bool doNotChargeVatToNonEU { get; set; }
        public bool barred { get; set; }
        public decimal vat_code_id { get; set; }
    }
}
