namespace Maven.Application.DTOs
{
    public class SubastaFinalizadaListDto
    {
        public int SubastaId { get; set; }
        public string Objeto { get; set; } = "";
        public string? ImagenUrl { get; set; }
        public DateTime FechaCierre { get; set; }
        public string EstadoFinal { get; set; } = ""; 
        public int CantidadPujas { get; set; } 
    }
}
