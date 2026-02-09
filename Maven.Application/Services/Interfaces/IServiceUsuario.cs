using Maven.Application.DTOs;

namespace Maven.Application.Services.Interfaces
{
    public interface IServiceUsuario
    {
        Task<List<UsuarioListDto>> GetAllAsync();
        Task<UsuarioDetailDto?> GetByIdAsync(int id);
    }
}
