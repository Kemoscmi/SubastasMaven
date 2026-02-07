namespace Maven.Application.DTOs
{
    public class UsuarioListDto
    {
        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; } = "";
        public string Correo { get; set; } = "";
        public string Rol { get; set; } = "";
        public string Estado { get; set; } = "";
        public DateTime FechaRegistro { get; set; }
    }
}
