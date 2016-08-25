using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish
{
    public class Player
    {
        public string Name { get; set; }
        public Deck Hand { get; set; }
        public Deck Source { get; set; }
        private int handCount = 5;

        public int booksCount { get; set; }

        public Player(string name, Deck source)
        {
            Name = name;

            Random rnd = new Random();
            var cards = new List<Card>();
            var fullDeck = new Deck();
            for (int i = 0; i < handCount; i++)
            {
                cards.Add(fullDeck.Cards[rnd.Next(0,fullDeck.Count-1)]);
            }
            Hand=new Deck(cards);
            Source = source;
            booksCount = 0;
        }

        public void AskForCard(Player player, Values value)
        {
            if (player.HasCard(value))
            {
                TakeCard(player, value);
            }
            else
            {
                TakeCardFromDeck();
            }
         }

        private Values? CheckForBooks()
        {
            var listOf

        }

        public void TakeCard(Player player, Values value)
        {
            var selectedCards = player.Hand.Cards
                .Where(x => x.Value == value)
                .ToList();
            foreach (var card in selectedCards)
            {
                Hand.Add(card);
                player.Hand.Deal(player.Hand.Cards.IndexOf(card));
            }
        }

        public void TakeCardFromDeck()
        {
            Hand.Add(Source.Deal());
        }

        public bool HasCard(Values value)
        {
            var check = Hand.Cards
                .Select(x => x.Value)
                .Where(x => x == value)
                .ToList();
            if (check.Count != 0) return true;
            else return false;
        }

        public void Refresh(Deck source)
        {
            Source = source;
        }
    }
}
