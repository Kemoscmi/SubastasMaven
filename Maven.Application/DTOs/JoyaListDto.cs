using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public class JoyaListDto
    {
        public int JoyaId { get; set; }
        public string Nombre { get; set; } = "";
        public string ImagenPrincipal { get; set; } = "";
        public string Categorias { get; set; } = "";
        public string Condicion { get; set; } = "";
        public string Estado { get; set; } = "";
    }
}
