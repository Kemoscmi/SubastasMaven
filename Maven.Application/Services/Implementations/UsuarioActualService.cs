using Maven.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace Maven.Application.Services.Implementations
{
    public class UsuarioActualService : IUsuarioActualService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioActualService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUsuarioId()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
                throw new UnauthorizedAccessException("No hay un usuario autenticado.");

            var claim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claim))
                throw new UnauthorizedAccessException("No se encontró el identificador del usuario autenticado.");

            if (!int.TryParse(claim, out int usuarioId))
                throw new UnauthorizedAccessException("El identificador del usuario autenticado no es válido.");

            return usuarioId;
        }

        public string? GetNombreUsuario()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
        }

        public string? GetCorreo()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }
    }
}