using Maven.Application.DTOs;
using Maven.Application.Services.Interfaces;
using Maven.Infraestructure.MavenData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Maven.Web.Controllers
{
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
        public async Task<IActionResult> Index()
        {
            var data = await _service.ListAsync();
            return View(data);
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

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await CargarCombosAsync();
                return View(dto);
            }

            await _service.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        // DELETE - GET (confirmación)
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
    }
}
