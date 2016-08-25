using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish
{
    public class Deck
    {
        public List<Card> Cards { get; set; }
        private Random rnd = new Random();
        public int Count { get { return Cards.Count; } }
        

        public Deck()
        {
            Cards = new List<Card>();
            for (int suit = 0; suit<=3;suit++)
                for(int value = 1; value<=13; value++)
                    Cards.Add(new Card((Suits)suit,(Values)value));
        }

        public Deck(IEnumerable<Card> initialCards)
        {
            Cards = new List<Card>(initialCards);
        }

        public void Add(Card cardToAdd)
        {
            Cards.Add(cardToAdd);
        }

        public Card Deal(int index)
        {
            Card cardToDeal = Cards[index];
            Cards.RemoveAt(index);
            return cardToDeal;
        }

        public Card Deal()
        {
            return Deal(0);
        }

        public void Shuffle()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                var temp = Cards[i];
                var a = rnd.Next(0, Cards.Count - 1);
                Cards[i] = Cards[a];
                Cards[a] = temp;
            }
        }

        public IEnumerable<string> GetCardNames()
        {
            foreach (var card in Cards)
            {
                yield return card.ToString();
            }
        }

        public void Sort()
        {
            Cards.Sort(new CardComparer());
        }

    }
}
