using Maven.Infraestructure.MavenData;
using Maven.Infraestructure.MavenModels;
using Maven.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Maven.Infraestructure.Repository.Implementations
{
    public class RepositorySubasta : IRepositorySubasta
    {
        private readonly MavenContext _db;

        public RepositorySubasta(MavenContext db)
        {
            _db = db;
        }

        public IQueryable<Subasta> Query()
        {
            return _db.Subasta.AsNoTracking();
        }
    }
}
