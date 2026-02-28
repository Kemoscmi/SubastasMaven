using AutoMapper;
using Maven.Application.DTOs;
using Maven.Infraestructure.MavenModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.Profiles
{
    public class EstadoPagoProfile : Profile
    {
        public EstadoPagoProfile()
        {
            // ENTIDAD → DTO
            CreateMap<EstadoPago, EstadoPagoDTO>().ReverseMap();

           
            CreateMap<EstadoPagoDTO, EstadoPago>()
                .ForMember(d => d.Pago, o => o.Ignore());
        }
    }
}
