using Maven.Infraestructure.MavenModels;

namespace Maven.Infraestructure.Repository.Interfaces
{
    public interface IRepositorySubasta
    {
        IQueryable<Subasta> Query();
    }
}
