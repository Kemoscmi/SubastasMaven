using Maven.Infraestructure.MavenData;
using Maven.Infraestructure.MavenModels;
using Maven.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Infraestructure.Repository.Implementations
{
    public class RepositorySubastaResultado : IRepositorySubastaResultado
    {
        private readonly MavenContext _db;

        public RepositorySubastaResultado(MavenContext db)
        {
            _db = db;
        }

        public async Task AddAsync(SubastaResultado entity)
        {
            await _db.SubastaResultado.AddAsync(entity);
            await _db.SaveChangesAsync();
        }
    }
}
