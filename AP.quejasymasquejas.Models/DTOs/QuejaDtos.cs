using System.ComponentModel.DataAnnotations;

namespace AP.quejasymasquejas.Models.DTOs
{
    /// <summary>
    /// DTO para crear una nueva queja
    /// </summary>
    public class QuejaCreateDto
    {
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es requerida")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = string.Empty;

        [Display(Name = "Prioridad")]
        public string Prioridad { get; set; } = "Normal";

        [Display(Name = "Categoría")]
        public string Categoria { get; set; } = "General";
    }

    /// <summary>
    /// DTO para editar una queja existente
    /// </summary>
    public class QuejaEditDto
    {
        public int QuejaId { get; set; }

        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es requerida")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = string.Empty;

        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Pendiente";

        [Display(Name = "Prioridad")]
        public string Prioridad { get; set; } = "Normal";

        [Display(Name = "Categoría")]
        public string Categoria { get; set; } = "General";
    }

    /// <summary>
    /// DTO para mostrar una queja en listados
    /// </summary>
    public class QuejaListDto
    {
        public int QuejaId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Prioridad { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string? UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = "Anónimo";
    }

    /// <summary>
    /// DTO para mostrar detalles completos de una queja
    /// </summary>
    public class QuejaDetailDto : QuejaListDto
    {
        public string DescripcionCompleta { get; set; } = string.Empty;
    }
}
