using System;
using GoFish;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace GoFishTests
{
    [TestClass]
    public class PlayerTests
    {
        private static List<Card> source = new List<Card>()
        {
            new Card(Suits.Diamonds, Values.Two),
            new Card(Suits.Spades, Values.Three),
            new Card(Suits.Hearts, Values.Four)
        };
        
        private Player player = new Player("Vova", new Deck(source));

        [TestMethod]
        public void HasCardTest()
        {
            SetPlayersHand();

            Assert.IsTrue(player.HasCard(Values.Seven));
            Assert.IsTrue(player.HasCard(Values.Queen));
            Assert.IsFalse(player.HasCard(Values.Ace));
        }

        [TestMethod()]
        public void TakeCardFromDeckTest()
        {
            player.TakeCardFromDeck();

            Assert.AreEqual(player.Hand.Cards[5].ToString(), "Two of Diamonds");
        }

        [TestMethod()]
        public void TakeCardTest()
        {
            var player2 = new Player("Vasya", new Deck(source));
            player2.Hand.Cards.Clear();
            List<Card> cards = new List<Card>()
            {
                new Card(Suits.Diamonds, Values.Ten),
                new Card(Suits.Spades, Values.Queen),
                new Card(Suits.Hearts, Values.Seven),
                new Card(Suits.Hearts, Values.Jack),
                new Card(Suits.Diamonds, Values.Queen)
            };

            player2.Hand = new Deck(cards);
            
            player.TakeCard(player2, Values.Ten);

            Assert.AreEqual(player.Hand.Cards[5].ToString(), "Ten of Diamonds");
            Assert.AreEqual(player2.Hand.Cards[0].ToString(), "Queen of Spades");
        }

        [TestMethod()]
        public void CheckForBooksTest()
        {
            SetPlayersHand();
            var player2 = new Player("Petya", new Deck());
            player2.Hand.Cards.Clear();
            List<Card> cards = new List<Card>()
            {
                new Card(Suits.Diamonds, Values.Ten),
                new Card(Suits.Spades, Values.Ten),
                new Card(Suits.Hearts, Values.Ten),
                new Card(Suits.Clubs, Values.Ten),
                new Card(Suits.Diamonds, Values.Queen),
                new Card(Suits.Clubs, Values.King)
            };

            player2.Hand = new Deck(cards);

            Assert.IsTrue(player.HasBookOf == null);
            Assert.IsTrue(player2.HasBookOf == Values.Ten);

        }

        public void SetPlayersHand()
        {
            player.Hand.Cards.Clear();
            List<Card> cards = new List<Card>()
            {
                new Card(Suits.Diamonds, Values.Ten),
                new Card(Suits.Spades, Values.Queen),
                new Card(Suits.Hearts, Values.Seven),
                new Card(Suits.Hearts, Values.Jack),
                new Card(Suits.Diamonds, Values.Queen)
            };

            player.Hand = new Deck(cards);
        }
    }
}
