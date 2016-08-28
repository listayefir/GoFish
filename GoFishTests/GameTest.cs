using System;
using System.Collections.Generic;
using GoFish;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoFishTests
{
    [TestClass]
    public class GameTest
    {
        private static List<Card> source = new List<Card>()
        {
            new Card(Suits.Diamonds, Values.Two),
            new Card(Suits.Spades, Values.Three),
            new Card(Suits.Hearts, Values.Four),
            new Card(Suits.Clubs,  Values.Five),
            new Card(Suits.Spades, Values.Two)
        };

        private static Deck sourceDeck = new Deck(source);
        private Player humanPlayer = new Player("Vova", sourceDeck);
        private Player compPlayer1 = new Player("Comp1", sourceDeck);
        private Player compPlayer2 = new Player("Comp2", sourceDeck);

        private Game game = new Game();

        private void SetGame()
        {
            game.HumanPlayer = humanPlayer;
            game.CompPlayer1 = compPlayer1;
            game.CompPlayer2 = compPlayer2;
            game.Source = sourceDeck;
        }
        private void SetPlayersHands()
        {
            humanPlayer.Hand.Cards.Clear();
            compPlayer1.Hand.Cards.Clear();
            compPlayer2.Hand.Cards.Clear();

            List<Card> cards = new List<Card>()
            {
                new Card(Suits.Diamonds, Values.Five),
                new Card(Suits.Clubs, Values.Three),
                new Card(Suits.Hearts, Values.Three),
                new Card(Suits.Hearts, Values.Six),
                new Card(Suits.Spades, Values.Four)
            };

            List<Card> cards1 = new List<Card>()
            {
                new Card(Suits.Hearts, Values.Two),
                new Card(Suits.Clubs, Values.Six),
                new Card(Suits.Spades, Values.Five),
                new Card(Suits.Diamonds, Values.Four),
                new Card(Suits.Hearts, Values.Five)
            };

            List<Card> cards2 = new List<Card>()
            {
                new Card(Suits.Clubs, Values.Two),
                new Card(Suits.Diamonds, Values.Six),
                new Card(Suits.Spades, Values.Six),
                new Card(Suits.Diamonds, Values.Three),
                new Card(Suits.Clubs, Values.Four)
            };

            humanPlayer.Hand = new Deck(cards);
            compPlayer1.Hand=new Deck(cards1);
            compPlayer2.Hand=new Deck(cards2);
        }
        [TestMethod]
        public void TwoWinnersTest()
        {
            humanPlayer.BooksCount = 2;
            compPlayer1.BooksCount = 2;
            compPlayer2.BooksCount = 1;

            var result = game.GetWinnerName();
            var answer = "We have two winners: Vova and Comp1";

            Assert.AreEqual(answer,result);
        }

        [TestMethod]
        public void OneWinnerTest()
        {
            humanPlayer.BooksCount = 3;
            compPlayer1.BooksCount = 1;
            compPlayer2.BooksCount = 1;

            var result = game.GetWinnerName();
            var answer = "Vova wins!";

            Assert.AreEqual(answer, result);
        }
    }
}
