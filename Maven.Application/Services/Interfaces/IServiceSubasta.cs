using Maven.Application.DTOs;
using Maven.Application.DTOs.Subasta;
using Maven.Infraestructure.MavenModels;

namespace Maven.Application.Services.Interfaces
{
    public interface IServiceSubasta
    {
        Task<int> AddAsync(SubastaDTO dto);
        Task UpdateAsync(int id, SubastaDTO dto);
        Task DeleteAsync(int id);

        Task<SubastaDTO> FindByIdAsync(int id);
        Task<ICollection<SubastaDTO>> ListAsync();
        Task<SubastaCombosDTO> GetCombosAsync();

        Task<ICollection<SubastaHistorialDTO>> GetActivasAsync();
        Task<ICollection<SubastaHistorialDTO>> GetFinalizadasAsync();
        Task<SubastaDetalleVisualDTO> GetDetalleVisualAsync(int id);
        Task<ICollection<PujaHistorialDTO>> GetHistorialPujasAsync(int subastaId);
        Task<ICollection<SubastaBorradorDTO>> GetBorradoresByVendedorAsync(int vendedorId);

        Task PublicarAsync(int id);
        Task CancelarAsync(int id);

        Task<int> ActivarPublicadasAsync();
        Task<ICollection<SubastaCierreTiempoRealDTO>> CerrarSubastasVencidasAsync();
        Task<ICollection<SubastaDTO>> ListVisiblesAsync();
        Task CerrarSubastaAsync(int subastaId);


    }
}