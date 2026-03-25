namespace Maven.Application.DTOs.Subasta
{
    public record PujaHistorialDTO
    {
        public long PujaId { get; set; }
        public int SubastaId { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public decimal MontoOfertado { get; set; }
        public DateTime FechaHora { get; set; }
    }
}