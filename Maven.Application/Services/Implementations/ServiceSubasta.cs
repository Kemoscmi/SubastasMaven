using AutoMapper;
using Maven.Application.DTOs;
using Maven.Application.DTOs.Subasta;
using Maven.Application.Services.Interfaces;
using Maven.Infraestructure.MavenModels;
using Maven.Infraestructure.Repository.Interfaces;


namespace Maven.Application.Services.Implementations
{
    public class ServiceSubasta : IServiceSubasta
    {
        private readonly IRepositorySubasta _repository;
        private readonly IMapper _mapper;
        private readonly IRepositorySubastaResultado _repositorySubastaResultado;
        private readonly IRepositoryPago _repositoryPago;

        public ServiceSubasta(
      IRepositorySubasta repository,
      IRepositorySubastaResultado repositorySubastaResultado,
      IRepositoryPago repositoryPago,
      IMapper mapper)
        {
            _repository = repository;
            _repositorySubastaResultado = repositorySubastaResultado;
            _repositoryPago = repositoryPago;
            _mapper = mapper;
        }

        public async Task<ICollection<SubastaDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<SubastaDTO>>(list);
        }

        public async Task<SubastaDTO> FindByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var entity = await _repository.FindByIdAsync(id);
            if (entity is null)
                throw new KeyNotFoundException($"No existe una subasta con id {id}");

