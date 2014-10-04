using FateDeck.Web.Models;
using FateDeck.Web.Repositories;
using FateDeck.Web.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FateDeck.Tests
{
    [TestClass]
    public class DeploymentRepositoryTest : TestBase
    {
        private readonly DeploymentRepository _repository = new DeploymentRepository();

        [TestMethod]
        public void GetDeployment_GivenFateCard_ReturnsDeployment()
        {
            var deck = new FateCardDeck();
            var deployment = _repository.GetDeployment(deck.AvailableCards[1]);
            deployment.IsNotNullOrEmpty();
            deployment.Name.ShouldEqual("Standard Deployment");
        }
    }
}