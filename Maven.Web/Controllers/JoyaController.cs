using Maven.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Maven.Web.Controllers
{
    public class JoyaController : Controller
    {
        private readonly IServiceJoya _service;
        public JoyaController(IServiceJoya service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return NotFound();
            return View(data);
        }
    }
}
