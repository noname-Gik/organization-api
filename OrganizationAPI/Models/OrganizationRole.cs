using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizationAPI.Models
{
    public class OrganizationRole
    {
        [Key]
        public int ID { get; protected set; }
        [ForeignKey("RolesId")]
        [Required]
        public int RolesId { get; set; }
        public List<RoleUser> RoleUser { get; set; }
    }
}
