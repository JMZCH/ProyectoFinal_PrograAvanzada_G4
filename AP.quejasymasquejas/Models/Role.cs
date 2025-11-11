using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.quejasymasquejas.web.Models
{
    [Table("Roles", Schema = "dbo")]
    public class Role
    {
        [Key]
        [Column("RoleId")]
        public int RoleId { get; set; }                    // ❌ SIN MaxLength

        [Required]
        [Column("RoleName")]
        public string RoleName { get; set; } = string.Empty;

        [Column("Description")]
        public string? Description { get; set; }
    }
}
