using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish.Tests
{
    [TestClass()]
    public class CardTests
    {
        private List<Card> cards = new List<Card>()
            {
                new Card(Suits.Diamonds, Values.Ten),
                new Card(Suits.Spades, Values.Queen),
                new Card(Suits.Hearts, Values.Seven),
                new Card(Suits.Hearts, Values.Jack),
                new Card(Suits.Diamonds, Values.Queen)
            };

    [TestMethod()]
        public void CardToStringTest()
        {
            var card = new Card(Suits.Clubs, Values.Ace);

            Assert.AreEqual("Ace of Clubs", card.ToString());
        }

        [TestMethod()]
        public void CartCompareTest()
        {
           
            cards.Sort(new CardComparer());    
        
            var result = new List<Card>()
            {
                new Card(Suits.Hearts, Values.Seven),
                new Card(Suits.Diamonds,Values.Ten),
                new Card(Suits.Hearts, Values.Jack),
                new Card(Suits.Spades, Values.Queen),
                new Card(Suits.Diamonds, Values.Queen)
            };

            Assert.IsTrue(result[0].ToString()==cards[0].ToString());
            Assert.AreEqual(result[4].ToString(), cards[4].ToString());
    }
        /// <summary>
        /// Иногда фейлится, потому что карта может остаться на том же месте в рез-те перемешивания
        /// </summary>
        [TestMethod()]
        public void ShuffleTest()
        {
            Deck deck = new Deck(cards);
            deck.Shuffle();

           Assert.AreNotEqual(deck.Cards[0], cards[0]);
        }
    }
}