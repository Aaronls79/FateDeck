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
        public void DrawCard_WhenCalled_RemovesOneCardFromTheDeckAndAddOneToTheHand()
        {
            var card = _fateCardDeck.DrawCard();

            Assert.AreEqual(53, _fateCardDeck.FateDeckStack.Count);
            Assert.AreEqual(1, _fateCardDeck.HandOfCards.Count);

        }

        [TestMethod]
        public void Flip_WhenCalled_RemovesCardFromDeckAndAddsToDiscard()
        {
            _fateCardDeck.Flip();
            _fateCardDeck.DiscardStack.Count.ShouldEqual(1);
            _fateCardDeck.FateDeckStack.Count.ShouldEqual(53);
        }

        [TestInitialize]
        public void Setup()
        {
            _fateCardDeck = new FateCardDeck();
            _fateCardDeck.HandOfCards.Clear();
            _fateCardDeck.Shuffle();
        }

    }
}
