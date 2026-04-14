using AutoMapper;
using Maven.Application.DTOs;
using Maven.Application.DTOs.Subasta;
using Maven.Application.Services.Interfaces;
using Maven.Infraestructure.MavenModels;
using Maven.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.Services.Implementations
{
    public class ServicePuja : IServicePuja
    {
        private readonly IRepositoryPuja _repositoryPuja;
        private readonly IRepositorySubasta _repositorySubasta;
        private readonly IUsuarioActualService _usuarioActualService;
        private readonly IMapper _mapper;

        public ServicePuja(
            IRepositoryPuja repositoryPuja,
            IRepositorySubasta repositorySubasta,
            IUsuarioActualService usuarioActualService,
            IMapper mapper)
        {
            _repositoryPuja = repositoryPuja;
            _repositorySubasta = repositorySubasta;
            _usuarioActualService = usuarioActualService;
            _mapper = mapper;
        }

        public async Task<PujaDTO> RegistrarPujaAsync(RegistrarPujaDTO dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            await using var transaction = await _repositoryPuja.BeginTransactionAsync();

            try
            {
                // 🔹 Obtener usuario actual desde login
                int usuarioActualId = _usuarioActualService.GetUsuarioId();

                // 🔹 Obtener subasta
                var subasta = await _repositorySubasta.FindByIdAsync(dto.SubastaId);

                if (subasta is null)
                    throw new KeyNotFoundException("La subasta no existe.");

                // 🔹 VALIDACIÓN 1: Subasta activa
                if (subasta.EstadoSubastaId != 3) // 3 = Activa (ajústalo si tu id es otro)
                    throw new InvalidOperationException("La subasta no está activa.");

                // 🔹 VALIDACIÓN 2: No puede ser el vendedor
                if (subasta.VendedorId == usuarioActualId)
                    throw new InvalidOperationException("El vendedor no puede pujar en su propia subasta.");

                // 🔹 Obtener puja más alta actual (LINQ)
                var montoActual = await _repositoryPuja.Query()
                    .Where(p => p.SubastaId == dto.SubastaId)
                    .Select(p => (decimal?)p.MontoOfertado)
                    .MaxAsync() ?? subasta.PrecioBase;

                // 🔹 VALIDACIÓN 3: Monto mayor a la actual
                if (dto.MontoOfertado <= montoActual)
                    throw new InvalidOperationException("El monto debe ser mayor a la puja actual.");

                // 🔹 VALIDACIÓN 4: Incremento mínimo
                decimal minimoPermitido = montoActual + subasta.IncrementoMinimo;

                if (dto.MontoOfertado < minimoPermitido)
                    throw new InvalidOperationException($"Debe respetar el incremento mínimo de {subasta.IncrementoMinimo}.");

                // 🔹 Crear puja
                var nuevaPuja = new Puja
                {
                    SubastaId = dto.SubastaId,
                    CompradorId = usuarioActualId,
                    MontoOfertado = dto.MontoOfertado,
                    FechaHora = DateTime.Now
                };

                // 🔹 Guardar
                await _repositoryPuja.AddAsync(nuevaPuja);

                await transaction.CommitAsync();

                // 🔹 Retornar DTO
                return _mapper.Map<PujaDTO>(nuevaPuja);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}