using Maven.Application.DTOs;
using Maven.Application.Services.Interfaces;
using Maven.Infraestructure.MavenData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace Maven.Web.Controllers
{
   

    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IServiceUsuario _service;
        private readonly MavenContext _db;

        public UsuarioController(IServiceUsuario service, MavenContext db)
        {
            _service = service;
            _db = db;
        }

        // LISTAR
        public async Task<IActionResult> Index(string? filtro)
        {
            var data = await _service.ListAsync();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.Trim().ToUpperInvariant();

                if (filtro == "ACTIVOS")
                {
                    data = data
                        .Where(u => (u.EstadoUsuario?.NombreEstado ?? "")
                            .Trim()
                            .ToUpperInvariant() == "ACTIVO")
                        .ToList();
                }
                else if (filtro == "BLOQUEADO")
                {
                    data = data
                        .Where(u => (u.EstadoUsuario?.NombreEstado ?? "")
                            .Trim()
                            .ToUpperInvariant() == "BLOQUEADO")
                        .ToList();
                }
            }

            ViewBag.FiltroActual = filtro;
            return View(data.ToList());
        }

        // DETALLE
        public async Task<IActionResult> Detalle(int id)
        {
            try
            {
                var data = await _service.FindByIdAsync(id);
                return View(data);
            }
            catch
            {
                return NotFound();
            }
        }

        // CREATE - GET
        public async Task<IActionResult> Create()
        {
            await CargarCombosAsync();
            return View(new UsuarioDTO());
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await CargarCombosAsync();
                return View(dto);
            }

            await _service.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // EDIT - GET
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var dto = await _service.FindByIdAsync(id);
                await CargarCombosAsync();
                return View(dto);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioDTO dto)
        {
            if (id != dto.UsuarioId)
                return BadRequest();

            // Quitar validaciones 
            ModelState.Remove("PasswordHash");
            ModelState.Remove("RolId");
            ModelState.Remove("EstadoUsuarioId");
            ModelState.Remove("FechaRegistro");
            ModelState.Remove("Rol");
            ModelState.Remove("EstadoUsuario");

 
            ModelState.Remove("Rol.RolId");
            ModelState.Remove("Rol.NombreRol");
            ModelState.Remove("EstadoUsuario.EstadoUsuarioId");
            ModelState.Remove("EstadoUsuario.NombreEstado");

            TempData["Debug"] = $"Entró al POST - ID: {id}";
            if (!ModelState.IsValid)
            {
                var errores = ModelState
                    .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors.Select(e => $"{x.Key}: {e.ErrorMessage}"))
                    .ToList();

                ViewBag.ErroresDebug = errores;

                var usuarioActual = await _service.FindByIdAsync(id);
                dto.Rol = usuarioActual.Rol;
                dto.EstadoUsuario = usuarioActual.EstadoUsuario;
                dto.FechaRegistro = usuarioActual.FechaRegistro;

                return View(dto);
            }

            await _service.UpdateAsync(id, dto);

            TempData["DebugOk"] = $"Usuario {id} actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        // DELETE - GET 
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dto = await _service.FindByIdAsync(id);
                return View(dto);
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Combos Rol/Estado
        private async Task CargarCombosAsync()
        {
            var roles = await _db.Rol.AsNoTracking()
                .OrderBy(r => r.NombreRol)
                .ToListAsync();

            var estados = await _db.EstadoUsuario.AsNoTracking()
                .OrderBy(e => e.NombreEstado)   
                .ToListAsync();

            ViewBag.RolId = new SelectList(roles, "RolId", "NombreRol");
            ViewBag.EstadoUsuarioId = new SelectList(estados, "EstadoUsuarioId", "NombreEstado");
        }


        public async Task<IActionResult> Bloquear(int id)
        {
            try
            {
                var dto = await _service.FindByIdAsync(id);
                return View(dto);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Bloquear")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BloquearConfirmado(int id)
        {
        
            await _service.CambiarEstadoAsync(id, 2);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activar(int id)
        {
            try
            {
                var dto = await _service.FindByIdAsync(id);
                return View(dto);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Activar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivarConfirmado(int id)
        {
            
            await _service.CambiarEstadoAsync(id, 1);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstado(int id)
        {
            try
            {
                var usuario = await _service.FindByIdAsync(id);

                var estadoActual = (usuario.EstadoUsuario?.NombreEstado ?? "")
                    .Trim()
                    .ToUpperInvariant();

                int nuevoEstadoId;

             

                if (estadoActual == "ACTIVO")
                    nuevoEstadoId = 2;
                else
                    nuevoEstadoId = 1;

                await _service.CambiarEstadoAsync(id, nuevoEstadoId);

                TempData["DebugOk"] = "Estado del usuario actualizado correctamente.";
                return RedirectToAction(nameof(Detalle), new { id });
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
