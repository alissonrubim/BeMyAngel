using System.ComponentModel.DataAnnotations;

namespace BeMyAngel.Persistance.Models
{
    public class SettingDto
    {
        [Key]
        public int SettingId { get; set; }
        public string Identifier { get; set; }
        public string Value { get; set; }
    }
}
