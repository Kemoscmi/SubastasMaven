using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public record EstadoSubastaDTO
    {
        [DisplayName("Identificador Estado Subasta")]
        public int EstadoSubastaId { get; set; }

        [DisplayName("Nombre del Estado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres")]
        public string NombreEstado { get; set; } = string.Empty;

 
        public List<SubastaDTO> Subasta { get; set; } = new();
    }
}

