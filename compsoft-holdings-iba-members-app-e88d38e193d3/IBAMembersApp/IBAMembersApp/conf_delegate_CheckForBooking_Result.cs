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
    
    public partial class conf_delegate_CheckForBooking_Result
    {
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public decimal record_id { get; set; }
        public Nullable<decimal> transferred_from_id { get; set; }
        public decimal conference_id { get; set; }
        public decimal type_id { get; set; }
        public decimal status_id { get; set; }
        public string badge_name { get; set; }
        public string badge_title { get; set; }
        public bool badge_title_ignore { get; set; }
        public bool visa { get; set; }
        public bool pay_at_conf { get; set; }
        public string conf_law_firm { get; set; }
        public string conf_address1 { get; set; }
        public string conf_address2 { get; set; }
        public string conf_address3 { get; set; }
        public decimal conf_city_id { get; set; }
        public string conf_county { get; set; }
        public decimal conf_state_id { get; set; }
        public string conf_pc_zip { get; set; }
        public decimal conf_country_id { get; set; }
        public string conf_email { get; set; }
        public string conf_alternative_email { get; set; }
        public string conf_telephone { get; set; }
        public string conf_fax { get; set; }
        public string conf_mobile { get; set; }
        public decimal hotel_id { get; set; }
        public string alternative_hotel { get; set; }
        public decimal fee_id { get; set; }
        public string fee_type { get; set; }
        public string notes { get; set; }
        public string speaker_expenses { get; set; }
        public string speaker_notes { get; set; }
        public bool speaker_to_pay { get; set; }
        public Nullable<System.DateTime> speaker_letter_sent { get; set; }
        public Nullable<System.DateTime> date_exported { get; set; }
        public Nullable<decimal> administrator_exported { get; set; }
        public System.DateTime date_registered { get; set; }
        public Nullable<System.DateTime> date_status_modified { get; set; }
        public System.DateTime date_created { get; set; }
        public decimal administrator_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<decimal> administrator_modified { get; set; }
        public bool non_iba_social { get; set; }
        public bool includeInLOP { get; set; }
        public Nullable<decimal> SpecialPrimaryContactDelegateId { get; set; }
        public bool SpecialPrimaryContact { get; set; }
        public Nullable<bool> VIPSpeaker { get; set; }
        public string badge_address { get; set; }
        public string badge_lawfirm { get; set; }
        public Nullable<int> PaidForByOrganisation { get; set; }
        public Nullable<int> AreYouRepresentingNonResidentOrganisationAtConference { get; set; }
        public string SpeakerBiography { get; set; }
        public string SpeakerAvRequirements { get; set; }
    }
}
