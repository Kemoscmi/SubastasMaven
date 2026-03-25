namespace Maven.Application.DTOs.Subasta
{
    public record SubastaBorradorDTO
    {
        public int SubastaId { get; set; }
        public string Objeto { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal IncrementoMinimo { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int CantidadPujas { get; set; }
        public string? ImagenUrl { get; set; }
    }
}