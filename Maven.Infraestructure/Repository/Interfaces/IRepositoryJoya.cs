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

        //  Query base para poder proyectar
        IQueryable<Joya> Query();


    }
}
