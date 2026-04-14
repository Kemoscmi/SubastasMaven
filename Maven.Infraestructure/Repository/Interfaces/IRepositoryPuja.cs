using Maven.Infraestructure.MavenModels;
using Microsoft.EntityFrameworkCore.Storage;

namespace Maven.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryPuja
    {
        IQueryable<Puja> Query();

        Task AddAsync(Puja entity);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
