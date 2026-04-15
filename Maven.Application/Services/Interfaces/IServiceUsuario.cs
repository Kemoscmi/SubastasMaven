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

        Task<UsuarioDTO?> LoginAsync(string correo, string password);

        Task CambiarEstadoAsync(int usuarioId, int estadoUsuarioId);



    }
}
