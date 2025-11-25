using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AP.quejasymasquejas.Models
{
    public class Queja
    {
        [Key]
        public int QuejaID { get; set; }

        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es requerida")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = string.Empty;

        [Display(Name = "Fecha de Registro")]
        [DataType(DataType.DateTime)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Pendiente";

        [Display(Name = "Prioridad")]
        public string Prioridad { get; set; } = "Normal";

        [Display(Name = "Categoría")]
        public string Categoria { get; set; } = "General";

        // Relación con Identity User
        public string? UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual IdentityUser? Usuario { get; set; }
    }
}
