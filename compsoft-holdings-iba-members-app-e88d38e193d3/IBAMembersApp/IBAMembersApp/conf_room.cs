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
    
    public partial class conf_room
    {
        public int id { get; set; }
        public System.Guid uid { get; set; }
        public int conference_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public Nullable<int> capacity { get; set; }
        public decimal percentage_tolerance { get; set; }
        public int roomsetup_id { get; set; }
        public int sessiontype_id { get; set; }
        public System.DateTime date_created { get; set; }
        public int administrator_created { get; set; }
        public System.DateTime date_modified { get; set; }
        public int administrator_modified { get; set; }
    }
}