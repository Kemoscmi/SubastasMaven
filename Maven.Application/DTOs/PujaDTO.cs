using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public record PujaDTO
    {
        [DisplayName("Identificador Puja")]
        public long PujaId { get; set; } 

        [DisplayName("Subasta")]
        [Required(ErrorMessage = "Debe seleccionar una {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        public int SubastaId { get; set; }

        [DisplayName("Comprador")]
        [Required(ErrorMessage = "Debe seleccionar un {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int CompradorId { get; set; }

        [DisplayName("Monto Ofertado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(typeof(decimal), "0.01", "999999999999", ErrorMessage = "El {0} debe ser mayor a 0")]
        public decimal MontoOfertado { get; set; }

        [DisplayName("Fecha y Hora")]
        public DateTime FechaHora { get; set; }

       
        public SubastaDTO Subasta { get; set; } = new();
        public UsuarioDTO Comprador { get; set; } = new();
    
}
}
