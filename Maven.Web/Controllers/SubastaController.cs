using Maven.Application.DTOs;
using Maven.Application.Services.Interfaces;
using Maven.Infraestructure.MavenData;
using Maven.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Maven.Web.Controllers
{
    public class SubastaController : Controller
    {
        private readonly IServiceSubasta _service;
        private readonly MavenContext _db;

        public SubastaController(IServiceSubasta service, MavenContext db)
        {
            _service = service;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.ListAsync();
            return View(data);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var data = await _service.FindByIdAsync(id);
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            await CargarCombosAsync();
            return View(new SubastaDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubastaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await CargarCombosAsync();
                return View(dto);
            }

            await _service.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.FindByIdAsync(id);
            await CargarCombosAsync();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubastaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await CargarCombosAsync();
                return View(dto);
            }

            await _service.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
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

        private async Task CargarCombosAsync()
        {
            var joyas = await _db.Joya.AsNoTracking()
                .OrderBy(j => j.Nombre)
                .ToListAsync();

            var vendedores = await _db.Usuario.AsNoTracking()
                .OrderBy(u => u.NombreCompleto)
                .ToListAsync();

            var estados = await _db.EstadoSubasta.AsNoTracking()
                .OrderBy(e => e.NombreEstado) // ajusta si el campo es diferente
                .ToListAsync();

            ViewBag.JoyaId = new SelectList(joyas, "JoyaId", "Nombre");
            ViewBag.VendedorId = new SelectList(vendedores, "UsuarioId", "NombreCompleto");
            ViewBag.EstadoSubastaId = new SelectList(estados, "EstadoSubastaId", "NombreEstado");
        }
    }
}