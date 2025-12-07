using AP.quejasymasquejas.Models.DTOs;
using AP.quejasymasquejas.Models.Entities;
using AP.quejasymasquejas.Models.ViewModels;

namespace AP.quejasymasquejas.Business.Interfaces
{
    public interface IQuejaService
    {
        Task<IEnumerable<QuejaListDto>> GetAllQuejasAsync();
        Task<QuejaDetailDto?> GetQuejaByIdAsync(int id);
        Task<QuejaEditDto?> GetQuejaForEditAsync(int id);
        Task<Queja> CreateQuejaAsync(QuejaCreateDto dto, string usuarioId);
        Task<bool> UpdateQuejaAsync(QuejaEditDto dto, string usuarioId);
        Task<bool> DeleteQuejaAsync(int id, string usuarioId);
        Task<bool> IsOwnerAsync(int quejaId, string usuarioId);
        Task<IEnumerable<QuejaResumenViewModel>> GetLatestQuejasAsync(int count);
        Task<DashboardViewModel> GetDashboardDataAsync(int latestCount = 5);
        Task<IEnumerable<QuejaListDto>> GetQuejasByCategoriaAsync(string categoria);
        Task<IEnumerable<QuejaListDto>> GetQuejasByEstadoAsync(string estado);
    }
}
