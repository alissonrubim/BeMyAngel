using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
