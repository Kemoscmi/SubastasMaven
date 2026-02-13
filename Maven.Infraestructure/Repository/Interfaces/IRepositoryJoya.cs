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
        Task<List<Joya>> GetAllAsync();
        Task<Joya?> GetByIdAsync(int id);
    }
}
