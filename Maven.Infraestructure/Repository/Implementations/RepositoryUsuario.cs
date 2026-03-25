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

        public async Task<Usuario?> FindByIdAsync(int id)
        {
            return await _db.Usuario
                .AsNoTracking()
                .Include(u => u.Rol)
                .Include(u => u.EstadoUsuario)
                .FirstOrDefaultAsync(u => u.UsuarioId == id);
        }

        public async Task<ICollection<Usuario>> ListAsync()
        {
            return await _db.Usuario
                .AsNoTracking()
                .Include(u => u.Rol)
                .Include(u => u.EstadoUsuario)
                .OrderBy(u => u.UsuarioId)
                .ToListAsync();
        }

  

        public async Task<int> AddAsync(Usuario entity)
        {
            _db.Usuario.Add(entity);
            await _db.SaveChangesAsync();
            return entity.UsuarioId;
        }

        public async Task UpdateAsync(Usuario entity)
        {
            var actual = await _db.Usuario.FirstOrDefaultAsync(u => u.UsuarioId == entity.UsuarioId);

            if (actual == null)
                throw new Exception("No se encontró el usuario.");

            actual.NombreCompleto = entity.NombreCompleto;
            actual.Correo = entity.Correo;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Usuario.FindAsync(id);
            if (entity is null) return;

            _db.Usuario.Remove(entity);
            await _db.SaveChangesAsync();
        }
        //  cantidad de subastas creadas por este usuario (como vendedor)
        public async Task<int> CountSubastasCreadasAsync(int usuarioId)
        {
            return await _db.Subasta
                .CountAsync(s => s.VendedorId == usuarioId);
        }

        //  cantidad de pujas realizadas por este usuario (como comprador)
        public async Task<int> CountPujasRealizadasAsync(int usuarioId)
        {
            return await _db.Puja
                .CountAsync(p => p.CompradorId == usuarioId);
        }


        public async Task<Usuario?> FindByEmailAsync(string email)
        {
            var correo = email.Trim().ToLower();

            return await _db.Usuario
                .Include(u => u.Rol)
                .Include(u => u.EstadoUsuario)
                .FirstOrDefaultAsync(u => u.Correo.ToLower() == correo);
        }

        public async Task CambiarEstadoAsync(int usuarioId, int estadoUsuarioId)
        {
            var actual = await _db.Usuario.FirstOrDefaultAsync(u => u.UsuarioId == usuarioId);

            if (actual == null)
                throw new Exception("No se encontró el usuario.");

            actual.EstadoUsuarioId = estadoUsuarioId;

            await _db.SaveChangesAsync();
        }
    }
}