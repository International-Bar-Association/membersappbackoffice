using System;
using System.ComponentModel.DataAnnotations;

namespace IBAMembersApp.BusinessLayer.Models
{
    public class ExportByDateParametersModel
    {
        [Display(Name = "Date From")]
        [DataType(DataType.Date)]
        public DateTime? From { get; set; }

        [Display(Name = "Date To")]
        [DataType(DataType.Date)]
        public DateTime? To { get; set; }
    }
}