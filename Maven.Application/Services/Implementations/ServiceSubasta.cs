using Microsoft.EntityFrameworkCore;
using Maven.Application.DTOs;
using Maven.Application.Services.Interfaces;
using Maven.Infraestructure.Repository.Interfaces;

namespace Maven.Application.Services.Implementations
{
    public class ServiceSubasta : IServiceSubasta
    {
        private readonly IRepositorySubasta _repoSubasta;
        private readonly IRepositoryPuja _repoPuja;

        public ServiceSubasta(IRepositorySubasta repoSubasta, IRepositoryPuja repoPuja)
        {
            _repoSubasta = repoSubasta;
            _repoPuja = repoPuja;
        }

        public async Task<List<SubastaActivaListDto>> GetActivasAsync()
        {
            return await _repoSubasta.Query()
                .Where(s => s.EstadoSubastaId == 3)
                .Select(s => new SubastaActivaListDto
                {
                    SubastaId = s.SubastaId,
                    Objeto = s.Joya.Nombre,
                    ImagenUrl = s.Joya.JoyaImagen
                        .OrderBy(i => i.JoyaImagenId)
                        .Select(i => i.UrlImagen)
                        .FirstOrDefault(),
                    FechaInicio = s.FechaInicio,
                    FechaEstimadaCierre = s.FechaCierre,
                    CantidadPujas = s.Puja.Count() 
                })
                .OrderBy(s => s.FechaEstimadaCierre)
                .ToListAsync();
        }

        public async Task<List<SubastaFinalizadaListDto>> GetFinalizadasAsync()
        {
            // FINALIZADA=4, CANCELADA=5
            return await _repoSubasta.Query()
                .Where(s => s.EstadoSubastaId == 4 || s.EstadoSubastaId == 5)
                .Select(s => new SubastaFinalizadaListDto
                {
                    SubastaId = s.SubastaId,
                    Objeto = s.Joya.Nombre,
                    ImagenUrl = s.Joya.JoyaImagen
                        .OrderBy(i => i.JoyaImagenId)
                        .Select(i => i.UrlImagen)
                        .FirstOrDefault(),
                    FechaCierre = s.FechaCierre,
                    EstadoFinal = s.EstadoSubasta.NombreEstado,
                    CantidadPujas = s.Puja.Count() 
                })
                .OrderByDescending(s => s.FechaCierre)
                .ToListAsync();
        }

        public async Task<SubastaDetalleDto?> GetDetalleAsync(int subastaId)
        {
            return await _repoSubasta.Query()
                .Where(s => s.SubastaId == subastaId)
                .Select(s => new SubastaDetalleDto
                {
                    SubastaId = s.SubastaId,
                    FechaInicio = s.FechaInicio,
                    FechaCierre = s.FechaCierre,
                    PrecioBase = s.PrecioBase,
                    IncrementoMinimo = s.IncrementoMinimo,
                    EstadoActual = s.EstadoSubasta.NombreEstado,
                    CantidadTotalPujas = s.Puja.Count(),

                    JoyaId = s.JoyaId,
                    NombreObjeto = s.Joya.Nombre,
                    Condicion = s.Joya.CondicionObjeto.NombreCondicion,
                    Categorias = s.Joya.CategoriaJoya
                        .OrderBy(c => c.CategoriaJoyaId)
                        .Select(c => c.Nombre)
                        .ToList(),
                    ImagenUrl = s.Joya.JoyaImagen
                        .OrderBy(i => i.JoyaImagenId)
                        .Select(i => i.UrlImagen)
                        .FirstOrDefault(),
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<PujaListDto>> GetHistorialPujasAsync(int subastaId, bool desc = true)
        {
            var query = _repoPuja.Query()
                .Where(p => p.SubastaId == subastaId)
                .Select(p => new PujaListDto
                {
                    PujaId = p.PujaId,
                    SubastaId = p.SubastaId,
                    Usuario = p.Comprador.NombreCompleto,
                    MontoOfertado = p.MontoOfertado,
                    FechaHora = p.FechaHora
                });

            query = desc
                ? query.OrderByDescending(p => p.FechaHora)
                : query.OrderBy(p => p.FechaHora);

            return await query.ToListAsync();
        }
    }
}
