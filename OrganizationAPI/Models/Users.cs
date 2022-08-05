using System.ComponentModel.DataAnnotations;

namespace OrganizationAPI.Models
{
    public class Users
    {
        [Key]
        public int ID { get; protected set; }
        [Required]
        public string fullname { get; set; }
        // дополнительная информация о пользователе
    }
}
