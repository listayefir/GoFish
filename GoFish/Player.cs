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
        //public Deck Source { get; set; }
        public Values? HasBookOf { get; private set; }
        public int BooksCount { get;set; }
        public string Question { get; private set; }

        public Player(string name, Deck source)
        {
            Name = name;

            Random rnd = new Random();
            var cards = new List<Card>();
            Hand = new Deck();
            Hand.Cards.Clear();
            source.Shuffle();
            TakeCardFromDeck(5,source);
            BooksCount = 0;
            HasBookOf = null;
        }

        public bool AskForCard(Player player1, Player player2, Values value)
        {
            var questionString = new StringBuilder();
            questionString.Append(Name + " asked for " + value.ToString() + "s\n");
            bool result=false;
            if (player1.HasCard(value))
            {
                questionString.Append(string.Format("{0} has {1} of {2}s\n", player1.Name, TakeCard(player1, value), value));
                result = true;
            }
            else
                questionString.Append(player1.Name + " has 0 " + value + "s\n");
            if (player2.HasCard(value))
            {
                questionString.Append(string.Format("{0} has {1} of {2}s\n", player2.Name, TakeCard(player2, value), value));
                result = true;
            }
            else
                questionString.Append(player2.Name + " has 0 " + value + "s\n");

            Question = questionString.ToString();
            return result;
           
         }

        public bool CheckForBooks()
        {
            Values result=0;
            var arrOfValues = new int[14];
            foreach (var card in Hand.Cards)
            {
                arrOfValues[(int)card.Value]++;
            }
            foreach (var value in arrOfValues)
            {
                if (value == 4) result = (Values)Array.IndexOf(arrOfValues, value);
            }
            if (result != 0)
            {
                BooksCount++;
                HasBookOf = result;
                return true;
            }
            else
            {
                HasBookOf = null;
                return false;
            }
        }

        public int TakeCard(Player player, Values value)
        {
            var selectedCards = player.Hand.Cards
                .Where(x => x.Value == value)
                .ToList();
            foreach (var card in selectedCards)
            {
                Hand.Add(card);
                player.Hand.Deal(player.Hand.Cards.IndexOf(card));
            }
            return selectedCards.Count;
        }

        public void TakeCardFromDeck(int count, Deck source)
        {
            if (count == 0) return;
            for (int i=0; i < count; i++)
            {
                Hand.Add(source.Deal());
            }
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

        public string RemoveBook()
        {
            if (HasBookOf == null) return string.Empty;
            var cardsToRemove = Hand.Cards.Where(x => x.Value == HasBookOf).ToList();
            foreach(var card in cardsToRemove)
            {
                Hand.Cards.Remove(card);
            }
            return string.Format(Name + " has book of " + HasBookOf+"s\n"); 
        }

        public void MakeUpWithBooks()
        {
            if (CheckForBooks())
            {
                RemoveBook();
            }
        }

        //public void RefreshSource(Deck source)
        //{
        //    Source = source;
        //}
    }
}
