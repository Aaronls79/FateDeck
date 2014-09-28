using FateDeck.Web.Repositories.Contracts;
using Ninject;

namespace FateDeck.Web.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        [Inject]
        public IDeploymentRepository DeploymentRepository { get; set; }
        [Inject]
        public ISchemesRepository SchemesRepository { get; set; }
        [Inject]
        public IStrategyRepository StrategyRepository { get; set; }
    }
}