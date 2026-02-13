using Microsoft.AspNetCore.Mvc;
using Maven.Application.Services.Interfaces;
using Maven.Web.Models;

namespace Maven.Web.Controllers
{
    public class SubastaController : Controller
    {
        private readonly IServiceSubasta _service;

        public SubastaController(IServiceSubasta service)
        {
            _service = service;
        }

        public async Task<IActionResult> Activas()
        {
            var data = await _service.GetActivasAsync();
            return View(data);
        }

        public async Task<IActionResult> Finalizadas()
        {
            var data = await _service.GetFinalizadasAsync();
            return View(data);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var dto = await _service.GetDetalleAsync(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        public async Task<IActionResult> HistorialPujas(int id)
        {
            // id = SubastaId
            var data = await _service.GetHistorialPujasAsync(id, desc: true);
            ViewBag.SubastaId = id;
            return View(data);
        }



public async Task<IActionResult> SeleccionHistorial()
    {
        var activas = await _service.GetActivasAsync();
        var finalizadas = await _service.GetFinalizadasAsync();

        var lista = new List<SubastaHistorialSelectVm>();

        // Activas
        lista.AddRange(activas.Select(s => new SubastaHistorialSelectVm
        {
            SubastaId = s.SubastaId,
            Objeto = s.Objeto,
            ImagenUrl = s.ImagenUrl,
            Estado = "ACTIVA",
            FechaRef = s.FechaEstimadaCierre,
            CantidadPujas = s.CantidadPujas
        }));

        // Finalizadas
        lista.AddRange(finalizadas.Select(s => new SubastaHistorialSelectVm
        {
            SubastaId = s.SubastaId,
            Objeto = s.Objeto,
            ImagenUrl = s.ImagenUrl,
            Estado = s.EstadoFinal,
            FechaRef = s.FechaCierre,
            CantidadPujas = s.CantidadPujas
        }));

        // Orden bonitoooooo primero activas, luego por fecha
        lista = lista
            .OrderByDescending(x => x.Estado == "ACTIVA")
            .ThenByDescending(x => x.FechaRef)
            .ToList();

        return View(lista);
    }

}
}
