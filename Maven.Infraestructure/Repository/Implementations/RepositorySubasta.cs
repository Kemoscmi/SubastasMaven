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
                .Include(s => s.Puja)
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

        public async Task<ICollection<Joya>> GetJoyasAsync()
        {
            return await _db.Joya
                .AsNoTracking()
                .Where(j => j.EstadoObjetoId == 1) 
                .OrderBy(j => j.Nombre)
                .ToListAsync();
        }

        public async Task<ICollection<Usuario>> GetVendedoresAsync()
        {
            return await _db.Usuario
                .AsNoTracking()
                .Where(u => u.RolId == 3)
                .OrderBy(u => u.NombreCompleto)
                .ToListAsync();
        }

        public async Task<ICollection<EstadoSubasta>> GetEstadosSubastaAsync()
        {
            return await _db.EstadoSubasta
                .AsNoTracking()
                .OrderBy(e => e.NombreEstado)
                .ToListAsync();
        }

        public async Task<ICollection<Subasta>> GetActivasAsync()
        {
            return await _db.Subasta
                .AsNoTracking()
                .Include(s => s.Joya)
                .Include(s => s.EstadoSubasta)
                .Include(s => s.Puja)
                .Include(s => s.Joya)
                    .ThenInclude(j => j.JoyaImagen)
                .Where(s => s.EstadoSubastaId == 3)
                .OrderBy(s => s.FechaCierre)
                .ToListAsync();
        }

        public async Task<ICollection<Subasta>> GetFinalizadasAsync()
        {
            return await _db.Subasta
                .AsNoTracking()
                .Include(s => s.Joya)
                .Include(s => s.EstadoSubasta)
                .Include(s => s.Puja)
                .Include(s => s.Joya)
                    .ThenInclude(j => j.JoyaImagen)
                .Where(s => s.EstadoSubastaId == 4 || s.EstadoSubastaId == 5)
                .OrderByDescending(s => s.FechaCierre)
                .ToListAsync();
        }

        public async Task<Subasta?> GetDetalleVisualByIdAsync(int id)
        {
            return await _db.Subasta
                .AsNoTracking()
                .Include(s => s.Joya)
                    .ThenInclude(j => j.CondicionObjeto)
                .Include(s => s.Joya)
                   .Include(s => s.Joya)
                      .ThenInclude(j => j.CategoriaJoya)
                .Include(s => s.Joya)
                    .ThenInclude(j => j.JoyaImagen)
                .Include(s => s.Vendedor)
                .Include(s => s.EstadoSubasta)
                .Include(s => s.Puja)
                     .ThenInclude(p => p.Comprador)
                .FirstOrDefaultAsync(s => s.SubastaId == id);
        }

        public async Task<ICollection<Puja>> GetPujasBySubastaIdAsync(int subastaId)
        {
            return await _db.Puja
                .AsNoTracking()
                .Include(p => p.Comprador)
                .Where(p => p.SubastaId == subastaId)
                .OrderBy(p => p.FechaHora)
                .ToListAsync();
        }

        public async Task<ICollection<Subasta>> GetBorradoresByVendedorAsync(int vendedorId)
        {
            return await _db.Subasta
                .AsNoTracking()
                .Include(s => s.Joya)
                    .ThenInclude(j => j.JoyaImagen)
                .Include(s => s.EstadoSubasta)
                .Include(s => s.Puja)
                .Where(s => s.VendedorId == vendedorId && (s.EstadoSubastaId == 1 || s.EstadoSubastaId == 2))
                .OrderByDescending(s => s.FechaCreacion)
                .ToListAsync();
        }

        public async Task<ICollection<Subasta>> GetPublicadasParaActivarAsync()
        {
            return await _db.Subasta
                .Include(s => s.EstadoSubasta)
                .Where(s => s.EstadoSubastaId == 2 && s.FechaInicio <= DateTime.Now)
                .ToListAsync();
        }

        public async Task<ICollection<Subasta>> GetActivasParaCerrarAsync()
        {
            return await _db.Subasta
                .Include(s => s.Puja)
                    .ThenInclude(p => p.Comprador)
                .Include(s => s.SubastaResultado)
                .Include(s => s.EstadoSubasta)
                .Where(s => s.EstadoSubastaId == 3 && s.FechaCierre <= DateTime.Now)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<ICollection<Subasta>> ListVisiblesAsync()
        {
            return await _db.Subasta
                .AsNoTracking()
                .Include(s => s.Joya)
                .Include(s => s.Vendedor)
                .Include(s => s.EstadoSubasta)
                .Include(s => s.Puja)
                .Where(s => s.EstadoSubastaId != 1) // ocultar BORRADOR
                .OrderByDescending(s => s.FechaCreacion)
                .ToListAsync();
        }
    }
}