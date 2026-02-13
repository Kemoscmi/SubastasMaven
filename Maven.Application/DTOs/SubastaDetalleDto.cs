namespace Maven.Application.DTOs
{
    public class SubastaDetalleDto
    {
        // Subasta
        public int SubastaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal IncrementoMinimo { get; set; }
        public string EstadoActual { get; set; } = "";
        public int CantidadTotalPujas { get; set; } 

        // Objeto (Joya)
        public int JoyaId { get; set; }
        public string NombreObjeto { get; set; } = "";
        public string Condicion { get; set; } = "";
        public List<string> Categorias { get; set; } = new();
        public string? ImagenUrl { get; set; }
    }
}
