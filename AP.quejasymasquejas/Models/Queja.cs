using System.ComponentModel.DataAnnotations;

namespace AP.quejasymasquejas.Models
{
    public class Queja
    {
        [Key]
        public int QuejaID { get; set; }

        [Required]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Pendiente";
        public string Prioridad { get; set; } = "Normal";
        public string Categoria { get; set; } = "General";
    }
}
