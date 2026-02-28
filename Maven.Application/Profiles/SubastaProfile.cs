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
    public class SubastaProfile :Profile
    {
        public SubastaProfile()
        {
            // ENTIDAD -> DTO
            CreateMap<Subasta, SubastaDTO>()
                .ForMember(d => d.Joya, o => o.MapFrom(s => s.Joya))
                .ForMember(d => d.Vendedor, o => o.MapFrom(s => s.Vendedor))
                .ForMember(d => d.EstadoSubasta, o => o.MapFrom(s => s.EstadoSubasta))

          
                .ForMember(d => d.Puja, o => o.Ignore())
                .ForMember(d => d.SubastaResultado, o => o.Ignore());

            // DTO -> ENTIDAD
            CreateMap<SubastaDTO, Subasta>()
                .ForMember(d => d.Joya, o => o.Ignore())
                .ForMember(d => d.Vendedor, o => o.Ignore())
                .ForMember(d => d.EstadoSubasta, o => o.Ignore())
                .ForMember(d => d.Puja, o => o.Ignore())
                .ForMember(d => d.SubastaResultado, o => o.Ignore());
        }
    }
}
