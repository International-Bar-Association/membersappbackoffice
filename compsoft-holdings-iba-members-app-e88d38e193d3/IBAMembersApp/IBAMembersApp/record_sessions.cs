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
    
    public partial class record_sessions
    {
        public int Id { get; set; }
        public decimal record_id { get; set; }
        public System.Guid session_token { get; set; }
        public System.DateTime session_expiry { get; set; }
        public Nullable<System.DateTime> logout_time { get; set; }
        public string client_platform { get; set; }
        public System.DateTime initial_logon { get; set; }
    
        public virtual C_records C_records { get; set; }
    }
}
