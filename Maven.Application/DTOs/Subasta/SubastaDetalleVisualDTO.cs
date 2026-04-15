namespace Maven.Application.DTOs.Subasta
{
    public record SubastaDetalleVisualDTO
    {
        public int SubastaId { get; set; }

        public string NombreJoya { get; set; } = string.Empty;
        public string DescripcionJoya { get; set; } = string.Empty;
        public string? Condicion { get; set; }
        public List<string> Categorias { get; set; } = new();

        public DateTime FechaInicio { get; set; }
        public DateTime FechaCierre { get; set; }

        public decimal PrecioBase { get; set; }
        public decimal IncrementoMinimo { get; set; }

        public string EstadoActual { get; set; } = string.Empty;
        public int CantidadPujas { get; set; }
        public string Vendedor { get; set; } = string.Empty;
        public string? ImagenUrl { get; set; }

        public decimal PujaActual { get; set; }
        public string UsuarioLider { get; set; } = "Sin usuario líder";
        public bool Finalizada { get; set; }

        public List<PujaHistorialDTO> HistorialPujas { get; set; } = new();
        public bool TienePujas { get; set; }
        public int? UsuarioLiderId { get; set; }
    }
}