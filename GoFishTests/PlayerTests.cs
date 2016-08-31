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
            new Card(Suits.Spades, Values.Two),
            new Card(Suits.Spades, Values.Seven)
        };

        private static Deck sourceDeck = new Deck(source);
        private Player humanPlayer = new Player("Vova", new Deck());
        private Player compPlayer1 = new Player("Comp1", new Deck());
        private Player compPlayer2 = new Player("Comp2", new Deck());

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
            compPlayer1.Hand = new Deck(cards1);
            compPlayer2.Hand = new Deck(cards2);
        }

        [TestMethod]
        public void HasCardTest()
        {
            SetPlayersHands();

            Assert.IsTrue(humanPlayer.HasCard(Values.Five));
            Assert.IsFalse(humanPlayer.HasCard(Values.Ace));
        }

        [TestMethod()]
        public void TakeCardFromDeckTest()
        {
            humanPlayer.TakeCardFromDeck(1, new Deck(source));

            Assert.AreEqual(humanPlayer.Hand.Cards[5].ToString(), "Two of Diamonds");
        }

        [TestMethod()]
        public void TakeCardTest()
        {
            SetPlayersHands();
            
            humanPlayer.TakeCard(compPlayer1, Values.Two);

            Assert.AreEqual(humanPlayer.Hand.Cards[5].ToString(), "Two of Hearts");
            Assert.AreEqual(compPlayer1.Hand.Cards[0].ToString(), "Six of Clubs");
        }

        [TestMethod()]
        public void CheckForBooksTest()
        {
            SetPlayersHands();
            compPlayer1.Hand.Cards.Clear();
            List<Card> cards = new List<Card>()
            {
                new Card(Suits.Diamonds, Values.Ten),
                new Card(Suits.Spades, Values.Ten),
                new Card(Suits.Hearts, Values.Ten),
                new Card(Suits.Clubs, Values.Ten),
                new Card(Suits.Diamonds, Values.Queen),
                new Card(Suits.Clubs, Values.King)
            };

            compPlayer1.Hand = new Deck(cards);
            humanPlayer.CheckForBooks();
            compPlayer1.CheckForBooks();
            Assert.AreEqual(null, humanPlayer.HasBookOf);
            Assert.AreEqual(Values.Ten, compPlayer1.HasBookOf);
            //Assert.AreEqual(Values.Ten, player2.HasBookOf);

        }

        [TestMethod]
        public void RemoveBookTest()
        {
            humanPlayer.Hand.Cards.Clear();
            List<Card> cards = new List<Card>()
            {
                new Card(Suits.Diamonds, Values.Ten),
                new Card(Suits.Spades, Values.Ten),
                new Card(Suits.Hearts, Values.Ten),
                new Card(Suits.Clubs, Values.Ten),
                new Card(Suits.Diamonds, Values.Queen)
            };

            humanPlayer.Hand = new Deck(cards);

            humanPlayer.CheckForBooks();
            humanPlayer.RemoveBook();
            Assert.AreEqual("Vova has book of Tens\n",humanPlayer.BookInfo);
        }

        [TestMethod]
        public void AskForCardWhenHasTest()
        {
           SetPlayersHands();

            var result = humanPlayer.AskForCard(compPlayer1, compPlayer2, Values.Six);
            string question = "Vova asked for Sixs\n" +
                              "Comp1 has 1 of Sixs\n" +
                              "Comp2 has 2 of Sixs\n";
                

            Assert.IsTrue(result);
            Assert.AreEqual(question, humanPlayer.Question);
        }

        [TestMethod]
        public void AskForCardWhenHasNotTest()
        {
            SetPlayersHands();

            var result = humanPlayer.AskForCard(compPlayer1, compPlayer2, Values.King);
            string question = "Vova asked for Kings\n" +
                              "Comp1 has 0 Kings\n" +
                              "Comp2 has 0 Kings\n";

            Assert.IsFalse(result);
            Assert.AreEqual(question,humanPlayer.Question);
        }

        //Игроки 2 и 3 отдают игроку 1 карты
        //результат выполнения метода - false
        [TestMethod]
        public void RoundForPlayerTestFalse()
        {
            SetPlayersHands();
            var result = humanPlayer.PlayRound(new List<Player>() { compPlayer1, compPlayer2 }, Values.Six, sourceDeck);
            string question = "Vova asked for Sixs\n" +
                              "Comp1 has 1 of Sixs\n" +
                              "Comp2 has 2 of Sixs\n";
            Assert.IsTrue(humanPlayer.HasBookOf == Values.Six);
            Assert.AreEqual("Four of Spades", humanPlayer.Hand.Cards[3].ToString());
            Assert.IsFalse(result);
            Assert.AreEqual(question, humanPlayer.Question);
        }

        //Игроки 2 и 3 НЕ отдают игроку 1 карты
        //в ресурсной колоде 5 карт
        //результат выполнения метода - false
        [TestMethod]
        public void RoundForPlayerTestFalse1()
        {
            SetPlayersHands();
            var result = humanPlayer.PlayRound(new List<Player>() { compPlayer1, compPlayer2 }, Values.Seven, sourceDeck);
            string question = "Vova asked for Sevens\n" +
                              "Comp1 has 0 Sevens\n" +
                              "Comp2 has 0 Sevens\n";
            Assert.AreEqual("Two of Diamonds", humanPlayer.Hand.Cards[5].ToString());
            Assert.AreEqual("Three of Spades", sourceDeck.Cards[0].ToString());
            Assert.IsFalse(result);
            Assert.AreEqual(question, humanPlayer.Question);
        }


        //Игрок 1 собирает комплект, берет из колоды 5 карт, в колоде еще остаются карты
        //результат выполнения метода - false
        [TestMethod]
        public void RoundForPlayerTestFalse2()
        {
            SetPlayersHands();
            humanPlayer.Hand.Cards.Clear();
            humanPlayer.Hand= new Deck(new List<Card>() {new Card(Suits.Hearts, Values.Six)});

            var result = humanPlayer.PlayRound(new List<Player>() { compPlayer1, compPlayer2 }, Values.Six, sourceDeck);
            string question = "Vova asked for Sixs\n" +
                              "Comp1 has 1 of Sixs\n" +
                              "Comp2 has 2 of Sixs\n";

            Assert.AreEqual("Two of Diamonds", humanPlayer.Hand.Cards[0].ToString());
            Assert.AreEqual("Two of Spades", humanPlayer.Hand.Cards[4].ToString());
            Assert.AreEqual("Seven of Spades", sourceDeck.Cards[0].ToString());
            Assert.IsTrue(sourceDeck.Cards.Count==1);
            Assert.IsFalse(result);
            Assert.AreEqual(question, humanPlayer.Question);
        }

        //Игрок 1 собирает комплект, берет из колоды 5 карт, в колоде больше не остается карт
        //результат выполнения метода - true
        [TestMethod]
        public void RoundForPlayerTestTrue()
        {
            SetPlayersHands();
            humanPlayer.Hand.Cards.Clear();
            humanPlayer.Hand = new Deck(new List<Card>() { new Card(Suits.Hearts, Values.Six) });
            sourceDeck.Deal(5);
            var result = humanPlayer.PlayRound(new List<Player>() { compPlayer1, compPlayer2 }, Values.Six, sourceDeck);
            string question = "Vova asked for Sixs\n" +
                              "Comp1 has 1 of Sixs\n" +
                              "Comp2 has 2 of Sixs\n";

            Assert.AreEqual("Two of Diamonds", humanPlayer.Hand.Cards[0].ToString());
            Assert.AreEqual("Two of Spades", humanPlayer.Hand.Cards[4].ToString());
            Assert.IsTrue(sourceDeck.Cards.Count == 0);
            Assert.IsTrue(result);
            Assert.AreEqual(question, humanPlayer.Question);
        }

        
        





    }
}
