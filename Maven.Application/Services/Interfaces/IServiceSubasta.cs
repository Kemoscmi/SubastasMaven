using Maven.Application.DTOs;

namespace Maven.Application.Services.Interfaces
{
    public interface IServiceSubasta
    {
        Task<List<SubastaActivaListDto>> GetActivasAsync();
        Task<List<SubastaFinalizadaListDto>> GetFinalizadasAsync();
        Task<SubastaDetalleDto?> GetDetalleAsync(int subastaId);
        Task<List<PujaListDto>> GetHistorialPujasAsync(int subastaId, bool desc = true);
    }
}
