using Maven.Application.DTOs;
using Maven.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;

namespace Maven.Web.Controllers
{
    [Authorize]
    public class JoyaController : Controller
    {
        private readonly IServiceJoya _service;
        private readonly IServiceUsuario _serviceUsuario;

        public JoyaController(IServiceJoya service, IServiceUsuario serviceUsuario)
        {
            _service = service;
            _serviceUsuario = serviceUsuario;
        }

        private async Task LoadCombosAsync()
        {
            ViewData["EstadoObjetoId"] = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "ACTIVO" },
                new SelectListItem { Value = "2", Text = "INACTIVO" },
                new SelectListItem { Value = "3", Text = "EN_SUBASTA" },
                new SelectListItem { Value = "4", Text = "VENDIDO" }
            };

            ViewData["CondicionObjetoId"] = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "NUEVO" },
                new SelectListItem { Value = "2", Text = "USADO" }
            };

      
            ViewData["CategoriaJoyaId"] = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Anillos" },
                new SelectListItem { Value = "2", Text = "Collares" },
                new SelectListItem { Value = "3", Text = "Pulseras" },
                new SelectListItem { Value = "4", Text = "Aretes" },
                new SelectListItem { Value = "5", Text = "Relojes" }
            };
        }

        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out int usuarioId))
            {
                return Unauthorized();
            }

            var data = await _service.GetByVendedorAsync(usuarioId);
            return View(data.ToList());
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var data = await _service.FindByIdAsync(id);
            return View(data);
        }

        // GET: Joya/Create
        public async Task<IActionResult> Create()
        {
            await LoadCombosAsync();
            ViewBag.VendedorNombre = User.Identity?.Name ?? "Usuario autenticado";
            ViewBag.CategoriasSeleccionadas = new List<int>();
            return View();
        }

        // POST: Joya/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JoyaDTO dto, List<int> categoriaIds, List<IFormFile> imageFiles)
        {
            // Remover validaciones que no corresponden al formulario de creación
            var keysToRemove = ModelState.Keys
                .Where(k =>
                    k.StartsWith("Vendedor") ||
                    k.StartsWith("EstadoObjeto") ||
                    k.StartsWith("CondicionObjeto") ||
                    k.StartsWith("JoyaImagen") ||
                    k.StartsWith("Subasta") ||
                    k.StartsWith("CategoriaJoya") ||
                    k.StartsWith("CategoriasTexto") ||
                    k.StartsWith("FechaRegistro"))
                .ToList();

            foreach (var key in keysToRemove)
            {
                ModelState.Remove(key);
            }

            // Tomar vendedor desde el usuario autenticado
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out int vendedorId))
            {
                ModelState.AddModelError("", "No se pudo identificar el usuario autenticado.");
                await LoadCombosAsync();
                ViewBag.VendedorNombre = User.Identity?.Name ?? "Usuario autenticado";
                ViewBag.CategoriasSeleccionadas = categoriaIds ?? new List<int>();
                return View(dto);
            }

            dto.VendedorId = vendedorId;

            // Estado inicial automático
            dto.EstadoObjetoId = 1;

            // Validación de categorías
            if (categoriaIds == null || categoriaIds.Count == 0)
            {
                ModelState.AddModelError("categoriaIds", "Debe seleccionar al menos una categoría.");
            }

            // Validación de imágenes
            if (imageFiles == null || imageFiles.Count == 0)
            {
                ModelState.AddModelError("imageFiles", "Debe seleccionar al menos una imagen.");
            }

            var rutasImagenes = new List<string>();

            if (imageFiles != null && imageFiles.Count > 0)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "joyas");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                foreach (var imageFile in imageFiles)
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine(folderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        rutasImagenes.Add("/images/joyas/" + fileName);
                    }
                }
            }

            if (rutasImagenes.Count == 0)
            {
                ModelState.AddModelError("imageFiles", "Debe seleccionar al menos una imagen válida.");
            }
            else
            {
                // La primera imagen será la principal
                dto.ImagenPrincipal = rutasImagenes.First();
            }

            if (!ModelState.IsValid)
            {
                await LoadCombosAsync();
                ViewBag.VendedorNombre = User.Identity?.Name ?? "Usuario autenticado";
                ViewBag.CategoriasSeleccionadas = categoriaIds ?? new List<int>();
                return View(dto);
            }

            // Guardar joya
            var joyaId = await _service.AddAsync(dto);

       
            await _service.AddCategoriasAsync(joyaId, categoriaIds);
            await _service.AddImagenesAsync(joyaId, rutasImagenes);

            return RedirectToAction(nameof(Index));
         

           
        }

        // GET: Joya/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.FindByIdAsync(id);

            if (data == null)
                return NotFound();

            // No permitir editar si tiene subasta activa
            if (data.Subasta != null && data.Subasta.Any(s =>
                s.EstadoSubasta != null &&
                !string.IsNullOrWhiteSpace(s.EstadoSubasta.NombreEstado) &&
                s.EstadoSubasta.NombreEstado.Trim().ToUpper() == "ACTIVA"))
            {
                TempData["MensajeError"] = "No se puede editar la joya porque se encuentra en una subasta activa.";
                return RedirectToAction(nameof(Detalle), new { id });
            }

            await LoadCombosAsync();

            // Vendedor
            ViewBag.VendedorNombre = data.Vendedor?.NombreCompleto ?? User.Identity?.Name ?? "Usuario autenticado";

            // Estado actual
            ViewBag.EstadoObjetoNombre = data.EstadoObjeto?.NombreEstado ?? "SIN ESTADO";

            // Categorías seleccionadas
            ViewBag.CategoriasSeleccionadas = data.CategoriaJoya != null
                ? data.CategoriaJoya.Select(c => c.CategoriaJoyaId).ToList()
                : new List<int>();

            return View(data);
        }

        // POST: Joya/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JoyaDTO dto, List<int> categoriaIds, List<IFormFile> imageFiles, List<int> imagenesEliminarIds)
        {
            // Remover validaciones que no corresponden al formulario Edit
            var keysToRemove = ModelState.Keys
                .Where(k =>
                    k.StartsWith("Vendedor") ||
                    k.StartsWith("EstadoObjeto") ||
                    k.StartsWith("CondicionObjeto") ||
                    k.StartsWith("JoyaImagen") ||
                    k.StartsWith("Subasta") ||
                    k.StartsWith("CategoriaJoya") ||
                    k.StartsWith("CategoriasTexto") ||
                    k.StartsWith("FechaRegistro"))
                .ToList();

            foreach (var key in keysToRemove)
            {
                ModelState.Remove(key);
            }

            var actual = await _service.FindByIdAsync(id);

            if (actual == null)
            {
                return NotFound();
            }

   
            // no  permitir editar si la joya está en una subasta activa
            if (actual.Subasta != null && actual.Subasta.Any(s =>
                s.EstadoSubasta != null &&
                s.EstadoSubasta.NombreEstado != null &&
                s.EstadoSubasta.NombreEstado.Trim().ToUpper() == "ACTIVA"))
            {
                TempData["MensajeError"] = "No se puede editar la joya porque se encuentra en una subasta activa.";
                return RedirectToAction(nameof(Detalle), new { id });
            }

           
            dto.JoyaId = id;
            dto.VendedorId = actual.VendedorId;
            dto.EstadoObjetoId = actual.EstadoObjetoId;
            dto.FechaRegistro = actual.FechaRegistro;

            if (categoriaIds == null || categoriaIds.Count == 0)
            {
                ModelState.AddModelError("categoriaIds", "Debe seleccionar al menos una categoría.");
            }

            var rutasImagenesNuevas = new List<string>();

            if (imageFiles != null && imageFiles.Count > 0)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "joyas");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                foreach (var imageFile in imageFiles)
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine(folderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        rutasImagenesNuevas.Add("/images/joyas/" + fileName);
                    }
                }
            }

            // Si subieron imágenes nuevas, la primera pasa a ser la principal
            // Si no subieron, se conserva la actual
            dto.ImagenPrincipal = rutasImagenesNuevas.Any()
                ? rutasImagenesNuevas.First()
                : actual.ImagenPrincipal;

            if (!ModelState.IsValid)
            {
                await LoadCombosAsync();
                ViewBag.VendedorNombre = actual.Vendedor?.NombreCompleto ?? User.Identity?.Name ?? "Usuario autenticado";
                ViewBag.EstadoObjetoNombre = actual.EstadoObjeto?.NombreEstado ?? "ACTIVO";
                ViewBag.CategoriasSeleccionadas = categoriaIds ?? new List<int>();

                // Mantener imágenes actuales 
                dto.JoyaImagen = actual.JoyaImagen;
                dto.FechaRegistro = actual.FechaRegistro;

                return View(dto);
            }

      
            await _service.UpdateAsync(id, dto);
            await _service.ReplaceCategoriasAsync(id, categoriaIds ?? new List<int>());

            // Reemplazar categorías seleccionadas
            // Eliminar imágenes marcadas que si se qiere eliiminar
            if (imagenesEliminarIds != null && imagenesEliminarIds.Any())
            {
                foreach (var imagenId in imagenesEliminarIds)
                {
                    await _service.DeleteImagenAsync(imagenId);
                }
            }

   
            if (rutasImagenesNuevas.Any())
            {
                await _service.AddImagenesAsync(id, rutasImagenesNuevas);
            }

            return RedirectToAction(nameof(Detalle), new { id });
        }

        // GET: Joya/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _service.FindByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        // POST: Joya/DeleteConfirmed/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["Mensaje"] = "La joya se desactivó correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = ex.Message;
                return RedirectToAction(nameof(Detalle), new { id });
            }
        }

        public async Task<IActionResult> Inactivos()
        {
            var data = await _service.ListInactivosAsync();
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleEstado(int id)
        {
            if (id <= 0)
                return BadRequest();

            await _service.ToggleEstadoAsync(id);

            return RedirectToAction(nameof(Inactivos));
        }
    }
}