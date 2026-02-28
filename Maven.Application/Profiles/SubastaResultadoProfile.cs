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
    public class SubastaResultadoProfile : Profile
    {
        public SubastaResultadoProfile()
        {
            // ENTIDAD DTO (lectura)
            CreateMap<SubastaResultado, SubastaResultadoDTO>()
                .ForMember(d => d.Subasta, o => o.MapFrom(s => s.Subasta));

            // DTO  ENTIDAD (crear/editar)
            CreateMap<SubastaResultadoDTO, SubastaResultado>()
                .ForMember(d => d.Subasta, o => o.Ignore());
        }
    }
}
