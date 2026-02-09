using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public class UsuarioDetailDto
    {
        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; } = "";
        public string Correo { get; set; } = "";
        public string Rol { get; set; } = "";
        public string Estado { get; set; } = "";
        public DateTime FechaRegistro { get; set; }

        // CAMPOS CALCULADOS (LINQ)
        public int CantidadSubastasCreadas { get; set; }
        public int CantidadPujasRealizadas { get; set; }
    }
}

