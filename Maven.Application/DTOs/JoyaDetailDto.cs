using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public class JoyaDetailDto
    {
        public int JoyaId { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Categorias { get; set; } = "";
        public string Condicion { get; set; } = "";
        public string Estado { get; set; } = "";
        public DateTime FechaRegistro { get; set; }

        public string Dueno { get; set; } = ""; // vendedor
        public List<string> Imagenes { get; set; } = new();

        // ✅ LINQ
        public List<JoyaSubastaHistorialDto> HistorialSubastas { get; set; } = new();
    }
}

