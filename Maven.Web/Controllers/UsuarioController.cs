using Maven.Application.DTOs;
using Maven.Application.Services.Implementations;
using Maven.Application.Services.Interfaces;
using Maven.Infraestructure.MavenData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create()
        {
            await CargarCombosAsync();
            return View(new UsuarioDTO());
        }

        // CREATE - POST
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioDTO dto)
        {
            ModelState.Remove("Rol.NombreRol");
            ModelState.Remove("EstadoUsuario.NombreEstado");
            ModelState.Remove("EstadoUsuarioId");
            if (!ModelState.IsValid)
            {
                await CargarCombosAsync();
                return View(dto);
            }

            try
            {
                await _service.AddAsync(dto);
                TempData["MensajeExito"] = "Usuario registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                await CargarCombosAsync();
                return View(dto);
            }
        }

        // EDIT - GET
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
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

            try
            {
                await _service.UpdateAsync(id, dto);
                TempData["DebugOk"] = $"Usuario {id} actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                await CargarCombosAsync();

                var usuarioActual = await _service.FindByIdAsync(id);
                dto.Rol = usuarioActual.Rol;
                dto.EstadoUsuario = usuarioActual.EstadoUsuario;
                dto.FechaRegistro = usuarioActual.FechaRegistro;

                return View(dto);
            }
        }
        // DELETE - GET 
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Combos Rol/Estado
        [Authorize(Roles = "ADMIN")]
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

        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
        [HttpPost, ActionName("Bloquear")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BloquearConfirmado(int id)
        {
        
            await _service.CambiarEstadoAsync(id, 2);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
        [HttpPost, ActionName("Activar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivarConfirmado(int id)
        {
            
            await _service.CambiarEstadoAsync(id, 1);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "ADMIN")]
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

        public async Task<IActionResult> MiPerfil()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return RedirectToAction("Index", "Login");

            var usuario = await _service.FindByIdAsync(int.Parse(userId));

            return View(usuario);
        }

        public async Task<IActionResult> EditarMiPerfil()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return RedirectToAction("Index", "Login");

            var usuario = await _service.FindByIdAsync(int.Parse(userId));

            return View(usuario);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarMiPerfil(UsuarioDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return RedirectToAction("Index", "Login");

            if (dto.UsuarioId != int.Parse(userId))
                return Forbid();
            ModelState.Remove("Rol.NombreRol");
            ModelState.Remove("EstadoUsuario.NombreEstado");

            if (!ModelState.IsValid)
                return View(dto);

            await _service.UpdateAsync(dto.UsuarioId, dto);


            TempData["MensajeExito"] = "Perfil actualizado correctamente.";
            return RedirectToAction("MiPerfil");
        }
    }
}
