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
    
    public partial class Error
    {
        public int id { get; set; }
        public System.Guid guid { get; set; }
        public string location { get; set; }
        public string message { get; set; }
        public string stackTrace { get; set; }
        public string resolution { get; set; }
        public System.DateTime createdOn { get; set; }
        public int createdById { get; set; }
        public int createdByTypeId { get; set; }
        public Nullable<System.DateTime> modifiedOn { get; set; }
        public Nullable<int> modifiedById { get; set; }
        public Nullable<int> modifiedByTypeId { get; set; }
    }
}
