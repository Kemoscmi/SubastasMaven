using Maven.Infraestructure.MavenModels;

namespace Maven.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryPuja
    {
        IQueryable<Puja> Query();
    }
}
