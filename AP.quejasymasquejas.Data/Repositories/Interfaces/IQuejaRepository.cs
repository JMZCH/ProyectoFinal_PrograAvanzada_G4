using AP.quejasymasquejas.Models.Entities;

namespace AP.quejasymasquejas.Data.Repositories.Interfaces
{
    public interface IQuejaRepository : IRepository<Queja>
    {
        Task<IEnumerable<Queja>> GetAllWithUsuarioAsync();
        Task<Queja?> GetByIdWithUsuarioAsync(int id);

        Task<IEnumerable<Queja>> GetByUsuarioIdAsync(string usuarioId);

        Task<IEnumerable<Queja>> GetByEstadoAsync(string estado);

        Task<IEnumerable<Queja>> GetByCategoriaAsync(string categoria);

        Task<IEnumerable<Queja>> GetLatestAsync(int count);

        Task<(int Total, int Pendientes, int EnProceso, int Resueltas, int Cerradas)> GetEstadisticasAsync();
    }
}
