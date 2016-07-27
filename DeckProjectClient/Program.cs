using System;
using DeckProjectLib;

namespace DeckProjectClient
{
    class Program
    {
        static Deck _deck;

        static void Main(string[] args)
        {
            StartNew();
        }

        static void StartNew()
        {
            _deck = new Deck();
            Console.WriteLine("New unshuffled deck created");
            DisplayMenu();
        }

        static void DisplayMenu()
        {
            Console.WriteLine("Please choose from one of the below options:");
            Console.WriteLine("1. Shuffle");
            Console.WriteLine("2. Deal Card(s)");
            Console.WriteLine("Press <Esc> to exit program, or 'r' to create new unshuffled deck");

            var selection = Console.ReadKey(true);

            switch (selection.KeyChar)
            {
                case (char)ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                case 'r':
                    Console.WriteLine();
                    StartNew();
                    break;
                case '1':
                    _deck.Shuffle();
                    Console.WriteLine();
                    Console.WriteLine("Deck Shuffled");
                    DisplayMenu();
                    break;
                case '2':
                    DealCards();
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry - please try again");
                    DisplayMenu();
                    break;
            }
        }

        static void DealCards()
        {
            Console.WriteLine();
            Console.WriteLine("How many cards to deal?");
            var numAsStr = Console.ReadLine();
            int num;
            if (!int.TryParse(numAsStr, out num))
            {
                Console.WriteLine("Invalid number - please try again:");
                DealCards();
                return;
            }

            try
            {
                var cardsDealt = _deck.Deal(num);

                var ctr = 0;
                foreach (var card in cardsDealt)
                {
                    Console.WriteLine("Card " + ++ctr + " : " + card);
                }
                Console.WriteLine("Cards remaining in pack: " + _deck.Count);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error occurred: " + ex.Message);
            }
            finally
            {
                DisplayMenu();
            }
        }
    }
}
