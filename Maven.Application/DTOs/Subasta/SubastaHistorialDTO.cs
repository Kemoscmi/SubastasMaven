namespace Maven.Application.DTOs.Subasta
{
    public record SubastaHistorialDTO
    {
        public int SubastaId { get; set; }
        public string Objeto { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaRef { get; set; }
        public string? ImagenUrl { get; set; }
        public int CantidadPujas { get; set; }
    }
}