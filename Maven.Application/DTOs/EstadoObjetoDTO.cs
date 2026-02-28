using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public record EstadoObjetoDTO
    {
        [DisplayName("Identificador Estado Objeto")]
        public int EstadoObjetoId { get; set; }

        [DisplayName("Nombre del Estado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres")]
        public string NombreEstado { get; set; } = string.Empty;

       
        public List<JoyaDTO> Joya { get; set; } = new();
    
}
}
