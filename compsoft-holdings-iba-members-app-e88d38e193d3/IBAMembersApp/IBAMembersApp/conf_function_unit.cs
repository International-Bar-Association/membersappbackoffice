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
    
    public partial class conf_function_unit
    {
        public decimal function_id { get; set; }
        public decimal unit_id { get; set; }
        public decimal type_id { get; set; }
        public bool lead { get; set; }
    
        public virtual conf_function conf_function { get; set; }
    }
}
