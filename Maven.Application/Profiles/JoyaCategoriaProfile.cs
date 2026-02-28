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
    public class JoyaCategoriaProfile : Profile
    {
        public JoyaCategoriaProfile()
        {
            // ENTIDAD → DTO
            CreateMap<JoyaCategoria, JoyaCategoriaDTO>()
                .ForMember(d => d.Joya, o => o.MapFrom(s => s.Joya))
                .ForMember(d => d.CategoriaJoya, o => o.MapFrom(s => s.CategoriaJoya));

            // DTO → ENTIDAD
            CreateMap<JoyaCategoriaDTO, JoyaCategoria>()
                .ForMember(d => d.Joya, o => o.Ignore())
                .ForMember(d => d.CategoriaJoya, o => o.Ignore());
        }
    }
}
