using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizationAPI.Models
{
    public class RoleUser
    {
        [Key]
        public int ID { get; protected set; }
        [ForeignKey("UsersId")]
        [Required]
        public int UsersId { get; set; }
    }
}
