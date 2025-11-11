using System.ComponentModel.DataAnnotations;

namespace AP.quejasymasquejas.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Contraseña { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<Queja> Quejas { get; set; } = new List<Queja>();
    }
}
