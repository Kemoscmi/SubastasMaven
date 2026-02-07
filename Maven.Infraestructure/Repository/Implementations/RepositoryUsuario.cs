using Microsoft.EntityFrameworkCore;
using Maven.Infraestructure.MavenData;
using Maven.Infraestructure.MavenModels;
using Maven.Infraestructure.Repository.Interfaces;

namespace Maven.Infraestructure.Repository.Implementations
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        private readonly MavenContext _db;

        public RepositoryUsuario(MavenContext db)
        {
            _db = db;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            // Incluimos Rol y EstadoUsuario para mostrar nombres
            return await _db.Usuario
                .AsNoTracking()
                .Include(u => u.Rol)
                .Include(u => u.EstadoUsuario)
                .OrderBy(u => u.UsuarioId)
                .ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _db.Usuario
                .AsNoTracking()
                .Include(u => u.Rol)
                .Include(u => u.EstadoUsuario)
                .FirstOrDefaultAsync(u => u.UsuarioId == id);
        }
    }
}
