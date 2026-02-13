namespace Maven.Web.Models
{
    public class SubastaHistorialSelectVm
    {
        public int SubastaId { get; set; }
        public string Objeto { get; set; } = "";
        public string Estado { get; set; } = ""; // ACTIVA / FINALIZADA / CANCELADA
        public DateTime FechaRef { get; set; }   // cierre o inicio, para mostrar
        public string? ImagenUrl { get; set; }
        public int CantidadPujas { get; set; }
    }
}
