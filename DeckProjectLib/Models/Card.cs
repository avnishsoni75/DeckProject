using System;

namespace DeckProjectLib
{
    public class Card : IEquatable<Card>
    {
        Suit _suit;
        Value _value;

        public Card(Suit suit, Value value)
        {
            if (!Enum.IsDefined(typeof(Suit), suit))
                throw new ArgumentOutOfRangeException("Invalid Suit!");

            if (!Enum.IsDefined(typeof(Value), value))
                throw new ArgumentOutOfRangeException("Invalid Card Value!");

            _suit = suit;
            _value = value;
        }

        public Suit Suit { get { return _suit; } }
        public Value Value { get { return _value; } }

        public bool Equals(Card other)
        {
            if (other == null)
                return false;

            return ((Suit == other.Suit) && (Value == other.Value));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Card);
        }

        public static bool operator ==(Card card1, Card card2)
        {
            if (((object)card1) == null || ((object)card2) == null)
                return Object.Equals(card1, card2);

            return card1.Equals(card2);
        }

        public static bool operator !=(Card card1, Card card2)
        {
            if (((object)card1) == null || ((object)card2) == null)
                return !Object.Equals(card1, card2);

            return !card1.Equals(card2);
        }

        // Must override this method if overriding Equals!
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} of {1}", _value.ToString(), _suit.ToString());
        }
    }
}
