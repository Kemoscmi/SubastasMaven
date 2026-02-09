using Maven.Application.DTOs;
using Maven.Application.Services.Interfaces;
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
        private readonly IRepositoryJoya _repo;

        public ServiceJoya(IRepositoryJoya repo)
        {
            _repo = repo;
        }

        public async Task<List<JoyaListDto>> GetAllAsync()
        {
            var joyas = await _repo.GetAllAsync();

            return joyas.Select(j => new JoyaListDto
            {
                JoyaId = j.JoyaId,
                Nombre = j.Nombre,

                //  imagen principal: primera imagen si existe, si no placeholder
                ImagenPrincipal = j.JoyaImagen
                    .OrderBy(i => i.JoyaImagenId)
                    .Select(i => i.UrlImagen)
                    .FirstOrDefault() ?? "",

                Categorias = string.Join(", ", j.CategoriaJoya.Select(c => c.Nombre)),
                Condicion = j.CondicionObjeto?.NombreCondicion ?? "",
                Estado = j.EstadoObjeto?.NombreEstado ?? ""
            }).ToList();
        }

        public async Task<JoyaDetailDto?> GetByIdAsync(int id)
        {
            var j = await _repo.GetByIdAsync(id);
            if (j == null) return null;

            return new JoyaDetailDto
            {
                JoyaId = j.JoyaId,
                Nombre = j.Nombre,
                Descripcion = j.Descripcion,
                Categorias = string.Join(", ", j.CategoriaJoya.Select(c => c.Nombre)),
                Condicion = j.CondicionObjeto?.NombreCondicion ?? "",
                Estado = j.EstadoObjeto?.NombreEstado ?? "",
                FechaRegistro = j.FechaRegistro,
                Dueno = j.Vendedor?.NombreCompleto ?? "",

                Imagenes = j.JoyaImagen
                    .OrderBy(i => i.JoyaImagenId)
                    .Select(i => i.UrlImagen)
                    .ToList(),

                //  LINQ: historial de subastas donde participó
                HistorialSubastas = j.Subasta
                    .OrderByDescending(s => s.FechaInicio)
                    .Select(s => new JoyaSubastaHistorialDto
                    {
                        SubastaId = s.SubastaId,
                        FechaInicio = s.FechaInicio,
                        FechaCierre = s.FechaCierre,
                        EstadoSubasta = s.EstadoSubasta?.NombreEstado ?? ""
                    })
                    .ToList()
            };
        }
    }
}

