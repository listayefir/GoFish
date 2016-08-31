using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            new Card(Suits.Hearts, Values.Two),
            new Card(Suits.Clubs,  Values.Two),
            new Card(Suits.Spades, Values.Two)
        };

        private static Deck sourceDeck = new Deck(source);
        private Player humanPlayer = new Player("Vova", new Deck());
        private Player compPlayer1 = new Player("Comp1", new Deck());
        private Player compPlayer2 = new Player("Comp2", new Deck());

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
                new Card(Suits.Clubs,  Values.Five),
                new Card(Suits.Clubs, Values.Six),
                new Card(Suits.Spades, Values.Five),
                new Card(Suits.Diamonds, Values.Four),
                new Card(Suits.Hearts, Values.Five)
            };

            List<Card> cards2 = new List<Card>()
            {
                new Card(Suits.Hearts, Values.Four),
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
            SetGame();
            SetPlayersHands();
            humanPlayer.BooksCount = 2;
            compPlayer1.BooksCount = 2;
            compPlayer2.BooksCount = 1;

            var result = game.GetWinnerName();
            var answer = "We have two winners: Comp1 and Vova";

            Assert.AreEqual(answer,result);
        }

        [TestMethod]
        public void OneWinnerTest()
        {
            SetGame();
            SetPlayersHands();
            humanPlayer.BooksCount = 3;
            compPlayer1.BooksCount = 1;
            compPlayer2.BooksCount = 1;

            var result = game.GetWinnerName();
            var answer = "Vova wins!";

            Assert.AreEqual(answer, result);
        }

        [TestMethod]
        public void PlayOneRoundFalseTest()
        {
            SetGame();
            SetPlayersHands();

            var result = game.PlayOneRound(Values.Six);

            Assert.IsTrue(humanPlayer.HasBookOf==Values.Six);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PlayOneRoundTrueTest()
        {
            SetGame();
            SetPlayersHands();
            humanPlayer.Hand.Cards.Clear();
            humanPlayer.Hand = new Deck(new List<Card>() { new Card(Suits.Hearts, Values.Six) });
            string request = "Vova asked for Sixs\n" +
                              "Comp1 has 1 of Sixs\n" +
                              "Comp2 has 2 of Sixs\n";
            var result = game.PlayOneRound(Values.Six);

            Assert.AreEqual(request, game.Request);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DescribeBooksTest()
        {
            SetGame();
            SetPlayersHands();
            humanPlayer.PlayRound(new List<Player>() {compPlayer1, compPlayer2}, Values.Six, sourceDeck);
            compPlayer1.PlayRound(new List<Player>() { humanPlayer, compPlayer2 }, Values.Five, sourceDeck);
            compPlayer2.PlayRound(new List<Player>() { humanPlayer, compPlayer1 }, Values.Four, sourceDeck);

            var answer = "Vova has book of Sixs" +
                         "\nComp1 has book of Fives" +
                         "\nComp2 has book of Fours";
            var result = game.DescribeBooks();

            Assert.AreEqual(answer,result);

        }




    }
}
