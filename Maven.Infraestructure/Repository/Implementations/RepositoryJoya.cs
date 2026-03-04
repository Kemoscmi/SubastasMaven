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
                .Include(j => j.Vendedor)
                .Include(j => j.EstadoObjeto)
                .Include(j => j.CondicionObjeto)
                .Include(j => j.JoyaImagen)
                .Include(j => j.Subasta)
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

        public async Task<ICollection<Joya>> ListCatalogoAsync()
        {
            return await _db.Joya
                .AsNoTracking()
                .OrderBy(j => j.JoyaId)
                .Select(j => new Joya
                {
                    JoyaId = j.JoyaId,
                    Nombre = j.Nombre,

                    // Solo lo necesario para mostrar Condición/Estado
                    EstadoObjeto = j.EstadoObjeto,
                    CondicionObjeto = j.CondicionObjeto,

                    //  Solo 1 imagen 
                    JoyaImagen = j.JoyaImagen
                        .OrderBy(i => i.JoyaImagenId)
                        .Take(1)
                        .ToList(),

                    // Categorías 
                    CategoriaJoya = j.CategoriaJoya.ToList()
                })
                .ToListAsync();
        }
    }

}
