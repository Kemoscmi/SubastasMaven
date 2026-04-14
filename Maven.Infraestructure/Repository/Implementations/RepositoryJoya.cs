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
      .Where(j => j.EstadoObjetoId == 1) 
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
            var actual = await _db.Joya.FirstOrDefaultAsync(j => j.JoyaId == entity.JoyaId);

            if (actual == null)
                throw new Exception("No se encontró la joya.");

            actual.Nombre = entity.Nombre;
            actual.Descripcion = entity.Descripcion;
            actual.CondicionObjetoId = entity.CondicionObjetoId;
            actual.EstadoObjetoId = entity.EstadoObjetoId;
            actual.VendedorId = entity.VendedorId;
            actual.FechaRegistro = entity.FechaRegistro;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Joya
                .Include(j => j.Subasta)
                .FirstOrDefaultAsync(j => j.JoyaId == id);

            if (entity is null)
                return;

            
            if (entity.Subasta != null && entity.Subasta.Any())
                throw new Exception("No se puede eliminar la joya porque ya ha sido subastada.");

           
            entity.EstadoObjetoId = 2;

            _db.Joya.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task AddCategoriasAsync(int joyaId, List<int> categoriaIds)
        {
            var joya = await _db.Joya
                .Include(j => j.CategoriaJoya)
                .FirstOrDefaultAsync(j => j.JoyaId == joyaId);

            if (joya == null)
                throw new Exception("No se encontró la joya.");

            var categorias = await _db.CategoriaJoya
                .Where(c => categoriaIds.Contains(c.CategoriaJoyaId))
                .ToListAsync();

            foreach (var categoria in categorias)
            {
                if (!joya.CategoriaJoya.Any(c => c.CategoriaJoyaId == categoria.CategoriaJoyaId))
                {
                    joya.CategoriaJoya.Add(categoria);
                }
            }

            await _db.SaveChangesAsync();
        }

        public async Task AddImagenesAsync(int joyaId, List<string> rutasImagenes)
        {
            if (rutasImagenes == null || rutasImagenes.Count == 0)
                return;

            var imagenes = rutasImagenes
                .Select(ruta => new JoyaImagen
                {
                    JoyaId = joyaId,
                    UrlImagen = ruta
                })
                .ToList();

            _db.JoyaImagen.AddRange(imagenes);
            await _db.SaveChangesAsync();
        }
        public async Task ReplaceCategoriasAsync(int joyaId, List<int> categoriaIds)
        {
            var joya = await _db.Joya
                .Include(j => j.CategoriaJoya)
                .FirstOrDefaultAsync(j => j.JoyaId == joyaId);

            if (joya == null)
                throw new Exception("No se encontró la joya.");

            joya.CategoriaJoya.Clear();

            if (categoriaIds != null && categoriaIds.Count > 0)
            {
                var categorias = await _db.CategoriaJoya
                    .Where(c => categoriaIds.Contains(c.CategoriaJoyaId))
                    .ToListAsync();

                foreach (var categoria in categorias)
                {
                    joya.CategoriaJoya.Add(categoria);
                }
            }

            await _db.SaveChangesAsync();
        }
        public async Task DeleteImagenAsync(int joyaImagenId)
        {
            var imagen = await _db.JoyaImagen
                .FirstOrDefaultAsync(i => i.JoyaImagenId == joyaImagenId);

            if (imagen == null)
                return;

            _db.JoyaImagen.Remove(imagen);
            await _db.SaveChangesAsync();
        }


        public async Task<ICollection<Joya>> ListInactivosAsync()
        {
            return await _db.Joya
                .Where(j => j.EstadoObjetoId == 2) 
                .AsNoTracking()
                .Include(j => j.EstadoObjeto)
                .Include(j => j.CondicionObjeto)
                .Include(j => j.JoyaImagen)
                .Include(j => j.CategoriaJoya)
                .OrderBy(j => j.JoyaId)
                .ToListAsync();
        }

        public async Task ToggleEstadoAsync(int id)
        {
            var joya = await _db.Joya.FirstOrDefaultAsync(j => j.JoyaId == id);

            if (joya == null)
                throw new Exception("No se encontró la joya.");

            joya.EstadoObjetoId = joya.EstadoObjetoId == 1 ? 2 : 1;

            await _db.SaveChangesAsync();
        }
    }

}
