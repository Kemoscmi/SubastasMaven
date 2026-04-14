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
    public class RepositoryPago : IRepositoryPago
    {
        private readonly MavenContext _db;

        public RepositoryPago(MavenContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Pago entity)
        {
            await _db.Pago.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Pago?> FindBySubastaIdAsync(int subastaId)
        {
            return await _db.Pago
                .Include(p => p.EstadoPago)
                .FirstOrDefaultAsync(p => p.SubastaId == subastaId);
        }

        public async Task UpdateAsync(Pago entity)
        {
            _db.Pago.Update(entity);
            await _db.SaveChangesAsync();
        }
    
}
}
