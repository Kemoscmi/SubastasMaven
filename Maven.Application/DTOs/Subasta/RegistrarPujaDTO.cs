using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs.Subasta
{
    public class RegistrarPujaDTO
    {
        [DisplayName("Subasta")]
        [Required(ErrorMessage = "La subasta es requerida")]
        public int SubastaId { get; set; }

        [DisplayName("Monto ofertado")]
        [Required(ErrorMessage = "El monto ofertado es requerido")]
        [Range(typeof(decimal), "0.01", "999999999999.99", ErrorMessage = "El monto debe ser mayor a cero")]
        public decimal MontoOfertado { get; set; }
    }
}
