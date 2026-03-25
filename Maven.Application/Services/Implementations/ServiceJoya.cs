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
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var entity = _mapper.Map<Joya>(dto);

            if (entity.FechaRegistro == default)
                entity.FechaRegistro = DateTime.Now;

            entity.Vendedor = null;
            entity.EstadoObjeto = null;
            entity.CondicionObjeto = null;
            entity.Subasta = new List<Subasta>();
            entity.CategoriaJoya = new List<CategoriaJoya>();
            entity.JoyaImagen = new List<JoyaImagen>();

            if (!string.IsNullOrWhiteSpace(dto.ImagenPrincipal))
            {
                entity.JoyaImagen.Add(new JoyaImagen
                {
                    UrlImagen = dto.ImagenPrincipal
                });
            }

            return await _repository.AddAsync(entity);
        }
        public async Task<JoyaDTO> FindByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var entity = await _repository.FindByIdAsync(id);
            if (entity is null)
                throw new KeyNotFoundException($"No existe una joya con id {id}");

            //  Mapeo base 
            var dto = _mapper.Map<JoyaDTO>(entity);

            //  IMAGEN PRINCIPAL
            dto.ImagenPrincipal = entity.JoyaImagen
                .OrderBy(i => i.JoyaImagenId)
                .Select(i => i.UrlImagen)
                .FirstOrDefault() ?? string.Empty;

            // TODAS LAS IMÁGENES para el detalle
            var imgs = (entity.JoyaImagen ?? new List<JoyaImagen>())
      .Where(i => !string.IsNullOrWhiteSpace(i.UrlImagen))
      .GroupBy(i => i.UrlImagen.Trim())
      .Select(g => g.OrderBy(x => x.JoyaImagenId).First())
      .OrderBy(i => i.JoyaImagenId)
      .ToList();

            dto.ImagenPrincipal = imgs.FirstOrDefault()?.UrlImagen ?? string.Empty;

            dto.JoyaImagen = imgs.Select(i => new JoyaImagenDTO
            {
                JoyaImagenId = i.JoyaImagenId,
                JoyaId = i.JoyaId,
                UrlImagen = i.UrlImagen
            }).ToList();

            //  CATEGORÍAS
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



            dto.CategoriaJoya = entity.CategoriaJoya
    .Select(c => new CategoriaJoyaDTO
    {
        CategoriaJoyaId = c.CategoriaJoyaId,
        Nombre = c.Nombre
    })
    .ToList();

            dto.EstadoObjeto = entity.EstadoObjeto == null
                ? new EstadoObjetoDTO()
                : new EstadoObjetoDTO
                {
                    EstadoObjetoId = entity.EstadoObjeto.EstadoObjetoId,
                    NombreEstado = entity.EstadoObjeto.NombreEstado
                };

            dto.Vendedor = entity.Vendedor == null
                ? new UsuarioDTO()
                : new UsuarioDTO
                {
                    UsuarioId = entity.Vendedor.UsuarioId,
                    NombreCompleto = entity.Vendedor.NombreCompleto,
                    Correo = entity.Vendedor.Correo
                };

            return dto;
        }
        public async Task<List<JoyaDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();     
            return _mapper.Map<List<JoyaDTO>>(list);
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

        public async Task AddCategoriasAsync(int joyaId, List<int> categoriaIds)
        {
            if (joyaId <= 0)
                throw new ArgumentOutOfRangeException(nameof(joyaId));

            if (categoriaIds == null || categoriaIds.Count == 0)
                return;

            await _repository.AddCategoriasAsync(joyaId, categoriaIds);
        }

        public async Task AddImagenesAsync(int joyaId, List<string> rutasImagenes)
        {
            if (joyaId <= 0)
                throw new ArgumentOutOfRangeException(nameof(joyaId));

            if (rutasImagenes == null || rutasImagenes.Count == 0)
                return;

            await _repository.AddImagenesAsync(joyaId, rutasImagenes);
        }

        public async Task ReplaceCategoriasAsync(int joyaId, List<int> categoriaIds)
        {
            if (joyaId <= 0)
                throw new ArgumentOutOfRangeException(nameof(joyaId));

            if (categoriaIds == null)
                categoriaIds = new List<int>();

            await _repository.ReplaceCategoriasAsync(joyaId, categoriaIds);
        }

        public async Task DeleteImagenAsync(int joyaImagenId)
        {
            if (joyaImagenId <= 0)
                throw new ArgumentOutOfRangeException(nameof(joyaImagenId));

            await _repository.DeleteImagenAsync(joyaImagenId);
        }

        public async Task<List<JoyaDTO>> ListInactivosAsync()
        {
            var data = await _repository.ListInactivosAsync();

            return data.Select(entity => new JoyaDTO
            {
                JoyaId = entity.JoyaId,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
                EstadoObjetoId = entity.EstadoObjetoId,
                ImagenPrincipal = entity.JoyaImagen.FirstOrDefault()?.UrlImagen ?? "",
                CategoriasTexto = string.Join(", ", entity.CategoriaJoya.Select(c => c.Nombre)),
                CondicionObjeto = entity.CondicionObjeto == null ? null : new CondicionObjetoDTO
                {
                    CondicionObjetoId = entity.CondicionObjeto.CondicionObjetoId,
                    NombreCondicion = entity.CondicionObjeto.NombreCondicion
                },
                EstadoObjeto = entity.EstadoObjeto == null ? null : new EstadoObjetoDTO
                {
                    EstadoObjetoId = entity.EstadoObjeto.EstadoObjetoId,
                    NombreEstado = entity.EstadoObjeto.NombreEstado
                }
            }).ToList();
        }

        public async Task ToggleEstadoAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            await _repository.ToggleEstadoAsync(id);
        }
    }
}


