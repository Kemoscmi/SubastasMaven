using AutoMapper;
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
    public class ServicePago: IServicePago
    {

        private readonly IRepositoryPago _repositoryPago;
        private readonly IMapper _mapper;

        public ServicePago(IRepositoryPago repositoryPago, IMapper mapper)
        {
            _repositoryPago = repositoryPago;
            _mapper = mapper;
        }

        public async Task<PagoDTO?> FindBySubastaIdAsync(int subastaId)
        {
            var entity = await _repositoryPago.FindBySubastaIdAsync(subastaId);

            if (entity == null)
                return null;

            return _mapper.Map<PagoDTO>(entity);
        }

        public async Task ConfirmarPagoAsync(int subastaId)
        {
            var pago = await _repositoryPago.FindBySubastaIdAsync(subastaId);

            if (pago == null)
                throw new KeyNotFoundException("No existe un pago asociado a esta subasta.");

            if (pago.EstadoPagoId == 2)
                return;

            pago.EstadoPagoId = 2;
            pago.FechaConfirmacion = DateTime.Now;

            await _repositoryPago.UpdateAsync(pago);
        }
    
}
}
