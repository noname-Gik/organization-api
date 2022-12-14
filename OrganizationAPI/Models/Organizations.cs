using System.ComponentModel.DataAnnotations;

namespace OrganizationAPI.Models
{
    public class Organizations
    {
        [Key]
        public int ID { get; protected set; }
        [Required]
        public string name { get; set; }
        public List<OrganizationRole> OrganizationRole { get; set; }
    }
}
