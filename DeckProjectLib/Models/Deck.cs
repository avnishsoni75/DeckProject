using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;

namespace DeckProjectLib
{
    public class Deck : IEnumerable<Card>
    {
        readonly List<Card> _cards;
        readonly Random _random;

        public Deck()
        {
            _cards = new List<Card>(52);
            _random = new Random();

            // Initialize deck by suit then by value
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Value value in Enum.GetValues(typeof(Value)))
                {
                    _cards.Add(new Card(suit, value));
                }
            }
        }

        // Ensure access to internal cards is read-only - so as not to write over the internal structure.
        // Note - ReadOnlyCollection is new to .NET 4.5
        public ReadOnlyCollection<Card> Cards
        {
            get { return _cards.AsReadOnly(); }
        }

        public Card this[int index]
        {
            get { return _cards[index]; }
        }

        /// <summary>
        /// Efficiency important - choose to swap elements in List with existing random card.  
        /// Each swap is O(1), hence this is a linear O(n) operation.
        /// </summary>
        public void Shuffle()
        {
            for (var i = _cards.Count - 1; i > 0; i--)
            {
                var idx = _random.Next(i);

                var temp = _cards[i];
                _cards[i] = _cards[idx];
                _cards[idx] = temp;
            }
        }

        // Note - using RemoveAt(index) here which is O(n) - no matter which index is used since an Array.Copy is performed in the background always.
        // We could have used a Stack or Queue which is O(1) for adding and removing - but since there are only ever 52 cards I have stuck with a generic List here.
        public List<Card> Deal(int numOfCards = 1)
        {
            if (numOfCards > Count)
                throw new ArgumentOutOfRangeException("Not enough cards in pack!");

            var list = new List<Card>();
            for (var i = 0; i < numOfCards; i++)
            {
                var card = _cards[0];
                _cards.RemoveAt(0);
                list.Add(card);
            }
            return list;
        }

        public int Capacity { get { return _cards.Capacity; } }

        public int Count { get { return _cards.Count; } }

        public IEnumerator<Card> GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
