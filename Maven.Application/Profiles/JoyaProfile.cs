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
    public class JoyaProfile : Profile
    {
        public JoyaProfile()
        {
            // ENTIDAD -> DTO
            CreateMap<Joya, JoyaDTO>()
                .ForMember(d => d.Vendedor, o => o.MapFrom(s => s.Vendedor))
                .ForMember(d => d.EstadoObjeto, o => o.MapFrom(s => s.EstadoObjeto))
                .ForMember(d => d.CondicionObjeto, o => o.MapFrom(s => s.CondicionObjeto))

           
                .ForMember(d => d.ImagenPrincipal, o => o.MapFrom(s =>
                    s.JoyaImagen
                        .OrderBy(i => i.JoyaImagenId)
                        .Select(i => i.UrlImagen)
                        .FirstOrDefault() ?? string.Empty
                ))

             
                .ForMember(d => d.CategoriasTexto, o => o.MapFrom(s =>
                    (s.CategoriaJoya != null && s.CategoriaJoya.Any())
                        ? string.Join(", ", s.CategoriaJoya.Select(c => c.Nombre))
                        : "Sin categorías"
                ))

              
                .ForMember(d => d.JoyaImagen, o => o.Ignore())
                .ForMember(d => d.Subasta, o => o.Ignore())
                .ForMember(d => d.CategoriaJoya, o => o.Ignore());

            // DTO -> ENTIDAD
            CreateMap<JoyaDTO, Joya>()
                .ForMember(d => d.Vendedor, o => o.Ignore())
                .ForMember(d => d.EstadoObjeto, o => o.Ignore())
                .ForMember(d => d.CondicionObjeto, o => o.Ignore())
                .ForMember(d => d.JoyaImagen, o => o.Ignore())
                .ForMember(d => d.Subasta, o => o.Ignore())
                .ForMember(d => d.CategoriaJoya, o => o.Ignore());
        }
    }
}
