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
    
    public partial class HRIContactCategory
    {
        public int ID { get; set; }
        public System.Guid Uid { get; set; }
        public int HRIContactID { get; set; }
        public int HRICategoryID { get; set; }
        public string CategoryType { get; set; }
        public string ExpertiseType { get; set; }
        public string Title { get; set; }
        public string Response { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public System.DateTime DateModified { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool Hide { get; set; }
    
        public virtual HRICategory HRICategory { get; set; }
        public virtual HRIContact HRIContact { get; set; }
    }
}
