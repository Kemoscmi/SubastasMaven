using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public record JoyaDTO
    {

    
        public string ImagenPrincipal { get; set; } = string.Empty;
        public string CategoriasTexto { get; set; } = string.Empty;

        [DisplayName("Identificador Joya")]
        public int JoyaId { get; set; }

        [DisplayName("Vendedor")]
        [Required(ErrorMessage = "Debe seleccionar un {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int VendedorId { get; set; }

        [DisplayName("Nombre de la Joya")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(120, ErrorMessage = "{0} no puede exceder los {1} caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(1000, ErrorMessage = "{0} no puede exceder los {1} caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [DisplayName("Estado del Objeto")]
        [Required(ErrorMessage = "Debe seleccionar un {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int EstadoObjetoId { get; set; }

        [DisplayName("Condición del Objeto")]
        [Required(ErrorMessage = "Debe seleccionar un {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int CondicionObjetoId { get; set; }

        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

     
        public UsuarioDTO Vendedor { get; set; } = new();
        public EstadoObjetoDTO EstadoObjeto { get; set; } = new();
        public CondicionObjetoDTO CondicionObjeto { get; set; } = new();

       
        public List<JoyaImagenDTO> JoyaImagen { get; set; } = new();
        public List<SubastaDTO> Subasta { get; set; } = new();
        public List<CategoriaJoyaDTO> CategoriaJoya { get; set; } = new();
    }

}
