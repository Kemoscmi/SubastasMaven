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

        public async Task<List<Joya>> GetAllAsync()
        {
            return await _db.Joya
                .AsNoTracking()
                .Include(j => j.CategoriaJoya)
                .Include(j => j.EstadoObjeto)
                .Include(j => j.CondicionObjeto)
                .Include(j => j.JoyaImagen)
                .OrderBy(j => j.JoyaId)
                .ToListAsync();
        }

        public async Task<Joya?> GetByIdAsync(int id)
        {
            return await _db.Joya
                .AsNoTracking()
                .Include(j => j.Vendedor)
                .Include(j => j.CategoriaJoya)
                .Include(j => j.EstadoObjeto)
                .Include(j => j.CondicionObjeto)
                .Include(j => j.JoyaImagen)
                .Include(j => j.Subasta) //  historial subastas (LINQ)
                .FirstOrDefaultAsync(j => j.JoyaId == id);
        }
    }
}
