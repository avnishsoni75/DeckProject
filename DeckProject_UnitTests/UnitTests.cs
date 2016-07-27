using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using DeckProjectLib;

namespace DeckProject_UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void ExistenceOfDeck()
        {
            var deck = new Deck();
            Assert.IsNotNull(deck);
        }

        [TestMethod]
        public void DecksCapacityIs52Cards()
        {
            var deck = new Deck();
            Assert.AreEqual(52, deck.Capacity);
        }

        [TestMethod]
        public void DeckInitiallyHas52Cards()
        {
            var deck = new Deck();
            Assert.AreEqual(52, deck.Count);
        }

        [TestMethod]
        public void FirstCardInDeckIsAceOfSpades()
        {
            var deck = new Deck();
            Assert.AreEqual("Ace of Spades", deck[0].ToString());
        }

        [TestMethod]
        public void LastCardInDeckIsKingOfDiamonds()
        {
            var deck = new Deck();
            Assert.AreEqual("King of Diamonds", deck[deck.Count - 1].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NoMoreThan52CardsInDeck()
        {
            var deck = new Deck();
            var card = deck[52];
        }

        [TestMethod]
        public void DeckHas4Suits()
        {
            var deck = new Deck();

            Assert.AreEqual(4, deck.Cards.Select(x => x.Suit).Distinct().Count());
        }

        [TestMethod]
        public void EachSuitHas13Values()
        {
            var deck = new Deck();
            var bySuit = deck.Cards.GroupBy(x => x.Suit);
            foreach (var suit in bySuit)
            {
                Assert.AreEqual(13, suit.Select(x => x.Value).Distinct().Count());
            }
        }

        [TestMethod]
        public void TestCardEquals()
        {
            var deck = new Deck();
            Assert.IsTrue(deck[0] == deck[0]);
            Assert.IsFalse(deck[0] == deck[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateInvalidCard()
        {
            var card = new Card(0, 0);
        }

        [TestMethod]
        // This is based from the fact that, the more the cards are shuffled, the average of the cards at any one particular index
        // will converge to 25.5 for cards labelled 0 to 51.  This test performs 50 shuffles and simply ensure the average of a 
        // random index point is between 20 and 30.
        public void CardsAreShuffled()
        {
            var deck = new Deck();

            // Label the cards from 0 to 51 (pre-shuffle)
            var cardIndexed = deck.Cards.Select((n, i) => new { Text = n, Index = i }).ToList();

            var rnd = new Random();
            var idx = rnd.Next(51);
            const int numOfShuffles = 50;
            var holder = new List<int>(numOfShuffles);

            for (var i = 0; i < numOfShuffles; i++)
            {
                deck.Shuffle();

                // Check it still has 4 unique suits
                Assert.AreEqual(4, deck.Cards.Select(x => x.Suit).Distinct().Count());

                // Check each suit still has 13 distinct values
                var bySuit = deck.Cards.GroupBy(x => x.Suit);
                foreach (var suit in bySuit)
                {
                    Assert.AreEqual(13, suit.Select(x => x.Value).Distinct().Count());
                }
                
                // Check average of random index point is converging to 25.5
                var joined = (from c in deck.Cards
                             join a in cardIndexed on c equals a.Text
                             select new { Index = a.Index }).ToList();
                holder.Add(joined[idx].Index);
            }
            var ans = holder.Average();
            Assert.IsTrue(ans > 20 && ans < 30);
        }

        [TestMethod]
        public void Deal1Card()
        {
            var deck = new Deck();
            deck.Shuffle();
            var firstCard = deck[0];

            var myCards = deck.Deal();
            Assert.IsTrue(firstCard == myCards[0]);
            Assert.AreEqual(deck.Count, 51);
        }

        [TestMethod]
        public void Deal5Cards()
        {
            var deck = new Deck();
            deck.Shuffle();

            var myCards = deck.Deal(5);
            Assert.AreEqual(deck.Count, 47);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Deal60Cards()
        {
            var deck = new Deck();
            deck.Shuffle();

            var myCards = deck.Deal(60);
        }
    }
}
