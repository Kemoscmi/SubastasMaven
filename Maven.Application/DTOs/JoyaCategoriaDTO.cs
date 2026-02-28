using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public record JoyaCategoriaDTO
    {
        [DisplayName("Joya")]
        [Required(ErrorMessage = "Debe seleccionar una {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        public int JoyaId { get; set; }

        [DisplayName("Categoría")]
        [Required(ErrorMessage = "Debe seleccionar una {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        public int CategoriaJoyaId { get; set; }

    
        public JoyaDTO Joya { get; set; } = new();
        public CategoriaJoyaDTO CategoriaJoya { get; set; } = new();
    }
}
