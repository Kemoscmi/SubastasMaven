using Microsoft.AspNetCore.Mvc;
using Maven.Application.Services.Interfaces;

namespace Maven.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IServiceUsuario _service;

        public UsuarioController(IServiceUsuario service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }
    }
}
