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

        public async Task<IActionResult> Activas()
        {
            var data = await _db.Subasta
                .AsNoTracking()
                .Include(s => s.Joya)
                .Include(s => s.EstadoSubasta)
                .Where(s => s.EstadoSubastaId == 3) // ACTIVA
                .OrderBy(s => s.FechaCierre)
                .Select(s => new SubastaHistorialSelectVm
                {
                    SubastaId = s.SubastaId,
                    Objeto = s.Joya.Nombre,
                    Estado = s.EstadoSubasta.NombreEstado,
                    FechaRef = s.FechaCierre, // “fecha estimada de cierre”
                    ImagenUrl = _db.JoyaImagen
                        .Where(img => img.JoyaId == s.JoyaId)
                        .OrderByDescending(img => img.FechaRegistro)
                        .Select(img => img.UrlImagen)
                        .FirstOrDefault(),
                    CantidadPujas = _db.Puja.Count(p => p.SubastaId == s.SubastaId) // ✅ LINQ
                })
                .ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> Finalizadas()
        {
            var data = await _db.Subasta
                .AsNoTracking()
                .Include(s => s.Joya)
                .Include(s => s.EstadoSubasta)
                .Where(s => s.EstadoSubastaId == 4 || s.EstadoSubastaId == 5) // FINALIZADA o CANCELADA
                .OrderByDescending(s => s.FechaCierre)
                .Select(s => new SubastaHistorialSelectVm
                {
                    SubastaId = s.SubastaId,
                    Objeto = s.Joya.Nombre,
                    Estado = s.EstadoSubasta.NombreEstado, // FINALIZADA/CANCELADA
                    FechaInicio = s.FechaInicio,
                    FechaRef = s.FechaCierre,
                    ImagenUrl = _db.JoyaImagen
                        .Where(img => img.JoyaId == s.JoyaId)
                        .OrderByDescending(img => img.FechaRegistro)
                        .Select(img => img.UrlImagen)
                        .FirstOrDefault(),
                    CantidadPujas = _db.Puja.Count(p => p.SubastaId == s.SubastaId) // ✅ LINQ
                })
                .ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> DetalleVisual(int id, string? origen)
        {
            var s = await _db.Subasta
                .AsNoTracking()
                .Include(x => x.Joya)
                .ThenInclude(j => j.CondicionObjeto)
                .Include(x => x.Joya)
                .ThenInclude(j => j.CategoriaJoya)
                .Include(x => x.Vendedor)
                .Include(x => x.EstadoSubasta)
                .FirstOrDefaultAsync(x => x.SubastaId == id);

            if (s == null) return NotFound();

            // Cantidad total pujas (LINQ)
            ViewBag.CantidadPujas = await _db.Puja.CountAsync(p => p.SubastaId == id);

            // Imagen principal
            ViewBag.ImagenUrl = await _db.JoyaImagen
                .AsNoTracking()
                .Where(i => i.JoyaId == s.JoyaId)
                .OrderByDescending(i => i.FechaRegistro)
                .Select(i => i.UrlImagen)
                .FirstOrDefaultAsync();

            // Condición del objeto (tabla CondicionObjeto)
            ViewBag.Condicion = s.Joya.CondicionObjeto?.NombreCondicion;

            // Categorías (JoyaCategoria -> CategoriaJoya)
            ViewBag.Categorias = s.Joya.CategoriaJoya
                 .Select(c => c.Nombre)
                 .OrderBy(n => n)
                 .ToList();

            ViewBag.Origen = origen;
            return View(s);
        }

        public async Task<IActionResult> HistorialPujas(int id)
        {
            // Validación: la subasta debe existir
            var existe = await _db.Subasta.AsNoTracking().AnyAsync(s => s.SubastaId == id);
            if (!existe) return NotFound();

            var pujas = await _db.Puja
                .AsNoTracking()
                .Include(p => p.Comprador)
                .Where(p => p.SubastaId == id)                 // ✅ asociadas al ID
                .OrderBy(p => p.FechaHora)                     // ✅ orden cronológico consistente (vieja → nueva)
                .Select(p => new PujaHistorialVm
                {
                    PujaId = p.PujaId,
                    SubastaId = p.SubastaId,
                    Usuario = p.Comprador.NombreCompleto,
                    MontoOfertado = p.MontoOfertado,
                    FechaHora = p.FechaHora
                })
                .ToListAsync();

            ViewBag.SubastaId = id;
            return View(pujas);
        }
    }
}