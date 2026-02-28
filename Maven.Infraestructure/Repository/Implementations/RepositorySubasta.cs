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

        public async Task<ICollection<Subasta>> ListAsync()
        {
            return await _db.Subasta
                .AsNoTracking()
                .Include(s => s.Joya)
                .Include(s => s.Vendedor)
                .Include(s => s.EstadoSubasta)
                .OrderByDescending(s => s.FechaCreacion)
                .ToListAsync();
        }

        public async Task<Subasta?> FindByIdAsync(int id)
        {
            return await _db.Subasta
                .AsNoTracking()
                .Include(s => s.Joya)
                .Include(s => s.Vendedor)
                .Include(s => s.EstadoSubasta)
                .Include(s => s.Puja)
                .Include(s => s.SubastaResultado)
                .FirstOrDefaultAsync(s => s.SubastaId == id);
        }

        public async Task<int> AddAsync(Subasta entity)
        {
            _db.Subasta.Add(entity);
            await _db.SaveChangesAsync();
            return entity.SubastaId;
        }

        public async Task UpdateAsync(Subasta entity)
        {
            _db.Subasta.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Subasta.FindAsync(id);
            if (entity is null) return;

            _db.Subasta.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
