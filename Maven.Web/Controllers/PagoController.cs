using Maven.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maven.Web.Controllers
{
    [Authorize]
    public class PagoController : Controller
    {
        private readonly IServicePago _servicePago;

        public PagoController(IServicePago servicePago)
        {
            _servicePago = servicePago;
        }

        public async Task<IActionResult> Detalle(int subastaId)
        {
            if (subastaId <= 0)
                return BadRequest();

            var pago = await _servicePago.FindBySubastaIdAsync(subastaId);

            if (pago == null)
                return NotFound();

            return View(pago);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirmar(int subastaId)
        {
            if (subastaId <= 0)
                return BadRequest();

            await _servicePago.ConfirmarPagoAsync(subastaId);

            return RedirectToAction(nameof(Detalle), new { subastaId });
        }
    
}
}
