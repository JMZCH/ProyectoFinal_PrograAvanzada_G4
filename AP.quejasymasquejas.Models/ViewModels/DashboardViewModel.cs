namespace AP.quejasymasquejas.Models.ViewModels
{
    /// <summary>
    /// ViewModel para mostrar estadísticas en el dashboard
    /// </summary>
    public class DashboardViewModel
    {
        public int TotalQuejas { get; set; }
        public int QuejasPendientes { get; set; }
        public int QuejasEnProceso { get; set; }
        public int QuejasResueltas { get; set; }
        public int QuejasCerradas { get; set; }
        public List<QuejaResumenViewModel> UltimasQuejas { get; set; } = new();
    }

    /// <summary>
    /// ViewModel para resumen de queja en listados
    /// </summary>
    public class QuejaResumenViewModel
    {
        public int QuejaId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string DescripcionCorta { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Prioridad { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string NombreAutor { get; set; } = "Anónimo";
        public string? UsuarioId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string TiempoTranscurrido { get; set; } = string.Empty;
    }
}
