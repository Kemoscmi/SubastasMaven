using AutoMapper;
using Maven.Application.DTOs;
using Maven.Application.Services.Interfaces;
using Maven.Infraestructure.MavenModels;
using Maven.Infraestructure.Repository.Interfaces;

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

            Usuario entity = _mapper.Map<Usuario>(dto);

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

            var entity = await _repository.FindByIdAsync(id);

            if (entity is null)
                throw new KeyNotFoundException($"No existe un usuario con id {id}.");

            try
            {
                // Importante: mapear sobre la entidad existente (tracked)
                _mapper.Map(dto, entity);

                await _repository.UpdateAsync(entity);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new InvalidOperationException("Error al mapear UsuarioDTO sobre Usuario existente.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "El id debe ser mayor que 0.");

            await _repository.DeleteAsync(id);
        }
    }
}
