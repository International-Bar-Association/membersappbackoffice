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
    
    public partial class conf_speakerFunction_getByUid_Result
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public string title { get; set; }
        public decimal status_id { get; set; }
        public string status { get; set; }
        public decimal role_id { get; set; }
        public string role { get; set; }
        public decimal type_id { get; set; }
        public string type { get; set; }
        public bool paper_submitted { get; set; }
        public bool abstract_submitted { get; set; }
        public bool biography_submitted { get; set; }
        public bool keywords_submitted { get; set; }
        public bool license_to_publish { get; set; }
        public bool publish { get; set; }
        public decimal function_id { get; set; }
    }
}