using Maven.Infraestructure.MavenData;
using Maven.Infraestructure.MavenModels;
using Maven.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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
    }
}
