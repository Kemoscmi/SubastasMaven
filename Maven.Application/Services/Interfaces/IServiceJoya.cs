using Maven.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Application.Services.Interfaces
{
    public interface IServiceJoya
    {
        Task<int> AddAsync(JoyaDTO dto);
        Task UpdateAsync(int id, JoyaDTO dto);
        Task DeleteAsync(int id);

        Task<List<JoyaDTO>> ListAsync();
        Task<JoyaDTO> FindByIdAsync(int id);

        Task AddCategoriasAsync(int joyaId, List<int> categoriaIds);
        Task AddImagenesAsync(int joyaId, List<string> rutasImagenes);
        Task ReplaceCategoriasAsync(int joyaId, List<int> categoriaIds);
        Task DeleteImagenAsync(int joyaImagenId);

        Task<List<JoyaDTO>> ListInactivosAsync();

        Task ToggleEstadoAsync(int id);
    }
}
