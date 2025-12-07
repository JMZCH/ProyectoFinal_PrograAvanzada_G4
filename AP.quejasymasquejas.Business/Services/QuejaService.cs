using AP.quejasymasquejas.Business.Interfaces;
using AP.quejasymasquejas.Data.Repositories.Interfaces;
using AP.quejasymasquejas.Models.DTOs;
using AP.quejasymasquejas.Models.Entities;
using AP.quejasymasquejas.Models.ViewModels;

namespace AP.quejasymasquejas.Business.Services
{
    public class QuejaService : IQuejaService
    {
        private readonly IQuejaRepository _quejaRepository;

        public QuejaService(IQuejaRepository quejaRepository)
        {
            _quejaRepository = quejaRepository;
        }
        public async Task<IEnumerable<QuejaListDto>> GetAllQuejasAsync()
        {
            var quejas = await _quejaRepository.GetAllWithUsuarioAsync();
            return quejas.Select(MapToListDto);
        }
        public async Task<QuejaDetailDto?> GetQuejaByIdAsync(int id)
        {
            var queja = await _quejaRepository.GetByIdWithUsuarioAsync(id);
            if (queja == null) return null;

            return new QuejaDetailDto
            {
                QuejaId = queja.QuejaId,
                Titulo = queja.Titulo,
                Descripcion = queja.Descripcion,
                DescripcionCompleta = queja.Descripcion,
                FechaRegistro = queja.FechaRegistro,
                Estado = queja.Estado,
                Prioridad = queja.Prioridad,
                Categoria = queja.Categoria,
                UsuarioId = queja.UsuarioId,
                NombreUsuario = GetNombreUsuario(queja.Usuario?.UserName)
            };
        }
        public async Task<QuejaEditDto?> GetQuejaForEditAsync(int id)
        {
            var queja = await _quejaRepository.GetByIdAsync(id);
            if (queja == null) return null;

            return new QuejaEditDto
            {
                QuejaId = queja.QuejaId,
                Titulo = queja.Titulo,
                Descripcion = queja.Descripcion,
                Estado = queja.Estado,
                Prioridad = queja.Prioridad,
                Categoria = queja.Categoria
            };
        }
        public async Task<Queja> CreateQuejaAsync(QuejaCreateDto dto, string usuarioId)
        {
            var queja = new Queja
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Prioridad = dto.Prioridad,
                Categoria = dto.Categoria,
                Estado = "Pendiente",
                FechaRegistro = DateTime.Now,
                UsuarioId = usuarioId
            };

            return await _quejaRepository.AddAsync(queja);
        }
        public async Task<bool> UpdateQuejaAsync(QuejaEditDto dto, string usuarioId)
        {
            var queja = await _quejaRepository.GetByIdAsync(dto.QuejaId);
            if (queja == null) return false;

            if (queja.UsuarioId != usuarioId) return false;
            queja.Titulo = dto.Titulo;
            queja.Descripcion = dto.Descripcion;
            queja.Estado = dto.Estado;
            queja.Prioridad = dto.Prioridad;
            queja.Categoria = dto.Categoria;

            await _quejaRepository.UpdateAsync(queja);
            return true;
        }
        public async Task<bool> DeleteQuejaAsync(int id, string usuarioId)
        {
            var queja = await _quejaRepository.GetByIdAsync(id);
            if (queja == null) return false;
            if (queja.UsuarioId != usuarioId) return false;

            await _quejaRepository.DeleteAsync(queja);
            return true;
        }
        public async Task<bool> IsOwnerAsync(int quejaId, string usuarioId)
        {
            var queja = await _quejaRepository.GetByIdAsync(quejaId);
            return queja?.UsuarioId == usuarioId;
        }
        public async Task<IEnumerable<QuejaResumenViewModel>> GetLatestQuejasAsync(int count)
        {
            var quejas = await _quejaRepository.GetLatestAsync(count);
            return quejas.Select(MapToResumenViewModel);
        }
        public async Task<DashboardViewModel> GetDashboardDataAsync(int latestCount = 5)
        {
            var stats = await _quejaRepository.GetEstadisticasAsync();
            var ultimasQuejas = await GetLatestQuejasAsync(latestCount);

            return new DashboardViewModel
            {
                TotalQuejas = stats.Total,
                QuejasPendientes = stats.Pendientes,
                QuejasEnProceso = stats.EnProceso,
                QuejasResueltas = stats.Resueltas,
                QuejasCerradas = stats.Cerradas,
                UltimasQuejas = ultimasQuejas.ToList()
            };
        }
        public async Task<IEnumerable<QuejaListDto>> GetQuejasByCategoriaAsync(string categoria)
        {
            var quejas = await _quejaRepository.GetByCategoriaAsync(categoria);
            return quejas.Select(MapToListDto);
        }
        public async Task<IEnumerable<QuejaListDto>> GetQuejasByEstadoAsync(string estado)
        {
            var quejas = await _quejaRepository.GetByEstadoAsync(estado);
            return quejas.Select(MapToListDto);
        }

