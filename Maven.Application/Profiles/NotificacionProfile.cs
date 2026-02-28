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
    public class NotificacionProfile : Profile  
    {
        public NotificacionProfile()
        {
            // ENTIDAD → DTO (lectura)
            CreateMap<Notificacion, NotificacionDTO>()
                .ForMember(d => d.Usuario, o => o.MapFrom(s => s.Usuario))
                .ForMember(d => d.Subasta, o => o.MapFrom(s => s.Subasta));

            // DTO → ENTIDAD (crear/editar)
            CreateMap<NotificacionDTO, Notificacion>()
                .ForMember(d => d.Usuario, o => o.Ignore())
                .ForMember(d => d.Subasta, o => o.Ignore());
        }
    }
}
