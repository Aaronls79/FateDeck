using System;
using System.Collections.Generic;

namespace FateDeck.Web.Models
{
    public class FateDeck
    {
        private readonly FateCard[] _availableCards =
        {
            new FateCard(1, Suite.Crows, "1C"), new FateCard(2, Suite.Crows, "2C"), new FateCard(3, Suite.Crows, "3C"), new FateCard(4, Suite.Crows, "4C"), new FateCard(5, Suite.Crows, "5C"), new FateCard(6, Suite.Crows, "6C"), new FateCard(7, Suite.Crows, "7C"), new FateCard(8, Suite.Crows, "8C"), new FateCard(9, Suite.Crows, "9C"), new FateCard(10, Suite.Crows, "10C"), new FateCard(11, Suite.Crows, "11C"), new FateCard(12, Suite.Crows, "12C"), new FateCard(13, Suite.Crows, "13C"),
            new FateCard(1, Suite.Masks, "1M"), new FateCard(2, Suite.Masks, "2M"), new FateCard(3, Suite.Masks, "3M"), new FateCard(4, Suite.Masks, "4M"), new FateCard(5, Suite.Masks, "5M"), new FateCard(6, Suite.Masks, "6M"), new FateCard(7, Suite.Masks, "7M"), new FateCard(8, Suite.Masks, "8M"), new FateCard(9, Suite.Masks, "9M"), new FateCard(10, Suite.Masks, "10M"), new FateCard(11, Suite.Masks, "11M"), new FateCard(12, Suite.Masks, "12M"), new FateCard(13, Suite.Masks, "13M"),
            new FateCard(1, Suite.Rams, "1R"), new FateCard(2, Suite.Rams, "2R"), new FateCard(3, Suite.Rams, "3R"), new FateCard(4, Suite.Rams, "4R"), new FateCard(5, Suite.Rams, "5R"), new FateCard(6, Suite.Rams, "6R"), new FateCard(7, Suite.Rams, "7R"), new FateCard(8, Suite.Rams, "8R"), new FateCard(9, Suite.Rams, "9R"), new FateCard(10, Suite.Rams, "10R"), new FateCard(11, Suite.Rams, "11R"), new FateCard(12, Suite.Rams, "12R"), new FateCard(13, Suite.Rams, "13R"),
            new FateCard(1, Suite.Tombs, "1T"), new FateCard(2, Suite.Tombs, "2T"), new FateCard(3, Suite.Tombs, "3T"), new FateCard(4, Suite.Tombs, "4T"), new FateCard(5, Suite.Tombs, "5T"), new FateCard(6, Suite.Tombs, "6T"), new FateCard(7, Suite.Tombs, "7T"), new FateCard(8, Suite.Tombs, "8T"), new FateCard(9, Suite.Tombs, "9T"), new FateCard(10, Suite.Tombs, "10T"), new FateCard(11, Suite.Tombs, "11T"), new FateCard(12, Suite.Tombs, "12T"), new FateCard(13, Suite.Tombs, "13T"),
            new FateCard(0, Suite.None, "BJ"), new FateCard(14, Suite.Wild, "RJ")
        };
        private readonly Stack<FateCard> _cardStack = new Stack<FateCard>();
        private readonly Stack<FateCard> _discardStack = new Stack<FateCard>();
        private readonly List<FateCard> _handOfCards = new List<FateCard>();

        public List<FateCard> HandOfCards
        {
            get { return _handOfCards; }
        }
        public Stack<FateCard> FateDeckStack
        {
            get { return _cardStack; }
        }

        public FateCard DrawCard()
        {
            var card = FateDeckStack.Pop();
            _handOfCards.Add(card);
            return card;
        }

        public FateCard Flip()
        {
            var card = FateDeckStack.Pop();
            _discardStack.Push(card);
            return card;
        }

        public void PlayCard(FateCard card)
        {
            var discard = _handOfCards.Find(x => x.Key == card.Key);
            if (discard == null)
                throw new ArgumentException(string.Format("Cheater, you do not have a {0} of {1} in your hand,", card.Value, card.Suite));
            _handOfCards.Remove(discard);
            _discardStack.Push(discard);
        }

        public void Shuffle()
        {
            var random = new Random(DateTime.Now.Millisecond);
            _discardStack.Clear();
            FateDeckStack.Clear();
            var cards = new List<FateCard>(_availableCards);
            cards.RemoveAll(x => HandOfCards.Exists(y => y.Key == x.Key));
            while (FateDeckStack.Count + HandOfCards.Count < _availableCards.Length)
            {
                var index = random.Next(cards.Count - 1);
                FateDeckStack.Push(cards[index]);
                cards.RemoveAt(index);
            }
        }
    }
}