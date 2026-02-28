using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public record JoyaImagenDTO
    {
        [DisplayName("Identificador Imagen")]
        public int JoyaImagenId { get; set; }

        [DisplayName("Joya")]
        [Required(ErrorMessage = "Debe seleccionar una {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        public int JoyaId { get; set; }

        [DisplayName("URL de la Imagen")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(400, ErrorMessage = "{0} no puede exceder los {1} caracteres")]
        public string UrlImagen { get; set; } = string.Empty;

        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

     
        public JoyaDTO Joya { get; set; } = new();
    
}
}
