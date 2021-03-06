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
    
    public partial class conf_function
    {
        public conf_function()
        {
            this.conf_delegate_function = new HashSet<conf_delegate_function>();
            this.conf_delegate_meeting = new HashSet<conf_delegate_meeting>();
            this.conf_function_av_requirement = new HashSet<conf_function_av_requirement>();
            this.conf_function_unit = new HashSet<conf_function_unit>();
            this.conf_speaker_function = new HashSet<conf_speaker_function>();
        }
    
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public decimal conference_id { get; set; }
        public string title { get; set; }
        public string code_A { get; set; }
        public string code_B { get; set; }
        public string code_C { get; set; }
        public string location { get; set; }
        public System.DateTime function_start { get; set; }
        public System.DateTime function_end { get; set; }
        public decimal type_id { get; set; }
        public decimal status_id { get; set; }
        public decimal vat_code_id { get; set; }
        public decimal nominal_code_id { get; set; }
        public decimal fee { get; set; }
        public decimal places { get; set; }
        public decimal warning_level { get; set; }
        public bool warning_sent { get; set; }
        public decimal roomsetup_id { get; set; }
        public string description { get; set; }
        public string notes { get; set; }
        public bool showcase { get; set; }
        public bool allow_guests { get; set; }
        public string transport { get; set; }
        public string dress_code { get; set; }
        public bool publish { get; set; }
        public System.DateTime date_created { get; set; }
        public decimal administrator_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<decimal> administrator_modified { get; set; }
        public bool DisplayLocation { get; set; }
        public Nullable<decimal> fee_vat { get; set; }
        public Nullable<decimal> fee_gross { get; set; }
        public string Address { get; set; }
        public Nullable<int> sessiontype_id { get; set; }
        public Nullable<int> SetupById { get; set; }
        public Nullable<int> PATHSessionId { get; set; }
    
        public virtual conf_conference conf_conference { get; set; }
        public virtual ICollection<conf_delegate_function> conf_delegate_function { get; set; }
        public virtual ICollection<conf_delegate_meeting> conf_delegate_meeting { get; set; }
        public virtual ICollection<conf_function_av_requirement> conf_function_av_requirement { get; set; }
        public virtual conf_roomsetup conf_roomsetup { get; set; }
        public virtual conf_function_status conf_function_status { get; set; }
        public virtual conf_function_type conf_function_type { get; set; }
        public virtual ICollection<conf_function_unit> conf_function_unit { get; set; }
        public virtual ICollection<conf_speaker_function> conf_speaker_function { get; set; }
    }
}
