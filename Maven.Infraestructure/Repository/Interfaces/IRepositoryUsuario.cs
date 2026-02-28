using Maven.Infraestructure.MavenModels;

namespace Maven.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryUsuario
    {
        Task<int> AddAsync(Usuario entity);
        Task UpdateAsync(Usuario entity);
        Task DeleteAsync(int id);

        Task<Usuario?> FindByIdAsync(int id);
        Task<ICollection<Usuario>> ListAsync();

        //  métodos para campos calculados (LINQ)
        Task<int> CountSubastasCreadasAsync(int usuarioId);
        Task<int> CountPujasRealizadasAsync(int usuarioId);
    }
}
