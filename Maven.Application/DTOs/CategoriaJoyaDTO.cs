using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public record CategoriaJoyaDTO
    {
        [DisplayName("Identificador Categoría")]
        public int CategoriaJoyaId { get; set; }

        [DisplayName("Nombre de la Categoría")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres")]
        public string Nombre { get; set; } = string.Empty;

      
    }
}

