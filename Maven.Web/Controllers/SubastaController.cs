using Maven.Application.DTOs;
using Maven.Application.DTOs.Subasta;
using Maven.Application.Services.Interfaces;
using Maven.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Maven.Web.Hubs;

namespace Maven.Web.Controllers
{
    [Authorize]
    public class SubastaController : Controller
    {
        private readonly IServiceSubasta _service;
        private readonly IServicePuja _servicePuja;
        private readonly IUsuarioActualService _usuarioActualService;
        private readonly IHubContext<SubastaHub> _hubContext;

        private const int UsuarioVendedorSimuladoId = 3;
        private const string UsuarioVendedorSimuladoNombre = "Daniel Rojas Solís";

        public SubastaController(
            IServiceSubasta service,
            IServicePuja servicePuja,
            IUsuarioActualService usuarioActualService,
            IHubContext<SubastaHub> hubContext)
        {
            _service = service;
            _servicePuja = servicePuja;
            _usuarioActualService = usuarioActualService;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.ListVisiblesAsync();
            return View(data);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var data = await _service.FindByIdAsync(id);
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var ahora = DateTime.Now.AddMinutes(20);

            var dto = new SubastaDTO
            {
                FechaInicio = ahora,
                FechaCierre = ahora.AddHours(2),
                EstadoSubastaId = 1,
                VendedorId = 3
            };

            var vm = await ConstruirSubastaFormVmAsync(dto);
            return View(vm);
        }

        private void LimpiarValidacionesSubasta()
        {
            var clavesAQuitar = ModelState.Keys
                .Where(k =>
                    k.StartsWith("Subasta.Joya") ||
                    k.StartsWith("Subasta.Vendedor") ||
                    k.StartsWith("Subasta.EstadoSubasta") ||
                    k.StartsWith("Subasta.Puja") ||
                    k.StartsWith("Subasta.SubastaResultado"))
                .ToList();

            foreach (var key in clavesAQuitar)
            {
                ModelState.Remove(key);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubastaFormViewModel vm)
        {
            vm.Subasta.VendedorId = UsuarioVendedorSimuladoId;
            vm.Subasta.EstadoSubastaId = 1;

            LimpiarValidacionesSubasta();

            if (!ModelState.IsValid)
            {
                var vmRecargado = await ConstruirSubastaFormVmAsync(vm.Subasta);
                ViewBag.UsuarioVendedorNombre = UsuarioVendedorSimuladoNombre;
                return View(vmRecargado);
            }

            try
            {
                var nuevoId = await _service.AddAsync(vm.Subasta);
                TempData["SuccessMessage"] = "Subasta guardada correctamente.";
                return RedirectToAction(nameof(Borradores));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var vmRecargado = await ConstruirSubastaFormVmAsync(vm.Subasta);
                ViewBag.UsuarioVendedorNombre = UsuarioVendedorSimuladoNombre;
                return View(vmRecargado);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.FindByIdAsync(id);
            var vm = await ConstruirSubastaFormVmAsync(dto);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubastaFormViewModel vm)
        {
            LimpiarValidacionesSubasta();

            if (!ModelState.IsValid)
            {
                var vmRecargado = await ConstruirSubastaFormVmAsync(vm.Subasta);
                return View(vmRecargado);
            }

            try
            {
                await _service.UpdateAsync(vm.Subasta.SubastaId, vm.Subasta);
                TempData["SuccessMessage"] = "Subasta actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var vmRecargado = await ConstruirSubastaFormVmAsync(vm.Subasta);
                return View(vmRecargado);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _service.FindByIdAsync(id);
            return View(dto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
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

        public async Task<IActionResult> DetalleVisual(int id, string? origen)
        {
            var data = await _service.GetDetalleVisualAsync(id);
            ViewBag.Origen = origen;
            return View(data);
        }

        public async Task<IActionResult> HistorialPujas(int id, string? origen, bool desdeDetalle = false)
        {
            var data = await _service.GetHistorialPujasAsync(id);
            ViewBag.Origen = origen;
            ViewBag.SubastaId = id;
            ViewBag.DesdeDetalle = desdeDetalle;
            return View(data);
        }

        private async Task<SubastaFormViewModel> ConstruirSubastaFormVmAsync(SubastaDTO? dto = null)
        {
            var combos = await _service.GetCombosAsync();

            return new SubastaFormViewModel
            {
                Subasta = dto ?? new SubastaDTO(),

                Joyas = combos.Joyas.Select(j => new SelectListItem
                {
                    Value = j.Id.ToString(),
                    Text = j.Nombre
                }),

                Vendedores = combos.Vendedores.Select(v => new SelectListItem
                {
                    Value = v.Id.ToString(),
                    Text = v.Nombre
                }),

                EstadosSubasta = combos.EstadosSubasta.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nombre
                })
            };
        }

        public async Task<IActionResult> Borradores()
        {
            var data = await _service.GetBorradoresByVendedorAsync(UsuarioVendedorSimuladoId);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Publicar(int id)
        {
            try
            {
                await _service.PublicarAsync(id);
                TempData["SuccessMessage"] = "Subasta publicada correctamente";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Borradores));
        }

        [HttpPost]
        public async Task<IActionResult> Cancelar(int id)
        {
            try
            {
                await _service.CancelarAsync(id);
                TempData["SuccessMessage"] = "Subasta cancelada correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarPuja(RegistrarPujaDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errores = ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value!.Errors.Select(e => $"{x.Key}: {e.ErrorMessage}"))
                        .ToList();

                    return BadRequest(new
                    {
                        ok = false,
                        mensaje = string.Join(" | ", errores)
                    });
                }

                var detalleAntes = await _service.GetDetalleVisualAsync(dto.SubastaId);

                var liderAnteriorId = detalleAntes.UsuarioLiderId;

                var pujaRegistrada = await _servicePuja.RegistrarPujaAsync(dto);

                var detalleDespues = await _service.GetDetalleVisualAsync(dto.SubastaId);

                var usuarioActualId = _usuarioActualService.GetUsuarioId();
                var nombreUsuarioActual = _usuarioActualService.GetNombreUsuario() ?? "Usuario actual";

                await _hubContext.Clients.Group($"subasta-{dto.SubastaId}")
                    .SendAsync("PujaRegistrada", new
                    {
                        usuario = nombreUsuarioActual,
                        montoOfertado = dto.MontoOfertado,
                        fechaHora = pujaRegistrada.FechaHora,
                        cantidadPujas = detalleDespues.CantidadPujas
                    });

                await _hubContext.Clients.Group($"subasta-{dto.SubastaId}")
                    .SendAsync("LiderActualizado", new
                    {
                        usuario = detalleDespues.UsuarioLider,
                        montoActual = detalleDespues.PujaActual
                    });

                if (liderAnteriorId.HasValue &&
                    liderAnteriorId.Value != usuarioActualId &&
                    liderAnteriorId.Value != detalleDespues.UsuarioLiderId)
                {
                    await _hubContext.Clients.Group($"usuario-{liderAnteriorId.Value}")
                    .SendAsync("PujaSuperada", new
                    {
                        mensaje = "⚠️ Tu puja ha sido superada."
                    });
                }

                return Json(new
                {
                    ok = true,
                    mensaje = "Puja registrada correctamente."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ok = false,
                    mensaje = ex.Message
                });
            }
        }
    }
}