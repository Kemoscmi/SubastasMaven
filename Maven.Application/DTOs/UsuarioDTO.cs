using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Maven.Application.DTOs
{
    public record UsuarioDTO : IValidatableObject
    {
        [DisplayName("Identificador Usuario")]
        public int UsuarioId { get; set; }

        [DisplayName("Correo Electrónico")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(120, ErrorMessage = "{0} no puede exceder los {1} caracteres")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Correo { get; set; } = string.Empty;

        [DisplayName("Contraseña")]
        [StringLength(255, ErrorMessage = "{0} no puede exceder los {1} caracteres")]
        public string? PasswordHash { get; set; }

        [DisplayName("Nombre Completo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(120, ErrorMessage = "{0} no puede exceder los {1} caracteres")]
        public string NombreCompleto { get; set; } = string.Empty;

        [DisplayName("Rol")]
        public int? RolId { get; set; }

        [DisplayName("Estado Usuario")]
        public int? EstadoUsuarioId { get; set; }

        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

        public RolDTO Rol { get; set; } = new();
        public EstadoUsuarioDTO EstadoUsuario { get; set; } = new();

        public int CantidadSubastasCreadas { get; set; }
        public int CantidadPujasRealizadas { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool esCreacion = UsuarioId == 0;

            if (esCreacion)
            {
                if (string.IsNullOrWhiteSpace(PasswordHash))
                {
                    yield return new ValidationResult(
                        "La Contraseña es un dato requerido",
                        new[] { nameof(PasswordHash) });
                }

                if (!RolId.HasValue || RolId <= 0)
                {
                    yield return new ValidationResult(
                        "Debe seleccionar un Rol",
                        new[] { nameof(RolId) });
                }

                if (!EstadoUsuarioId.HasValue || EstadoUsuarioId <= 0)
                {
                    yield return new ValidationResult(
                        "Debe seleccionar un Estado Usuario",
                        new[] { nameof(EstadoUsuarioId) });
                }
            }
        }
    }
}