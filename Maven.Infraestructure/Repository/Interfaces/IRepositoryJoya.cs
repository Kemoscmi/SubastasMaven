using Maven.Infraestructure.MavenModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryJoya
    {
        Task<int> AddAsync(Joya entity);
        Task UpdateAsync(Joya entity);
        Task DeleteAsync(int id);

        Task<Joya?> FindByIdAsync(int id);
        Task<ICollection<Joya>> ListAsync();

    
        IQueryable<Joya> Query();

        Task AddCategoriasAsync(int joyaId, List<int> categoriaIds);
        Task AddImagenesAsync(int joyaId, List<string> rutasImagenes);

        Task ReplaceCategoriasAsync(int joyaId, List<int> categoriaIds);

        Task DeleteImagenAsync(int joyaImagenId);

        Task<ICollection<Joya>> ListInactivosAsync();

        Task ToggleEstadoAsync(int id);
    }
}
