using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public record NotificacionDTO
    {
        [DisplayName("Identificador Notificación")]
        public long NotificacionId { get; set; }  

        [DisplayName("Usuario")]
        [Required(ErrorMessage = "Debe seleccionar un {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int UsuarioId { get; set; }

        [DisplayName("Subasta")]
        public int? SubastaId { get; set; }

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres")]
        public string Tipo { get; set; } = string.Empty;

        [DisplayName("Mensaje")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres")]
        public string Mensaje { get; set; } = string.Empty;

        [DisplayName("Leída")]
        public bool Leida { get; set; }

        [DisplayName("Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }

      
        public UsuarioDTO Usuario { get; set; } = new();
        public SubastaDTO? Subasta { get; set; } 
    
}
}
