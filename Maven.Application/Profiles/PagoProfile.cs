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
    public class PagoProfile : Profile
    {
        public PagoProfile()
        {
            // ENTIDAD → DTO (lectura)
            CreateMap<Pago, PagoDTO>()
                .ForMember(d => d.Subasta, o => o.MapFrom(s => s.Subasta))
                .ForMember(d => d.EstadoPago, o => o.MapFrom(s => s.EstadoPago));

            // DTO → ENTIDAD (crear/editar)
            CreateMap<PagoDTO, Pago>()
                .ForMember(d => d.Subasta, o => o.Ignore())
                .ForMember(d => d.EstadoPago, o => o.Ignore());
        }
    }

}
