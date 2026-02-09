using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.DTOs
{
    public class JoyaSubastaHistorialDto
    {
        public int SubastaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCierre { get; set; }
        public string EstadoSubasta { get; set; } = "";
    }
}
