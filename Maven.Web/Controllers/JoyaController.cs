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
            var data = await _service.ListAsync();
            return View(data.ToList());
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var data = await _service.FindByIdAsync(id);
            return View(data);
        }
    }
}
