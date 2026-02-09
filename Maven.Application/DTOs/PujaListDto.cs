namespace Maven.Application.DTOs
{
    public class PujaListDto
    {
        public long PujaId { get; set; }
        public int SubastaId { get; set; }

        public string Usuario { get; set; } = "";
        public decimal MontoOfertado { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
