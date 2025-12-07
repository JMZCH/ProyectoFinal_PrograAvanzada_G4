using AP.quejasymasquejas.Data.Context;
using AP.quejasymasquejas.Data.Repositories.Interfaces;
using AP.quejasymasquejas.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AP.quejasymasquejas.Data.Repositories.Implementations
{
    public class QuejaRepository : Repository<Queja>, IQuejaRepository
    {
        public QuejaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Queja>> GetAllWithUsuarioAsync()
        {
            return await _dbSet
                .Include(q => q.Usuario)
                .OrderByDescending(q => q.FechaRegistro)
                .ToListAsync();
        }

        public async Task<Queja?> GetByIdWithUsuarioAsync(int id)
        {
            return await _dbSet
                .Include(q => q.Usuario)
                .FirstOrDefaultAsync(q => q.QuejaId == id);
        }

        public async Task<IEnumerable<Queja>> GetByUsuarioIdAsync(string usuarioId)
        {
            return await _dbSet
                .Include(q => q.Usuario)
                .Where(q => q.UsuarioId == usuarioId)
                .OrderByDescending(q => q.FechaRegistro)
                .ToListAsync();
        }

        public async Task<IEnumerable<Queja>> GetByEstadoAsync(string estado)
        {
            return await _dbSet
                .Include(q => q.Usuario)
                .Where(q => q.Estado == estado)
                .OrderByDescending(q => q.FechaRegistro)
                .ToListAsync();
        }

        public async Task<IEnumerable<Queja>> GetByCategoriaAsync(string categoria)
        {
            return await _dbSet
                .Include(q => q.Usuario)
                .Where(q => q.Categoria == categoria)
                .OrderByDescending(q => q.FechaRegistro)
                .ToListAsync();
        }

        public async Task<IEnumerable<Queja>> GetLatestAsync(int count)
        {
            return await _dbSet
                .Include(q => q.Usuario)
                .OrderByDescending(q => q.FechaRegistro)
                .Take(count)
                .ToListAsync();
        }

        public async Task<(int Total, int Pendientes, int EnProceso, int Resueltas, int Cerradas)> GetEstadisticasAsync()
        {
            var total = await _dbSet.CountAsync();
            var pendientes = await _dbSet.CountAsync(q => q.Estado == "Pendiente");
            var enProceso = await _dbSet.CountAsync(q => q.Estado == "En Proceso");
            var resueltas = await _dbSet.CountAsync(q => q.Estado == "Resuelto");
            var cerradas = await _dbSet.CountAsync(q => q.Estado == "Cerrado");

            return (total, pendientes, enProceso, resueltas, cerradas);
        }
    }
}
