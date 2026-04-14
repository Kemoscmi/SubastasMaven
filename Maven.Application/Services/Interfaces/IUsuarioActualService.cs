using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.Services.Interfaces
{
    public interface IUsuarioActualService
    {
        int GetUsuarioId();
        string? GetNombreUsuario();
        string? GetCorreo();
        bool IsAuthenticated();



    }
}
