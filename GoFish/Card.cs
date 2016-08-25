﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish
{
    public enum Suits
    {
        Spades,
        Clubs,
        Diamonds, 
        Hearts
    }

    public enum Values
    {
        Ace=1,
        Two=2,
        Three=3,
        Four=4,
        Five=5,
        Six=6,
        Seven = 7,
        Eight=8,
        Nine=9,
        Ten =10,
        Jack=11,
        Queen=12,
        King=13
    }

    public class Card
    {
        public Suits Suit { get; set; }
        public Values Value { get; set; }

        public Card(Suits suit, Values value)
        {
            Suit = suit;
            Value = value;
        }

        public override string ToString()
        {
            return string.Format(Value.ToString() + " of " + Suit.ToString());
        }

        public bool DoesCardMatch(Suits suit)
        {
            if (Suit == suit) return true;
            else return false;
        }

        public bool DoesCardMatch(Values value)
        {
            if (Value == value) return true;
            else return false;
        }
    }
}
