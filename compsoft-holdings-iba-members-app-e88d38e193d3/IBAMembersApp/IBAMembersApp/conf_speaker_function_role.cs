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
    
    public partial class conf_speaker_function_role
    {
        public conf_speaker_function_role()
        {
            this.conf_speaker_function = new HashSet<conf_speaker_function>();
        }
    
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public string title { get; set; }
        public bool isChair { get; set; }
        public System.DateTime dateCreated { get; set; }
        public int administratorCreated { get; set; }
        public Nullable<System.DateTime> dateModified { get; set; }
        public Nullable<int> administratorModified { get; set; }
        public int sequence { get; set; }
    
        public virtual ICollection<conf_speaker_function> conf_speaker_function { get; set; }
    }
}
