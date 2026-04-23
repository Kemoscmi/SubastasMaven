using Maven.Application.DTOs.Reportes;

namespace Maven.Web.ViewModels
{
    public class ReporteCategoriasViewModel
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? GraficoBase64 { get; set; }
        public bool FiltroAplicado { get; set; }
        public ICollection<ReporteCategoriasDTO> Datos { get; set; } = new List<ReporteCategoriasDTO>();
    }
}