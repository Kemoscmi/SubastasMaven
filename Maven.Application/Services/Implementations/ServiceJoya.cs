using AutoMapper;
using Maven.Application.DTOs;
using Maven.Application.Services.Interfaces;
using Maven.Infraestructure.MavenModels;
using Maven.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.Services.Implementations
{
    public class ServiceJoya : IServiceJoya
    {
        private readonly IRepositoryJoya _repository;
        private readonly IMapper _mapper;

        public ServiceJoya(IRepositoryJoya repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> AddAsync(JoyaDTO dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var entity = _mapper.Map<Joya>(dto);

            if (entity.FechaRegistro == default)
                entity.FechaRegistro = DateTime.Now;

            return await _repository.AddAsync(entity);
        }

        public async Task<JoyaDTO> FindByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var entity = await _repository.FindByIdAsync(id);
            if (entity is null)
                throw new KeyNotFoundException($"No existe una joya con id {id}");

            //  Mapeo base (nombre, descripción, estado, condición, vendedor, etc.)
            var dto = _mapper.Map<JoyaDTO>(entity);

            //  IMAGEN PRINCIPAL
            dto.ImagenPrincipal = entity.JoyaImagen
                .OrderBy(i => i.JoyaImagenId)
                .Select(i => i.UrlImagen)
                .FirstOrDefault() ?? string.Empty;

            // TODAS LAS IMÁGENES para el detalle
            dto.JoyaImagen = entity.JoyaImagen
                .OrderBy(i => i.JoyaImagenId)
                .Select(i => new JoyaImagenDTO
                {
                    JoyaImagenId = i.JoyaImagenId,
                    JoyaId = i.JoyaId,
                    UrlImagen = i.UrlImagen
                  
                })
                .ToList();

            //  CATEGORÍAS texto plano para la vista
            dto.CategoriasTexto = entity.CategoriaJoya.Any()
                ? string.Join(", ", entity.CategoriaJoya.Select(c => c.Nombre))
                : string.Empty;

            //  HISTORIAL DE SUBASTAS (Id, fechas, estado)
            dto.Subasta = entity.Subasta
                .OrderByDescending(s => s.FechaInicio)
                .Select(s => new SubastaDTO
                {
                    SubastaId = s.SubastaId,
                    FechaInicio = s.FechaInicio,
                    FechaCierre = s.FechaCierre,
                    EstadoSubasta = s.EstadoSubasta == null
                        ? null
                        : new EstadoSubastaDTO
                        {
                            EstadoSubastaId = s.EstadoSubasta.EstadoSubastaId,
                            NombreEstado = s.EstadoSubasta.NombreEstado
                        }
                })
                .ToList();

            return dto;
        }
        public async Task<ICollection<JoyaDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();

            var dtos = new List<JoyaDTO>();

            foreach (var j in list)
            {
                // 1) Mapeamos lo básico con AutoMapperc
                var dto = _mapper.Map<JoyaDTO>(j);

                // 2) Calculamos la imagen principal desde la colección JoyaImagen
                dto.ImagenPrincipal = j.JoyaImagen
                    .OrderBy(i => i.JoyaImagenId)
                    .Select(i => i.UrlImagen)
                    .FirstOrDefault() ?? string.Empty;

                // 3) Calculamos el texto de categorías
                dto.CategoriasTexto = (j.CategoriaJoya != null && j.CategoriaJoya.Any())
                    ? string.Join(", ", j.CategoriaJoya.Select(c => c.Nombre))
                    : "Sin categorías";

                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task UpdateAsync(int id, JoyaDTO dto)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var entity = await _repository.FindByIdAsync(id);
            if (entity is null) throw new KeyNotFoundException($"No existe una joya con id {id}");

            _mapper.Map(dto, entity); // mapea sobre la entidad
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            await _repository.DeleteAsync(id);
        }
    }
}


