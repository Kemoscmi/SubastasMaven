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
    public class CondicionObjetoProfile : Profile
    {
        public CondicionObjetoProfile()
        {
            // ENTIDAD → DTO
            CreateMap<CondicionObjeto, CondicionObjetoDTO>().ReverseMap();

            // DTO → ENTIDAD 
            CreateMap<CondicionObjetoDTO, CondicionObjeto>()
                .ForMember(d => d.Joya, o => o.Ignore());
        }
    }
}
