using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.CommandInterfaces;
using Model.GameObjects;
using Model.Commands;
using System.Collections.Generic;

namespace ModelTest.CommandsTest
{
    [TestClass]
    public class DealerCommandsTest: DealerCommands
    {
        List<DeckCommands> decks = new List<DeckCommands>();

        [TestMethod]
        public void AddANewDeckHoldsOneDeck()
        {
            List<DeckCommands> decks = new List<DeckCommands>();
            DeckCommands deck = new DeckCommands();

            decks.Add(deck);
            var numberfDecks = decks.Count;

            Assert.AreEqual(numberfDecks, 1);
        }
        [TestMethod]
        public void DeckHoldFullSuiteOfCards() //Borde testas i DeckCommandsTest
        {
            List<DeckCommands> decks = new List<DeckCommands>();
            DeckCommands deck = new DeckCommands();

            decks.Add(deck);
            int fullDeck = 52;
            var numberOfCardsInDeck = deck.GetCardsLeftInDeck();

            Assert.AreEqual(fullDeck, numberOfCardsInDeck);
        }
    }
}
