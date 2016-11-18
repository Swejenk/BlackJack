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
        /// <summary>
        /// Return cards left in deck
        /// </summary>
        /// <returns></returns>
        public int CardsLeftInActiveDeck()
        {
            return decks[0].GetCardsLeftInDeck();
        }
        /// <summary>
        /// Add new deck
        /// </summary>
        public void AddDeck()
        {
            DeckCommands deck = new DeckCommands();
            decks.Add(deck);
            ShuffleDecks();
        }
        /// <summary>
        /// Shuffle deck
        /// </summary>
        public void ShuffleDecks()
        {
            foreach (DeckCommands deck in decks)
            {
                deck.ShuffleDeck();
            }
        }
       
        /// <summary>
        /// Deal card, if deck is empty go to next deck
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
        /// <summary>
        /// Deal a hidden card
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Get next deck
        /// </summary>
        /// <returns></returns>
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
