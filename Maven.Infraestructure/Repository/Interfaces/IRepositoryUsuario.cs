using Maven.Infraestructure.MavenModels;

namespace Maven.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryUsuario
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
    }
}
