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
    public class EstadoObjetoProfile :Profile
    {
        public EstadoObjetoProfile()
        {
            // ENTIDAD → DTO
            CreateMap<EstadoObjeto, EstadoObjetoDTO>().ReverseMap();

          
            CreateMap<EstadoObjetoDTO, EstadoObjeto>()
                .ForMember(d => d.Joya, o => o.Ignore());
        }
    }
}
