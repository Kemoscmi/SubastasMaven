using Maven.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Maven.Web.Models
{
    public class SubastaFormViewModel
    {
        public SubastaDTO Subasta { get; set; } = new();

        public IEnumerable<SelectListItem> Joyas { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Vendedores { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> EstadosSubasta { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}