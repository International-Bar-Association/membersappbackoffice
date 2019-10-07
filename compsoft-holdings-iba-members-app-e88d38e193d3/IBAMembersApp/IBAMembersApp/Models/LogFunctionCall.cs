namespace IBAMembersApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LogFunctionCall")]
    public partial class LogFunctionCall
    {
        public int id { get; set; }

        [Column("class")]
        [Required]
        [StringLength(256)]
        public string _class { get; set; }

        [Required]
        [StringLength(256)]
        public string function { get; set; }

        public DateTime createdOn { get; set; }
    }
}
