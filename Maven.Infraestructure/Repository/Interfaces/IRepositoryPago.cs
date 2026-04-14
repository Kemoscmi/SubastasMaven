using Maven.Infraestructure.MavenModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryPago
    {
        Task AddAsync(Pago entity);
        Task<Pago?> FindBySubastaIdAsync(int subastaId);
        Task UpdateAsync(Pago entity);
    }
}
