using System.ComponentModel.DataAnnotations;

namespace BeMyAngel.Persistance.Models
{
    public class RoleDto
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
    }
}
