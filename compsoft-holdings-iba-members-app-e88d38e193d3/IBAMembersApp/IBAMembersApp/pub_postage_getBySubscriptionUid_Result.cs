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
    
    public partial class pub_postage_getBySubscriptionUid_Result
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public Nullable<decimal> publication_id { get; set; }
        public Nullable<decimal> type_id { get; set; }
        public Nullable<decimal> uk { get; set; }
        public Nullable<decimal> world { get; set; }
        public Nullable<decimal> europe { get; set; }
        public bool discretionary_allow { get; set; }
        public System.DateTime date_created { get; set; }
        public decimal administrator_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<decimal> administrator_modified { get; set; }
    }
}
