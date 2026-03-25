using Maven.Infraestructure.MavenModels;

namespace Maven.Infraestructure.Repository.Interfaces
{
    public interface IRepositorySubasta
    {
        Task<int> AddAsync(Subasta entity);
        Task UpdateAsync(Subasta entity);
        Task DeleteAsync(int id);

        Task<Subasta?> FindByIdAsync(int id);
        Task<ICollection<Subasta>> ListAsync();

        Task<ICollection<Joya>> GetJoyasAsync();
        Task<ICollection<Usuario>> GetVendedoresAsync();
        Task<ICollection<EstadoSubasta>> GetEstadosSubastaAsync();

        Task<ICollection<Subasta>> GetActivasAsync();
        Task<ICollection<Subasta>> GetFinalizadasAsync();
        Task<Subasta?> GetDetalleVisualByIdAsync(int id);
        Task<ICollection<Puja>> GetPujasBySubastaIdAsync(int subastaId);
        Task<ICollection<Subasta>> GetBorradoresByVendedorAsync(int vendedorId);

        Task<ICollection<Subasta>> GetPublicadasParaActivarAsync();
        Task SaveChangesAsync();

        Task<ICollection<Subasta>> ListVisiblesAsync();

    }
}