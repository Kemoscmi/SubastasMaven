using Maven.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.Services.Interfaces
{
    public interface IServicePago
    {
        Task<PagoDTO?> FindBySubastaIdAsync(int subastaId);
        Task ConfirmarPagoAsync(int subastaId);
    }
}
