using AutoMapper;
using Maven.Application.DTOs;
using Maven.Application.Services.Interfaces;
using Maven.Infraestructure.MavenModels;
using Maven.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Maven.Application.Services.Implementations
{
    public class ServiceSubasta : IServiceSubasta
    {
        private readonly IRepositorySubasta _repository;
        private readonly IMapper _mapper;

        public ServiceSubasta(IRepositorySubasta repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ICollection<SubastaDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<SubastaDTO>>(list);
        }

        public async Task<SubastaDTO> FindByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            var entity = await _repository.FindByIdAsync(id);
            if (entity is null) throw new KeyNotFoundException($"No existe una subasta con id {id}");

            return _mapper.Map<SubastaDTO>(entity);
        }

        public async Task<int> AddAsync(SubastaDTO dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var entity = _mapper.Map<Subasta>(dto);

            if (entity.FechaCreacion == default)
                entity.FechaCreacion = DateTime.Now;

            return await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, SubastaDTO dto)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var entity = await _repository.FindByIdAsync(id);
            if (entity is null) throw new KeyNotFoundException($"No existe una subasta con id {id}");

            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            await _repository.DeleteAsync(id);
        }
    }
}
