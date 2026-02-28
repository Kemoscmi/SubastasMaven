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
    public class EstadoUsuarioProfile : Profile
    {
        public EstadoUsuarioProfile()
        {
            CreateMap<EstadoUsuarioDTO, EstadoUsuario>().ReverseMap();
        }
    }
}
