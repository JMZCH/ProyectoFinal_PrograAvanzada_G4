namespace AP.quejasymasquejas.Models.ViewModels
{
    /// <summary>
    /// ViewModel para mostrar errores
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
