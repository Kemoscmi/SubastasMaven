using Maven.Application.Services.Interfaces;
using Maven.Web.Documents;
using Maven.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Maven.Application.DTOs.Reportes;

namespace Maven.Web.Controllers
{
    public class ReporteController : Controller
    {
        private readonly IServiceSubasta _serviceSubasta;

        public ReporteController(IServiceSubasta serviceSubasta)
        {
            _serviceSubasta = serviceSubasta;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SubastasFinalizadasPdf(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var inicio = fechaInicio ?? DateTime.Today.AddMonths(-1);
            var fin = fechaFin ?? DateTime.Today;

            var data = await _serviceSubasta.GetReporteFinalizadasAsync(inicio, fin);

            var document = new ReporteSubastasFinalizadasDocument(data, inicio, fin);
            var pdf = document.GeneratePdf();

            return File(pdf, "application/pdf", "Reporte_Subastas_Finalizadas.pdf");
        }

        [HttpGet]
        public async Task<IActionResult> Categorias(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var vm = new ReporteCategoriasViewModel
            {
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                Datos = new List<ReporteCategoriasDTO>(),
                FiltroAplicado = fechaInicio.HasValue && fechaFin.HasValue
            };

            if (vm.FiltroAplicado)
            {
                vm.Datos = await _serviceSubasta.GetReporteCategoriasAsync(fechaInicio!.Value, fechaFin!.Value);
            }

            return View(vm);
        }


        public IActionResult SubastasFinalizadas()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CategoriasPdf(ReporteCategoriasViewModel model)
        {
            if (!model.FechaInicio.HasValue || !model.FechaFin.HasValue)
                return BadRequest("Debe indicar el rango de fechas.");

            var data = await _serviceSubasta.GetReporteCategoriasAsync(model.FechaInicio.Value, model.FechaFin.Value);

            model.Datos = data;

            var document = new ReporteCategoriasDocument(
                model.Datos,
                model.FechaInicio.Value,
                model.FechaFin.Value,
                model.GraficoBase64
            );

            var pdf = document.GeneratePdf();

            return File(pdf, "application/pdf", "Reporte_Categorias.pdf");
        }
    }
}