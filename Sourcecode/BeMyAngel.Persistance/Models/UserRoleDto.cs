using System.ComponentModel.DataAnnotations;

namespace BeMyAngel.Persistance.Models
{
    public class UserRoleDto
    {
        [Key]
        public int UserId { get; set; }
        [Key]
        public int RoleId { get; set; }
    }
}
