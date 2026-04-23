using AutoMapper;
using Maven.Application.DTOs;
using Maven.Application.Services.Interfaces;
using Maven.Infraestructure.MavenModels;
using Maven.Infraestructure.Repository.Interfaces;
using BCrypt.Net;

namespace Maven.Application.Services.Implementations
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IRepositoryUsuario _repository;
        private readonly IMapper _mapper;

        public ServiceUsuario(IRepositoryUsuario repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> AddAsync(UsuarioDTO dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            var existente = await _repository.FindByEmailAsync(dto.Correo);

            if (existente != null)
                throw new Exception("Ya existe un usuario registrado con ese correo.");


            Usuario entity = _mapper.Map<Usuario>(dto);

            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);
            entity.EstadoUsuarioId = 1;
            if (entity.FechaRegistro == default)
                entity.FechaRegistro = DateTime.Now;

            return await _repository.AddAsync(entity);
        }

        public async Task<UsuarioDTO> FindByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "El id debe ser mayor que 0.");

            var entity = await _repository.FindByIdAsync(id);

            if (entity is null)
                throw new KeyNotFoundException($"No existe un usuario con id {id}.");

            // 1) Mapeamos lo normal con AutoMapper
            var dto = _mapper.Map<UsuarioDTO>(entity);

            // 2) Obtenemos los valores calculados con LINQ desde el repositorio
            var cantSubastas = await _repository.CountSubastasCreadasAsync(entity.UsuarioId);
            var cantPujas = await _repository.CountPujasRealizadasAsync(entity.UsuarioId);

            // 3) Los asignamos al DTO
            dto.CantidadSubastasCreadas = cantSubastas;
            dto.CantidadPujasRealizadas = cantPujas;

            return dto;
        }

        public async Task<ICollection<UsuarioDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<UsuarioDTO>>(list);
        }
        public async Task UpdateAsync(int id, UsuarioDTO dto)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "El id debe ser mayor que 0.");

            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var existente = await _repository.FindByEmailAsync(dto.Correo);

            if (existente != null && existente.UsuarioId != id)
                throw new Exception("Ya existe un usuario registrado con ese correo.");

            var entity = await _repository.FindByIdAsync(id);

            if (entity is null)
                throw new KeyNotFoundException($"No existe un usuario con id {id}.");

            entity.NombreCompleto = dto.NombreCompleto;
            entity.Correo = dto.Correo;

            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "El id debe ser mayor que 0.");

            await _repository.DeleteAsync(id);
        }


        public async Task<UsuarioDTO?> LoginAsync(string correo, string password)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(password))
                return null;

            var usuario = await _repository.FindByEmailAsync(correo);

            if (usuario == null)
                return null;

           
            // Validar estado (bloqueado no entra)
            if (usuario.EstadoUsuarioId != 1) // 1 = ACTIVO
                return null;

            // Validar contraseña con hash
            bool esValido = BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash);
            if (!esValido)
                return null;

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task CambiarEstadoAsync(int usuarioId, int estadoUsuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentOutOfRangeException(nameof(usuarioId), "El id debe ser mayor que 0.");

            if (estadoUsuarioId <= 0)
                throw new ArgumentOutOfRangeException(nameof(estadoUsuarioId), "El estado debe ser válido.");

            var entity = await _repository.FindByIdAsync(usuarioId);

            if (entity is null)
                throw new KeyNotFoundException($"No existe un usuario con id {usuarioId}.");

            await _repository.CambiarEstadoAsync(usuarioId, estadoUsuarioId);
        }
    }
}
