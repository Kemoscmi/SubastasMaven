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
    public class PujaProfile : Profile
    {
        public PujaProfile()
        {
            // ENTIDAD → DTO
            CreateMap<Puja, PujaDTO>()
                .ForMember(d => d.Subasta, o => o.MapFrom(s => s.Subasta))
                .ForMember(d => d.Comprador, o => o.MapFrom(s => s.Comprador));

            // DTO → ENTIDAD
            CreateMap<PujaDTO, Puja>()
                .ForMember(d => d.Subasta, o => o.Ignore())
                .ForMember(d => d.Comprador, o => o.Ignore());
        }
    }
}
