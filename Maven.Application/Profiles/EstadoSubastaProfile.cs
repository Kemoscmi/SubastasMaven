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
    public class EstadoSubastaProfile : Profile
    {
        public EstadoSubastaProfile()
        {
            // ENTIDAD → DTO
            CreateMap<EstadoSubasta, EstadoSubastaDTO>().ReverseMap();

         
            CreateMap<EstadoSubastaDTO, EstadoSubasta>()
                .ForMember(d => d.Subasta, o => o.Ignore());
        }

    }
}
