using System.Collections.Generic;
using System.Linq;
using Model.GameObjects;
using Model.EnumTypes;
using IListExtensions;
using System;
using Model.CommandInterfaces;
/**
* Jens Malm 
* 2016-09
* BlackJackGame
**/
namespace Model.Commands
{
    public class DeckCommands: IDeckCommands
    {
        private List<Card> cards = null;

        public DeckCommands()
        {
            cards = new List<Card>();
            LoadDeck();
        }

        private void LoadDeck()
        {
            cards.Clear();
            int cardNumber = 1;
            for (int suit = 0; suit < 4; suit++)
            {
                cardNumber = GetCardsForSuit(cardNumber, suit);
            }
        }

        private int GetCardsForSuit(int cardNumber, int suit)
        {
            for (int card = 1; card < 14; card++)
            {
                Card currentCard = new Card();
                currentCard.CardNumber = cardNumber;
                currentCard.Suit = (SuitType)suit;
                currentCard.FileSuitCharacter = (ImageFileSuitDefinition)suit;
                if (card > 10)
                {
                    currentCard.FileFaceCharacter = (ImageFileFaceDefinition)card;
                }
                currentCard.Value = card;
                cards.Add(currentCard);
                cardNumber++;
            }

            return cardNumber;
        }

        public void ShuffleDeck()
        {
            if (cards != null)
            {
                cards.Shuffle();                
            }
        }

        public Card FindCardByCardnumber(int cardNumber)
        {
            return cards.Where(x => x.CardNumber == cardNumber).FirstOrDefault();
        }

        public int GetCardsLeftInDeck()
        {
            return cards.Count();
        }

        public Card GetCard()
        {
            Card card = null;
            card = cards.FirstOrDefault();
            RemoveCardFromList(card);
            return card;
        }

        private void RemoveCardFromList(Card card)
        {
            if (card != null)
            {
                cards.Remove(card);
            }
        }
    }
}