        #region Private Helpers

        /// <summary>
        /// Mapea una entidad Queja a QuejaListDto
        /// </summary>
        private QuejaListDto MapToListDto(Queja queja)
        {
            return new QuejaListDto
            {
                QuejaId = queja.QuejaId,
                Titulo = queja.Titulo,
                Descripcion = queja.Descripcion.Length > 200 
                    ? queja.Descripcion.Substring(0, 200) + "..." 
                    : queja.Descripcion,
                FechaRegistro = queja.FechaRegistro,
                Estado = queja.Estado,
                Prioridad = queja.Prioridad,
                Categoria = queja.Categoria,
                UsuarioId = queja.UsuarioId,
                NombreUsuario = GetNombreUsuario(queja.Usuario?.UserName)
            };
        }

        /// <summary>
        /// Mapea una entidad Queja a QuejaResumenViewModel
        /// </summary>
        private QuejaResumenViewModel MapToResumenViewModel(Queja queja)
        {
            return new QuejaResumenViewModel
            {
                QuejaId = queja.QuejaId,
                Titulo = queja.Titulo,
                DescripcionCorta = queja.Descripcion.Length > 150 
                    ? queja.Descripcion.Substring(0, 150) + "..." 
                    : queja.Descripcion,
                Estado = queja.Estado,
                Prioridad = queja.Prioridad,
                Categoria = queja.Categoria,
                UsuarioId = queja.UsuarioId,
                NombreAutor = GetNombreUsuario(queja.Usuario?.UserName),
                FechaRegistro = queja.FechaRegistro,
                TiempoTranscurrido = GetTiempoTranscurrido(queja.FechaRegistro)
            };
        }

        /// <summary>
        /// Obtiene el nombre de usuario limpio (sin dominio de email)
        /// </summary>
        private string GetNombreUsuario(string? userName)
        {
            if (string.IsNullOrEmpty(userName)) return "Anónimo";
            return userName.Contains('@') ? userName.Split('@')[0] : userName;
        }

        /// <summary>
        /// Calcula el tiempo transcurrido desde una fecha
        /// </summary>
        private string GetTiempoTranscurrido(DateTime fecha)
        {
            var diferencia = DateTime.Now - fecha;
            
            if (diferencia.TotalMinutes < 1) return "ahora mismo";
            if (diferencia.TotalMinutes < 60) return $"hace {(int)diferencia.TotalMinutes} min";
            if (diferencia.TotalHours < 24) return $"hace {(int)diferencia.TotalHours} h";
            if (diferencia.TotalDays < 7) return $"hace {(int)diferencia.TotalDays} días";
            if (diferencia.TotalDays < 30) return $"hace {(int)(diferencia.TotalDays / 7)} semanas";
            
            return fecha.ToString("dd MMM yyyy");
        }

        #endregion
    }
}
