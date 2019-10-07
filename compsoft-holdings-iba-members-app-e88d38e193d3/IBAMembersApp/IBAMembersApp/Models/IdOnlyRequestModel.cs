using System.ComponentModel.DataAnnotations;

namespace IBAMembersApp.Models
{
    public class IdOnlyRequestModel
    {
        [Required]
        public int? Id { get; set; }
    }
}
