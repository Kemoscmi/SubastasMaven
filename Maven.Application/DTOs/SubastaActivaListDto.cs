namespace Maven.Application.DTOs
{
    public class SubastaActivaListDto
    {
        public int SubastaId { get; set; }
        public string Objeto { get; set; } = "";
        public string? ImagenUrl { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaEstimadaCierre { get; set; }
        public int CantidadPujas { get; set; } 
    }
}
