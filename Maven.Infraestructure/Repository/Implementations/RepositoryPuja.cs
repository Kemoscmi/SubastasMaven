using Maven.Infraestructure.MavenData;
using Maven.Infraestructure.MavenModels;
using Maven.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Maven.Infraestructure.Repository.Implementations
{
    public class RepositoryPuja : IRepositoryPuja
    {
        private readonly MavenContext _db;

        public RepositoryPuja(MavenContext db)
        {
            _db = db;
        }

        public IQueryable<Puja> Query()
        {
            return _db.Puja.AsNoTracking();
        }

        public async Task AddAsync(Puja entity)
        {
            await _db.Puja.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _db.Database.BeginTransactionAsync();
        }
    }
}
