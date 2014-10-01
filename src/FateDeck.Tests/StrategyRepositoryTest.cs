using FateDeck.Web.Models;
using FateDeck.Web.Repositories;
using FateDeck.Web.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FateDeck.Tests
{
    [TestClass]
    public class StrategyRepositoryTest : TestBase
    {
        private readonly StrategyRepository _repository = new StrategyRepository();

        [TestMethod]
        public void GetStrategy_GivenFateCard_ReturnsStrategy()
        {
            var deck = new FateCardDeck();
            deck.Shuffle();
            var strategy = _repository.GetStandardStrategy(deck.Flip());
            strategy.IsNotNullOrEmpty();
        }
    }
}