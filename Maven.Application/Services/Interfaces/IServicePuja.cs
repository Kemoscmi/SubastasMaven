using Maven.Application.DTOs;
using Maven.Application.DTOs.Subasta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.Services.Interfaces
{
    public interface IServicePuja
    {
        Task<PujaDTO> RegistrarPujaAsync(RegistrarPujaDTO dto);


    }
}
