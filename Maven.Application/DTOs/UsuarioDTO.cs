using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public record UsuarioDTO
    {
        [DisplayName("Identificador Usuario")]
        public int UsuarioId { get; set; }

        [DisplayName("Correo Electrónico")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(120, ErrorMessage = "{0} no puede exceder los {1} caracteres")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Correo { get; set; } = string.Empty;

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(255, ErrorMessage = "{0} no puede exceder los {1} caracteres")]
        public string PasswordHash { get; set; } = string.Empty;

        [DisplayName("Nombre Completo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(120, ErrorMessage = "{0} no puede exceder los {1} caracteres")]
        public string NombreCompleto { get; set; } = string.Empty;

        [DisplayName("Rol")]
        [Required(ErrorMessage = "Debe seleccionar un {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int RolId { get; set; }

        [DisplayName("Estado Usuario")]
        [Required(ErrorMessage = "Debe seleccionar un {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int EstadoUsuarioId { get; set; }

        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }


        public RolDTO Rol { get; set; } = new();

        public EstadoUsuarioDTO EstadoUsuario { get; set; } = new();


        public int CantidadSubastasCreadas { get; set; }
        public int CantidadPujasRealizadas { get; set; }
    }
}

