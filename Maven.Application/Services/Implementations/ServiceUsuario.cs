using Maven.Application.DTOs;
using Maven.Application.Services.Interfaces;
using Maven.Infraestructure.Repository.Interfaces;

namespace Maven.Application.Services.Implementations
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IRepositoryUsuario _repo;

        public ServiceUsuario(IRepositoryUsuario repo)
        {
            _repo = repo;
        }

        public async Task<List<UsuarioListDto>> GetAllAsync()
        {
            var usuarios = await _repo.GetAllAsync();

            return usuarios.Select(u => new UsuarioListDto
            {
                UsuarioId = u.UsuarioId,
                NombreCompleto = u.NombreCompleto,
                Correo = u.Correo,
                Rol = u.Rol?.NombreRol ?? "(sin rol)",
                Estado = u.EstadoUsuario?.NombreEstado ?? "(sin estado)",
                FechaRegistro = u.FechaRegistro
            }).ToList();
        }
        public async Task<UsuarioDetailDto?> GetByIdAsync(int id)
        {
            var u = await _repo.GetByIdAsync(id);
            if (u == null) return null;

            return new UsuarioDetailDto
            {
                UsuarioId = u.UsuarioId,
                NombreCompleto = u.NombreCompleto,
                Correo = u.Correo,
                Rol = u.Rol?.NombreRol ?? "(sin rol)",
                Estado = u.EstadoUsuario?.NombreEstado ?? "(sin estado)",
                FechaRegistro = u.FechaRegistro,

                // ✅ LINQ (campos calculados)
                CantidadSubastasCreadas = u.Subasta.Count(),
                CantidadPujasRealizadas = u.Puja.Count()
            };
        }

    }
}
