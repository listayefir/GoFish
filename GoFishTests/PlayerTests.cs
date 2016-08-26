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
            new Card(Suits.Hearts, Values.Four),
            new Card(Suits.Clubs,  Values.Five),
            new Card(Suits.Hearts, Values.Six),
            new Card(Suits.Diamonds, Values.Seven)
        };

        private List<Card> cards = new List<Card>()
            {
                new Card(Suits.Diamonds, Values.Ten),
                new Card(Suits.Spades, Values.Queen),
                new Card(Suits.Hearts, Values.Seven),
                new Card(Suits.Hearts, Values.Jack),
                new Card(Suits.Diamonds, Values.Queen)
            };


        private Player player = new Player("Vova", new Deck(source));

        [TestMethod]
        public void HasCardTest()
        {
            SetPlayersHand(player,cards);

            Assert.IsTrue(player.HasCard(Values.Seven));
            Assert.IsTrue(player.HasCard(Values.Queen));
            Assert.IsFalse(player.HasCard(Values.Ace));
        }

        [TestMethod()]
        public void TakeCardFromDeckTest()
        {
            player.TakeCardFromDeck(1, new Deck(source));

            Assert.AreEqual(player.Hand.Cards[5].ToString(), "Two of Diamonds");
        }

        [TestMethod()]
        public void TakeCardTest()
        {
            var player2 = new Player("Vasya", new Deck(source));
            SetPlayersHand(player2,cards);
            
            player.TakeCard(player2, Values.Ten);

            Assert.AreEqual(player.Hand.Cards[5].ToString(), "Ten of Diamonds");
            Assert.AreEqual(player2.Hand.Cards[0].ToString(), "Queen of Spades");
        }

        [TestMethod()]
        public void CheckForBooksTest()
        {
            SetPlayersHand(player, this.cards);
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
            player.CheckForBooks();
            player2.CheckForBooks();
            Assert.AreEqual(null, player.HasBookOf);
            Assert.AreEqual(Values.Ten, player2.HasBookOf);
            //Assert.AreEqual(Values.Ten, player2.HasBookOf);

        }

        public void SetPlayersHand(Player player,IEnumerable<Card> cards )
        {
            player.Hand.Cards.Clear();
            player.Hand = new Deck(cards);
        }

        [TestMethod]
        public void RemoveBookTest()
        {
            player.Hand.Cards.Clear();
            List<Card> cards = new List<Card>()
            {
                new Card(Suits.Diamonds, Values.Ten),
                new Card(Suits.Spades, Values.Ten),
                new Card(Suits.Hearts, Values.Ten),
                new Card(Suits.Clubs, Values.Ten),
                new Card(Suits.Diamonds, Values.Queen)
            };

            player.Hand = new Deck(cards);

            player.CheckForBooks();
            string result = player.RemoveBook();
            Assert.AreEqual("Vova has book of Tens\n",result);
        }

        [TestMethod]
        public void AskForCardWhenHasTest()
        {
            var player1 = new Player("Vasya", new Deck(source));
            var player2 = new Player("Anna", new Deck(source));
            SetPlayersHand(player,cards);
            SetPlayersHand(player1,cards);
            SetPlayersHand(player2,cards);

            var result = player.AskForCard(player1, player2, Values.Ten);
            string question = "Vova asked for Tens\n" +
                              "Vasya has 1 of Tens\n" +
                              "Anna has 1 of Tens\n";
                

            Assert.IsTrue(result);
            Assert.AreEqual(question, player.Question);
        }

        [TestMethod]
        public void AskForCardWhenHasNotTest()
        {
            var player1 = new Player("Vasya", new Deck(source));
            var player2 = new Player("Anna", new Deck(source));
            SetPlayersHand(player, cards);
            SetPlayersHand(player1, cards);
            SetPlayersHand(player2, cards);

            var result = player.AskForCard(player1, player2, Values.King);
            string question = "Vova asked for Kings\n" +
                              "Vasya has 0 Kings\n" +
                              "Anna has 0 Kings\n";

            Assert.IsFalse(result);
            Assert.AreEqual(question,player.Question);
        }


    }
}
