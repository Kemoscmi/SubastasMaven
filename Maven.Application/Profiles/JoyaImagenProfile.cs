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
    public class JoyaImagenProfile :Profile
    {
        public JoyaImagenProfile()
        {
            // ENTIDAD → DTO
            CreateMap<JoyaImagen, JoyaImagenDTO>()
                .ForMember(d => d.Joya, o => o.MapFrom(s => s.Joya));

            // DTO → ENTIDAD
            CreateMap<JoyaImagenDTO, JoyaImagen>()
                .ForMember(d => d.Joya, o => o.Ignore());
        }
    }
}
