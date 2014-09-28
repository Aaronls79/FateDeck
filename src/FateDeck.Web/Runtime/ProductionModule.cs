using FateDeck.Web.Repositories;
using FateDeck.Web.Repositories.Contracts;
using Ninject.Modules;

namespace FateDeck.Web.Runtime
{
    public class ProductionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDeploymentRepository>().To<DeploymentRepository>();
            Bind<ISchemesRepository>().To<SchemesRepository>();
            Bind<IStrategyRepository>().To<StrategyRepository>();
            Bind<IRepositoryFactory>().To<RepositoryFactory>();
        }
    }
}