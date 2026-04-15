namespace Maven.Application.DTOs.Subasta
{
    public class SubastaCierreTiempoRealDTO
    {
        public int SubastaId { get; set; }
        public string Estado { get; set; } = "FINALIZADA";
        public string? UsuarioGanador { get; set; }
        public decimal? MontoFinal { get; set; }
        public bool SinPujas { get; set; }
    }
}