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
    
    public partial class conf_conference
    {
        public conf_conference()
        {
            this.conf_administrator = new HashSet<conf_administrator>();
            this.conf_output = new HashSet<conf_output>();
            this.conf_conference_hotel = new HashSet<conf_conference_hotel>();
            this.conf_delegate = new HashSet<conf_delegate>();
            this.conf_fee_structure = new HashSet<conf_fee_structure>();
            this.conf_function = new HashSet<conf_function>();
            this.conf_unit = new HashSet<conf_unit>();
            this.conf_website_config = new HashSet<conf_website_config>();
            this.conf_website_content = new HashSet<conf_website_content>();
            this.conf_website_menu = new HashSet<conf_website_menu>();
        }
    
        public decimal id { get; set; }
        public System.Guid uid { get; set; }
        public string title { get; set; }
        public string venue { get; set; }
        public string city { get; set; }
        public decimal organiser_id { get; set; }
        public decimal country_id { get; set; }
        public decimal currency_id { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public bool provisional_dates { get; set; }
        public decimal type_id { get; set; }
        public string alternative_text { get; set; }
        public decimal status_id { get; set; }
        public decimal places { get; set; }
        public decimal warning_level { get; set; }
        public bool warning_sent { get; set; }
        public Nullable<System.DateTime> early_regfee_cutoff_date { get; set; }
        public Nullable<System.DateTime> late_regfee_cutoff_date { get; set; }
        public Nullable<System.DateTime> prelim_lop_cutoff_date { get; set; }
        public Nullable<System.DateTime> final_lop_cutoff_date { get; set; }
        public Nullable<System.DateTime> cancellation_date { get; set; }
        public decimal cancellation_rate { get; set; }
        public Nullable<System.DateTime> function_payment_date { get; set; }
        public string description { get; set; }
        public string who_should_attend { get; set; }
        public string fees_include { get; set; }
        public string directory_name { get; set; }
        public decimal vat_code_id { get; set; }
        public decimal nominal_code_id { get; set; }
        public string vat_reference { get; set; }
        public bool charge_fees { get; set; }
        public decimal sponsor_id { get; set; }
        public bool allow_subs { get; set; }
        public bool allow_guests { get; set; }
        public bool publish { get; set; }
        public decimal cpd_hours { get; set; }
        public decimal ethic_hours { get; set; }
        public System.DateTime date_created { get; set; }
        public decimal administrator_created { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<decimal> administrator_modified { get; set; }
        public bool suppress_bccemail { get; set; }
        public bool forceToMyIba { get; set; }
        public bool isVatNumberValid { get; set; }
        public bool isMobileAppSendAMessageActive { get; set; }
        public string AlternativeWebsiteLink { get; set; }
        public string MobileDirectoryName { get; set; }
        public string MobileShortName { get; set; }
        public bool DefaultMobileWebsite { get; set; }
        public string MobileSponsorWebsiteLink { get; set; }
        public int AMPMCutOff { get; set; }
        public Nullable<int> sponsorship_currency_id { get; set; }
        public int EveningCutOff { get; set; }
        public int LunchPeriod { get; set; }
        public bool RegistrationOpen { get; set; }
        public bool VisaRequiredDefaultFlag { get; set; }
        public string SpeakerRegistrationFormLink { get; set; }
        public string SpeakerInformationPageLink { get; set; }
        public string ChairRegistrationFormLink { get; set; }
        public string ChairLPDInformationPageLink { get; set; }
        public string ChairPPIDInformationPageLink { get; set; }
        public Nullable<System.DateTime> ChairSessionSetUpDate { get; set; }
        public int BreakfastStartTime { get; set; }
        public int DayStartTime { get; set; }
        public bool UseRooms { get; set; }
        public bool SchedulingOn { get; set; }
        public string website_type { get; set; }
        public string ConferenceAddress { get; set; }
        public string AccommodationLink { get; set; }
        public Nullable<System.DateTime> SpeakerMaterialDeadlineDate { get; set; }
        public bool SetupSessions { get; set; }
        public int OfficeAddressId { get; set; }
        public bool UseCustomVAT { get; set; }
    
        public virtual ICollection<conf_administrator> conf_administrator { get; set; }
        public virtual ICollection<conf_output> conf_output { get; set; }
        public virtual conf_status conf_status { get; set; }
        public virtual conf_type conf_type { get; set; }
        public virtual ICollection<conf_conference_hotel> conf_conference_hotel { get; set; }
        public virtual ICollection<conf_delegate> conf_delegate { get; set; }
        public virtual ICollection<conf_fee_structure> conf_fee_structure { get; set; }
        public virtual ICollection<conf_function> conf_function { get; set; }
        public virtual ICollection<conf_unit> conf_unit { get; set; }
        public virtual ICollection<conf_website_config> conf_website_config { get; set; }
        public virtual ICollection<conf_website_content> conf_website_content { get; set; }
        public virtual ICollection<conf_website_menu> conf_website_menu { get; set; }
    }
}
