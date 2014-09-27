using System;
using FateDeck.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FateDeck.Tests
{
    [TestClass]
    public class FatDeckTest
    {
        private FateCardDeck _fateCardDeck;



        [TestMethod]
        public void Shuffle_WhenCalled_ShufflesAllCardsIntoYourDeck()
        {
            Assert.AreEqual(54, _fateCardDeck.FateDeckStack.Count);
        }

        [TestMethod]
        public void DrawCard_WhenCalled_RemovesACardFromTheDeckAndAddOneToTheHand()
        {
             var card =_fateCardDeck.DrawCard();

            Assert.AreEqual(53, _fateCardDeck.FateDeckStack.Count);
            Assert.AreEqual(1, _fateCardDeck.HandOfCards.Count);

        }

        [TestInitialize]
        public void Setup()
        {
            _fateCardDeck = new FateCardDeck();
            _fateCardDeck.Shuffle();
        }


    }
}
