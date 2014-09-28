using FateDeck.Web.Models;

namespace FateDeck.Web.Repositories.Contracts
{
    public interface IStrategyRepository : IRepositoryBase<Strategy>
    {
        Strategy GetStandardStrategy(FateCard fateCard);
    }
}