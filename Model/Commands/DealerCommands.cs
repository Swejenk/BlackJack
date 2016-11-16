using System;
using System.Collections.Generic;
using System.Linq;
using Model.GameObjects;
/**
 * Jens Malm 
 * 2016-09
 * BlackJackGame
 **/
namespace Model.Commands
{
    public class DealerCommands 
    {
        private List<DeckCommands> decks = new List<DeckCommands>();

        public DealerCommands()
        {
            AddDeck();
        }

        public int CardsLeftInActiveDeck()
        {
            return decks[0].GetCardsLeftInDeck();
        }

        public void AddDeck()
        {
            DeckCommands deck = new DeckCommands();
            decks.Add(deck);
            ShuffleDecks();
        }

        public void ShuffleDecks()
        {
            foreach (DeckCommands deck in decks)
            {
                deck.ShuffleDeck();
            }
        }

        public void TestDealDeck()
        {

            for (int i = 0; i < 104; i++)
            {
                Card c = DealCard();
                Console.WriteLine("Räknare: {0} Kortnummer: {1} Suit: {2} Värde: {3}", i, c.CardNumber, c.Suit, c.Value);
                System.Threading.Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Returnerar ett kort, om en kortlek är tom så gå till nästa eller sluta
        /// </summary>
        /// <returns></returns>
        public Card DealCard()
        {
            Card card = new Card();

            DeckCommands deck = GetNextDeck();
            if (deck.GetCardsLeftInDeck() > 0)
            {
                card = deck.GetCard();
            }

            return card;
        }
        public Card DealHiddenCard()
        {
            Card card = new Card();

            DeckCommands deck = GetNextDeck();
            if (deck.GetCardsLeftInDeck() > 0)
            {
                card = deck.GetCard();
                card.HideCard = true;
            }

            return card;
        }
        private DeckCommands GetNextDeck()
        {
            DeckCommands deck =  new DeckCommands();
            if (decks.FirstOrDefault().GetCardsLeftInDeck() == 0)
            {
                decks.Remove(decks.FirstOrDefault());
                AddDeck();
                deck = decks.FirstOrDefault();
            }
            else
            {
                deck = decks.FirstOrDefault();
            }
            return deck;
        }


    }
}
