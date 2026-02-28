using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public record PagoDTO
    {
        [DisplayName("Subasta")]
        [Required(ErrorMessage = "Debe seleccionar una {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        public int SubastaId { get; set; }

        [DisplayName("Comprador")]
        [Required(ErrorMessage = "Debe seleccionar un {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int CompradorId { get; set; }

        [DisplayName("Vendedor")]
        [Required(ErrorMessage = "Debe seleccionar un {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int VendedorId { get; set; }

        [DisplayName("Monto")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(typeof(decimal), "0.01", "999999999999", ErrorMessage = "El {0} debe ser mayor a 0")]
        public decimal Monto { get; set; }

        [DisplayName("Estado del Pago")]
        [Required(ErrorMessage = "Debe seleccionar un {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int EstadoPagoId { get; set; }

        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

        [DisplayName("Fecha de Confirmación")]
        public DateTime? FechaConfirmacion { get; set; }

       
        public SubastaDTO Subasta { get; set; } = new();
        public EstadoPagoDTO EstadoPago { get; set; } = new();
    }
}

