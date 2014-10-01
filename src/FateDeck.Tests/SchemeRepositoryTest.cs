using System.Linq;
using FateDeck.Web.Models;
using FateDeck.Web.Repositories;
using FateDeck.Web.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FateDeck.Tests
{
    [TestClass]
    public class SchemeRepositoryTest : TestBase
    {
        private readonly SchemesRepository _repository = new SchemesRepository();


        [TestMethod]
        public void GetScheme_GivenFateCard_ReturnsScheme()
        {
            var deck = new FateCardDeck();
            deck.Shuffle();
            var scheme = _repository.GetSchemes(deck.AvailableCards[0], deck.AvailableCards[1]);
            scheme.IsNotNullOrEmpty();
            var lineInTheSand = (
                from s in scheme
                where s.Name == "A Line in the Sand"
                select s).FirstOrDefault();
            lineInTheSand.IsNotNullOrEmpty();
            var assassinate = (
                from s in scheme
                where s.Name == "Assassinate"
                select s).FirstOrDefault();
            assassinate.IsNotNullOrEmpty();
            var cursedObject = (
                from s in scheme
                where s.Name == "Cursed Object"
                select s).FirstOrDefault();
            cursedObject.IsNotNullOrEmpty();
            var outflank = (
                from s in scheme
                where s.Name == "Outflank"
                select s).FirstOrDefault();
            outflank.IsNotNullOrEmpty();
            var distract = (
                from s in scheme
                where s.Name == "Distract"
                select s).FirstOrDefault();
            distract.IsNotNullOrEmpty();

        }
    }
}