            return _mapper.Map<SubastaDTO>(entity);
        }

        public async Task<int> AddAsync(SubastaDTO dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.FechaInicio < DateTime.Now)
                throw new ArgumentException("La fecha de inicio no puede ser menor a la fecha actual.");

            if (dto.FechaCierre <= dto.FechaInicio)
                throw new ArgumentException("La fecha de cierre debe ser mayor que la fecha de inicio.");

            if (dto.PrecioBase <= 0)
                throw new ArgumentException("El precio base debe ser mayor a 0.");

            if (dto.IncrementoMinimo <= 0)
                throw new ArgumentException("El incremento mínimo debe ser mayor a 0.");

            var entity = _mapper.Map<Subasta>(dto);

            entity.EstadoSubastaId = 1; 
            entity.FechaCreacion = DateTime.Now;
            entity.FechaPublicacion = null;

            return await _repository.AddAsync(entity);
        }
        public async Task UpdateAsync(int id, SubastaDTO dto)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.FechaInicio < DateTime.Now)
                throw new ArgumentException("La fecha de inicio no puede ser menor a la fecha actual.");

            if (dto.FechaCierre <= dto.FechaInicio)
                throw new ArgumentException("La fecha de cierre debe ser mayor que la fecha de inicio.");

            if (dto.PrecioBase <= 0)
                throw new ArgumentException("El precio base debe ser mayor a 0.");

            if (dto.IncrementoMinimo <= 0)
                throw new ArgumentException("El incremento mínimo debe ser mayor a 0.");

            var entity = await _repository.FindByIdAsync(id);
            if (entity is null)
                throw new KeyNotFoundException($"No existe una subasta con id {id}");

            if (entity.EstadoSubastaId != 1 && entity.EstadoSubastaId != 2)
                throw new InvalidOperationException("Solo se pueden editar subastas en estado borrador o publicada.");

            if (entity.FechaInicio <= DateTime.Now)
                throw new InvalidOperationException("No se puede editar una subasta que ya inició.");

            if (entity.Puja != null && entity.Puja.Any())
                throw new InvalidOperationException("No se puede editar una subasta que ya tiene pujas.");

            var joyaIdOriginal = entity.JoyaId;
            var vendedorIdOriginal = entity.VendedorId;
            var fechaCreacionOriginal = entity.FechaCreacion;
            var fechaPublicacionOriginal = entity.FechaPublicacion;

            _mapper.Map(dto, entity);

            entity.JoyaId = joyaIdOriginal;
            entity.VendedorId = vendedorIdOriginal;
            entity.FechaCreacion = fechaCreacionOriginal;

            if (dto.EstadoSubastaId == 2 && fechaPublicacionOriginal == null)
                entity.FechaPublicacion = DateTime.Now;
            else
                entity.FechaPublicacion = fechaPublicacionOriginal;

            // Limpiar navegaciones viejas para que la vista respete los IDs
            entity.Joya = null!;
            entity.Vendedor = null!;
            entity.EstadoSubasta = null!;
            entity.Puja = new List<Puja>();
            entity.SubastaResultado = null;

            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            await _repository.DeleteAsync(id);
        }

        public async Task<SubastaCombosDTO> GetCombosAsync()
        {
            var joyas = await _repository.GetJoyasAsync();
            var vendedores = await _repository.GetVendedoresAsync();
            var estados = await _repository.GetEstadosSubastaAsync();

            return new SubastaCombosDTO
            {
                Joyas = joyas
                    .Select(j => new ComboItemDTO
                    {
                        Id = j.JoyaId,
                        Nombre = j.Nombre
                    })
                    .ToList(),

                Vendedores = vendedores
                    .Select(v => new ComboItemDTO
                    {
                        Id = v.UsuarioId,
                        Nombre = v.NombreCompleto
                    })
                    .ToList(),

                EstadosSubasta = estados
                    .Select(e => new ComboItemDTO
                    {
                        Id = e.EstadoSubastaId,
                        Nombre = e.NombreEstado
                    })
                    .ToList()
            };
        }

        public async Task<ICollection<SubastaHistorialDTO>> GetActivasAsync()
        {
            var list = await _repository.GetActivasAsync();

            return list.Select(s => new SubastaHistorialDTO
            {
                SubastaId = s.SubastaId,
                Objeto = s.Joya?.Nombre ?? string.Empty,
                Estado = s.EstadoSubasta?.NombreEstado ?? string.Empty,
                FechaInicio = s.FechaInicio,
                FechaRef = s.FechaCierre,
                ImagenUrl = s.Joya?.JoyaImagen?
                    .OrderByDescending(img => img.FechaRegistro)
                    .Select(img => img.UrlImagen)
                    .FirstOrDefault(),
                CantidadPujas = s.Puja?.Count ?? 0
            }).ToList();
        }

        public async Task<ICollection<SubastaHistorialDTO>> GetFinalizadasAsync()
        {
            var list = await _repository.GetFinalizadasAsync();

            return list.Select(s => new SubastaHistorialDTO
            {
                SubastaId = s.SubastaId,
                Objeto = s.Joya?.Nombre ?? string.Empty,
                Estado = s.EstadoSubasta?.NombreEstado ?? string.Empty,
                FechaInicio = s.FechaInicio,
                FechaRef = s.FechaCierre,
                ImagenUrl = s.Joya?.JoyaImagen?
                    .OrderByDescending(img => img.FechaRegistro)
                    .Select(img => img.UrlImagen)
                    .FirstOrDefault(),
                CantidadPujas = s.Puja?.Count ?? 0
            }).ToList();
        }

        public async Task<SubastaDetalleVisualDTO> GetDetalleVisualAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var s = await _repository.GetDetalleVisualByIdAsync(id);

            if (s is null)
                throw new KeyNotFoundException($"No existe una subasta con id {id}");

            var pujasOrdenadas = s.Puja?
                .OrderByDescending(p => p.MontoOfertado)
                .ThenBy(p => p.FechaHora)
                .ToList() ?? new List<Puja>();

            var pujaLider = pujasOrdenadas.FirstOrDefault();

            return new SubastaDetalleVisualDTO
            {
                SubastaId = s.SubastaId,
                NombreJoya = s.Joya?.Nombre ?? string.Empty,
                DescripcionJoya = s.Joya?.Descripcion ?? string.Empty,
                Condicion = s.Joya?.CondicionObjeto?.NombreCondicion,
                Categorias = s.Joya?.CategoriaJoya?
                    .Select(c => c.Nombre)
                    .OrderBy(n => n)
                    .ToList() ?? new List<string>(),
                FechaInicio = s.FechaInicio,
                FechaCierre = s.FechaCierre,
                PrecioBase = s.PrecioBase,
                IncrementoMinimo = s.IncrementoMinimo,
                EstadoActual = s.EstadoSubasta?.NombreEstado ?? string.Empty,
                CantidadPujas = s.Puja?.Count ?? 0,
                Vendedor = s.Vendedor?.NombreCompleto ?? string.Empty,
                ImagenUrl = s.Joya?.JoyaImagen?
                    .OrderByDescending(img => img.FechaRegistro)
                    .Select(img => img.UrlImagen)
                    .FirstOrDefault(),

                // CAMBIOS AQUÍ
                TienePujas = pujasOrdenadas.Any(),
                PujaActual = pujaLider?.MontoOfertado ?? 0,
                UsuarioLider = pujaLider?.Comprador?.NombreCompleto ?? "Sin usuario líder",
                UsuarioLiderId = pujaLider?.CompradorId,

                Finalizada = s.EstadoSubastaId == 4 || s.EstadoSubastaId == 5,
                HistorialPujas = s.Puja?
                    .OrderByDescending(p => p.FechaHora)
                    .Select(p => new PujaHistorialDTO
                    {
                        PujaId = p.PujaId,
                        SubastaId = p.SubastaId,
                        Usuario = p.Comprador?.NombreCompleto ?? string.Empty,
                        MontoOfertado = p.MontoOfertado,
                        FechaHora = p.FechaHora
                    })
                    .ToList() ?? new List<PujaHistorialDTO>()
            };
        }
        public async Task<ICollection<PujaHistorialDTO>> GetHistorialPujasAsync(int subastaId)
        {
            if (subastaId <= 0)
                throw new ArgumentOutOfRangeException(nameof(subastaId));

            var subasta = await _repository.FindByIdAsync(subastaId);
            if (subasta is null)
                throw new KeyNotFoundException($"No existe una subasta con id {subastaId}");

            var pujas = await _repository.GetPujasBySubastaIdAsync(subastaId);

            return pujas.Select(p => new PujaHistorialDTO
            {
                PujaId = p.PujaId,
                SubastaId = p.SubastaId,
                Usuario = p.Comprador?.NombreCompleto ?? string.Empty,
                MontoOfertado = p.MontoOfertado,
                FechaHora = p.FechaHora
            }).ToList();
        }

        public async Task<ICollection<SubastaBorradorDTO>> GetBorradoresByVendedorAsync(int vendedorId)
        {
            if (vendedorId <= 0)
                throw new ArgumentOutOfRangeException(nameof(vendedorId));

            var list = await _repository.GetBorradoresByVendedorAsync(vendedorId);

            return list.Select(s => new SubastaBorradorDTO
            {
                SubastaId = s.SubastaId,
                Objeto = s.Joya?.Nombre ?? string.Empty,
                FechaInicio = s.FechaInicio,
                FechaCierre = s.FechaCierre,
                PrecioBase = s.PrecioBase,
                IncrementoMinimo = s.IncrementoMinimo,
                Estado = s.EstadoSubasta?.NombreEstado ?? string.Empty,
                CantidadPujas = s.Puja?.Count ?? 0,
                ImagenUrl = s.Joya?.JoyaImagen?
                    .OrderByDescending(i => i.FechaRegistro)
                    .Select(i => i.UrlImagen)
                    .FirstOrDefault()
            }).ToList();
        }

        public async Task PublicarAsync(int id)
        {
            var entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                throw new Exception("Subasta no encontrada");

            if (entity.EstadoSubastaId != 1)
                throw new Exception("Solo se pueden publicar subastas en borrador");

            if (entity.FechaInicio <= DateTime.Now)
                throw new Exception("La fecha de inicio debe ser futura");

            entity.EstadoSubastaId = 2; 
            entity.FechaPublicacion = DateTime.Now;

            entity.Joya = null!;
            entity.Vendedor = null!;
            entity.EstadoSubasta = null!;
            entity.Puja = new List<Puja>();
            entity.SubastaResultado = null;

            await _repository.UpdateAsync(entity);
        }

        public async Task CancelarAsync(int id)
        {
            var entity = await _repository.FindByIdAsync(id);

            if (entity == null)
                throw new Exception("Subasta no encontrada");

            if (entity.EstadoSubastaId == 4 || entity.EstadoSubastaId == 5)
                throw new Exception("No se puede cancelar esta subasta");

            bool yaInicio = entity.FechaInicio <= DateTime.Now;
            bool tienePujas = entity.Puja != null && entity.Puja.Any();

            if (yaInicio && tienePujas)
                throw new Exception("No se puede cancelar una subasta iniciada con pujas");

            entity.EstadoSubastaId = 5; 

            entity.Joya = null!;
            entity.Vendedor = null!;
            entity.EstadoSubasta = null!;
            entity.Puja = new List<Puja>();
            entity.SubastaResultado = null;

            await _repository.UpdateAsync(entity);
        }

        public async Task<int> ActivarPublicadasAsync()
        {
            var subastas = await _repository.GetPublicadasParaActivarAsync();

            if (subastas == null || !subastas.Any())
                return 0;

            foreach (var subasta in subastas)
            {
                subasta.EstadoSubastaId = 3; 
                subasta.EstadoSubasta = null!;
            }

            await _repository.SaveChangesAsync();
            return subastas.Count;
        }

        public async Task<ICollection<SubastaCierreTiempoRealDTO>> CerrarSubastasVencidasAsync()
        {
            var subastas = await _repository.GetActivasParaCerrarAsync();

            var resultadoTiempoReal = new List<SubastaCierreTiempoRealDTO>();

            if (subastas == null || !subastas.Any())
                return resultadoTiempoReal;

            foreach (var subasta in subastas)
            {
                // Si ya tenía resultado, solo aseguramos estado y armamos respuesta
                if (subasta.SubastaResultado != null)
                {
                    subasta.EstadoSubastaId = 4;
                    subasta.EstadoSubasta = null!;

                    resultadoTiempoReal.Add(new SubastaCierreTiempoRealDTO
                    {
                        SubastaId = subasta.SubastaId,
                        Estado = "FINALIZADA",
                        UsuarioGanador = subasta.SubastaResultado.Ganador?.NombreCompleto,
                        MontoFinal = subasta.SubastaResultado.MontoFinal,
                        SinPujas = subasta.SubastaResultado.GanadorId == null
                    });

                    continue;
                }

                // Regla: mayor monto; si empatan, gana la más temprana
                var pujaGanadora = subasta.Puja?
                    .OrderByDescending(p => p.MontoOfertado)
                    .ThenBy(p => p.FechaHora)
                    .FirstOrDefault();

                subasta.EstadoSubastaId = 4;
                subasta.EstadoSubasta = null!;

                subasta.SubastaResultado = new SubastaResultado
                {
                    SubastaId = subasta.SubastaId,
                    GanadorId = pujaGanadora?.CompradorId,
                    MontoFinal = pujaGanadora?.MontoOfertado,
                    PujaGanadoraId = pujaGanadora?.PujaId,
                    FechaCierre = subasta.FechaCierre
                };

                if (pujaGanadora != null)
                {
                    var pagoExistente = await _repositoryPago.FindBySubastaIdAsync(subasta.SubastaId);

                    if (pagoExistente == null)
                    {
                        var pago = new Pago
                        {
                            SubastaId = subasta.SubastaId,
                            CompradorId = pujaGanadora.CompradorId,
                            VendedorId = subasta.VendedorId,
                            Monto = pujaGanadora.MontoOfertado,
                            FechaRegistro = DateTime.Now,
                            EstadoPagoId = 1
                        };

                        await _repositoryPago.AddAsync(pago);
                    }
                }

                resultadoTiempoReal.Add(new SubastaCierreTiempoRealDTO
                {
                    SubastaId = subasta.SubastaId,
                    Estado = "FINALIZADA",
                    UsuarioGanador = pujaGanadora?.Comprador?.NombreCompleto,
                    MontoFinal = pujaGanadora?.MontoOfertado,
                    SinPujas = pujaGanadora == null
                });
            }

            await _repository.SaveChangesAsync();
            return resultadoTiempoReal;
        }


        public async Task<ICollection<SubastaDTO>> ListVisiblesAsync()
        {
            var list = await _repository.ListVisiblesAsync();
            return _mapper.Map<ICollection<SubastaDTO>>(list);
        }

        public async Task CerrarSubastaAsync(int subastaId)
        {
            var subasta = await _repository.FindByIdAsync(subastaId);

            if (subasta is null)
                throw new KeyNotFoundException("La subasta no existe.");

            if (subasta.EstadoSubastaId == 4)
                return;

            var pujas = await _repository.GetPujasBySubastaIdAsync(subastaId);

            subasta.EstadoSubastaId = 4;

            await _repository.UpdateAsync(subasta);

            if (subasta.SubastaResultado != null)
                return;

            var pujaGanadora = pujas
                .Where(p => p.FechaHora <= subasta.FechaCierre)
                .OrderByDescending(p => p.MontoOfertado)
                .ThenBy(p => p.FechaHora)
                .FirstOrDefault();

            var resultado = new SubastaResultado
            {
                SubastaId = subasta.SubastaId,
                GanadorId = pujaGanadora?.CompradorId,
                PujaGanadoraId = pujaGanadora?.PujaId,
                MontoFinal = pujaGanadora?.MontoOfertado,
                FechaCierre = subasta.FechaCierre
            };

            await _repositorySubastaResultado.AddAsync(resultado);

            if (pujaGanadora != null)
            {
                var pagoExistente = await _repositoryPago.FindBySubastaIdAsync(subasta.SubastaId);

                if (pagoExistente == null)
                {
                    var pago = new Pago
                    {
                        SubastaId = subasta.SubastaId,
                        CompradorId = pujaGanadora.CompradorId,
                        VendedorId = subasta.VendedorId,
                        Monto = pujaGanadora.MontoOfertado,
                        FechaRegistro = DateTime.Now,
                        EstadoPagoId = 1
                    };

                    await _repositoryPago.AddAsync(pago);
                }
            }
        }
    }
}