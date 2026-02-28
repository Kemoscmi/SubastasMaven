using Maven.Application.DTOs;

namespace Maven.Application.Services.Interfaces
{
    public interface IServiceSubasta
    {
        Task<int> AddAsync(SubastaDTO dto);
        Task UpdateAsync(int id, SubastaDTO dto);
        Task DeleteAsync(int id);

        Task<SubastaDTO> FindByIdAsync(int id);
        Task<ICollection<SubastaDTO>> ListAsync();
    }
}
