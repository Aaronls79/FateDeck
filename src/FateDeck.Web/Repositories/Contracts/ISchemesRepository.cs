using FateDeck.Web.Models;

namespace FateDeck.Web.Repositories.Contracts
{
    public interface ISchemesRepository : IRepositoryBase<Scheme>
    {
        Scheme[] GetSchemes(params FateCard[] fateCards);
    }
}