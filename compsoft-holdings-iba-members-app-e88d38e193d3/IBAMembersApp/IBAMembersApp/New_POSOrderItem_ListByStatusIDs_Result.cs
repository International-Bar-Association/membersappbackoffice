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
    
    public partial class New_POSOrderItem_ListByStatusIDs_Result
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public decimal invoice_type_id { get; set; }
        public decimal product { get; set; }
        public string description { get; set; }
        public string code { get; set; }
        public Nullable<System.DateTime> FunctionEnd { get; set; }
        public string FunctionStartDay { get; set; }
        public string FunctionFormattedTime { get; set; }
        public decimal currency_id { get; set; }
        public decimal quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal item_net { get; set; }
        public decimal item_vat { get; set; }
        public decimal vat_code_id { get; set; }
        public decimal vat_percentage { get; set; }
        public string vat_description { get; set; }
        public decimal nominal_id { get; set; }
        public string nominal_code { get; set; }
        public System.DateTime date_ordered { get; set; }
        public string currency_symbol { get; set; }
        public bool complimentary { get; set; }
        public int TotalTicketsTransferred { get; set; }
    }
}
