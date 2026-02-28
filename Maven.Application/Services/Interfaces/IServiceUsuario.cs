using Maven.Application.DTOs;

namespace Maven.Application.Services.Interfaces
{
    public interface IServiceUsuario
    {
        Task<int> AddAsync(UsuarioDTO dto);
        Task UpdateAsync(int id, UsuarioDTO dto);
        Task DeleteAsync(int id);
        Task<UsuarioDTO> FindByIdAsync(int id);
        Task<ICollection<UsuarioDTO>> ListAsync();
    }
}
