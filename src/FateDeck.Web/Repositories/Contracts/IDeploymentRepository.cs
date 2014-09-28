using FateDeck.Web.Models;

namespace FateDeck.Web.Repositories.Contracts
{
    public interface IDeploymentRepository:IRepositoryBase<Deployment>
    {
        Deployment GetDeployment(FateCard fateCard);
    }
}