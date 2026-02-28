using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public record SubastaResultadoDTO
    {
        [DisplayName("Subasta")]
        [Required(ErrorMessage = "Debe seleccionar una {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        public int SubastaId { get; set; }

        [DisplayName("Ganador")]
        public int? GanadorId { get; set; }

        [DisplayName("Puja Ganadora")]
        public long? PujaGanadoraId { get; set; } 

        [DisplayName("Monto Final")]
        public decimal? MontoFinal { get; set; }

        [DisplayName("Fecha de Cierre")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public DateTime FechaCierre { get; set; }

     
        public SubastaDTO Subasta { get; set; } = new();

     
    
}
}
