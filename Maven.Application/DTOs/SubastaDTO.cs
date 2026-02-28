using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public record SubastaDTO
    {
        [DisplayName("Identificador Subasta")]
        public int SubastaId { get; set; }

        [DisplayName("Joya")]
        [Required(ErrorMessage = "Debe seleccionar una {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        public int JoyaId { get; set; }

        [DisplayName("Vendedor")]
        [Required(ErrorMessage = "Debe seleccionar un {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int VendedorId { get; set; }

        [DisplayName("Fecha de Inicio")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public DateTime FechaInicio { get; set; }

        [DisplayName("Fecha de Cierre")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public DateTime FechaCierre { get; set; }

        [DisplayName("Precio Base")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "El {0} debe ser mayor a 0")]
        public decimal PrecioBase { get; set; }

        [DisplayName("Incremento Mínimo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "El {0} debe ser mayor a 0")]
        public decimal IncrementoMinimo { get; set; }

        [DisplayName("Estado de la Subasta")]
        [Required(ErrorMessage = "Debe seleccionar un {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int EstadoSubastaId { get; set; }

        [DisplayName("Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }

        [DisplayName("Fecha de Publicación")]
        public DateTime? FechaPublicacion { get; set; } 

       
        public JoyaDTO Joya { get; set; } = new();
        public UsuarioDTO Vendedor { get; set; } = new();
        public EstadoSubastaDTO EstadoSubasta { get; set; } = new();

       
        public List<PujaDTO> Puja { get; set; } = new();
        public SubastaResultadoDTO? SubastaResultado { get; set; }
    
}
}
