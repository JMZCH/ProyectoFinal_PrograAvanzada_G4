namespace AP.quejasymasquejas.Models.Enums
{
    /// <summary>
    /// Estados posibles de una queja
    /// </summary>
    public enum EstadoQueja
    {
        Pendiente,
        EnProceso,
        Resuelto,
        Cerrado
    }

    /// <summary>
    /// Niveles de prioridad de una queja
    /// </summary>
    public enum PrioridadQueja
    {
        Baja,
        Normal,
        Alta,
        Urgente
    }

    /// <summary>
    /// Categorías disponibles para clasificar quejas
    /// </summary>
    public enum CategoriaQueja
    {
        General,
        Servicio,
        Producto,
        Atencion,
        Infraestructura,
        Otro
    }
}
