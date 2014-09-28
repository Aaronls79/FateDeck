namespace FateDeck.Web.Repositories.Contracts
{
    public interface IRepositoryFactory
    {
        IDeploymentRepository DeploymentRepository { get; }
        ISchemesRepository SchemesRepository { get; }
        IStrategyRepository StrategyRepository { get; }
    }
}