using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public record CondicionObjetoDTO
    {
        [DisplayName("Identificador Condición")]
        public int CondicionObjetoId { get; set; }

        [DisplayName("Nombre de la Condición")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres")]
        public string NombreCondicion { get; set; } = string.Empty;

      
        public List<JoyaDTO> Joya { get; set; } = new();
    
}
}
