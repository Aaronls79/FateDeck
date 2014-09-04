using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FateDeck.Tests
{
    [TestClass]
    public class FatDeckTest
    {
        private Web.Models.FateDeck _fateDeck = new Web.Models.FateDeck();

        [TestMethod]
        public void Shuffle_WhenCalled_ShufflesAllCardsIntoYourDeck()
        {
            _fateDeck.Shuffle();
            Assert.AreEqual(54,_fateDeck.FateDeckStack.Count);
        }
    }
}
