using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Maven.Web.ViewModels
{
    public class ViewModelLogin
    {
        [DisplayName("Correo")]
        [Required(ErrorMessage = "El correo es requerido")]
        public string Correo { get; set; } = string.Empty;

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string? ReturnUrl { get; set; }
    }
}