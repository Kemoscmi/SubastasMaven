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
    }
}
