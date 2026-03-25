namespace Maven.Application.DTOs.Subasta
{
    public record SubastaListadoDTO
    {
        public int SubastaId { get; set; }
        public string Joya { get; set; } = string.Empty;
        public string Vendedor { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal IncrementoMinimo { get; set; }
    }
}