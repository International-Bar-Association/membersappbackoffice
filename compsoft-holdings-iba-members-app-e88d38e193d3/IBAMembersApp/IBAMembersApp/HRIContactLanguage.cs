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
    
    public partial class HRIContactLanguage
    {
        public int ID { get; set; }
        public System.Guid Uid { get; set; }
        public int HRIContactID { get; set; }
        public string Language { get; set; }
        public string SpokenFluency { get; set; }
        public string WrittenFluency { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public System.DateTime DateModified { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool Hide { get; set; }
    
        public virtual HRIContact HRIContact { get; set; }
    }
}