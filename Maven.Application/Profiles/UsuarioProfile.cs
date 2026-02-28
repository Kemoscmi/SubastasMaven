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
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(d => d.Rol, o => o.MapFrom(s => s.Rol))
                .ForMember(d => d.EstadoUsuario, o => o.MapFrom(s => s.EstadoUsuario));

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(d => d.UsuarioId, o => o.Ignore()) 
                .ForMember(d => d.Rol, o => o.Ignore())
                .ForMember(d => d.EstadoUsuario, o => o.Ignore());
        }
    }
}
