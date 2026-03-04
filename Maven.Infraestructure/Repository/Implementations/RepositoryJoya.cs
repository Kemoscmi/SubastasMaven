using Maven.Infraestructure.MavenData;
using Maven.Infraestructure.MavenModels;
using Maven.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Infraestructure.Repository.Implementations
{
    public class RepositoryJoya : IRepositoryJoya
    {
        private readonly MavenContext _db;

        public RepositoryJoya(MavenContext db)
        {
            _db = db;
        }
        public IQueryable<Joya> Query()
        {
            //   AsNoTracking para listados
            return _db.Joya.AsNoTracking();
        }

        public async Task<ICollection<Joya>> ListAsync()
        {
            return await _db.Joya
                .AsNoTracking()
                .AsSplitQuery() 
                .Include(j => j.EstadoObjeto)
                .Include(j => j.CondicionObjeto)
                .Include(j => j.JoyaImagen)
                .Include(j => j.CategoriaJoya)
                .OrderBy(j => j.JoyaId)
                .ToListAsync();
        }
        public async Task<Joya?> FindByIdAsync(int id)
        {
            return await _db.Joya
                .AsNoTracking()
                .AsSplitQuery()
                .Include(j => j.Vendedor)
                .Include(j => j.EstadoObjeto)
                .Include(j => j.CondicionObjeto)
                .Include(j => j.JoyaImagen)
                .Include(j => j.Subasta).ThenInclude(s => s.EstadoSubasta)
                .Include(j => j.CategoriaJoya)
                .FirstOrDefaultAsync(j => j.JoyaId == id);
        }

        public async Task<int> AddAsync(Joya entity)
        {
            _db.Joya.Add(entity);
            await _db.SaveChangesAsync();
            return entity.JoyaId;
        }

        public async Task UpdateAsync(Joya entity)
        {
            _db.Joya.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Joya.FindAsync(id);
            if (entity is null) return;

            _db.Joya.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }

}
