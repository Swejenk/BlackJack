using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Commands;
using Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Commands.Tests
{
    [TestClass()]
    public class GameCommandsTests
    {
        GameCommands _gameCommands = new GameCommands();

        [TestMethod()]
        public void ValueFrom8AndKing()
        {

            var Card1 = new Card() { CardNumber = 1, Suit = Model.EnumTypes.SuitType.Clubs, Value = 8 };
            var Card2 = new Card() { CardNumber = 1, Suit = Model.EnumTypes.SuitType.Clubs, Value = 13, Face = Model.EnumTypes.FaceType.King };

            var cards = new List<Card>();
            cards.Add(Card1);
            cards.Add(Card2);

            var expected = 18;

            var result = _gameCommands.GetHandValue(cards);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void StatusShouldBeBlackJack()
        {

            var Card1 = new Card() { CardNumber = 1, Suit = Model.EnumTypes.SuitType.Clubs, Value = 11, Face = EnumTypes.FaceType.Jack };
            var Card2 = new Card() { CardNumber = 1, Suit = Model.EnumTypes.SuitType.Clubs, Value = 1, Face = EnumTypes.FaceType.Ace };

            var cards = new List<Card>();
            cards.Add(Card1);
            cards.Add(Card2);

            var expectedStatus = "Blackjack";
            var expectedHandValue = 21;
            var boolForblackjack = true;
            var boolForBust = false;

            var result = _gameCommands.GetStatus(cards);
            Assert.AreEqual(expectedStatus, result.Status);
            Assert.AreEqual(expectedHandValue, result.HandValue);
            Assert.AreEqual(boolForblackjack, result.Blackjack);
            Assert.AreEqual(boolForBust, result.Bust);
        }

        [TestMethod()]
        public void StatusShouldBeBustAndNotBlackJack()
        {

            var Card1 = new Card() { CardNumber = 1, Suit = Model.EnumTypes.SuitType.Hearts, Value = 12, Face = EnumTypes.FaceType.Queen };
            var Card2 = new Card() { CardNumber = 1, Suit = Model.EnumTypes.SuitType.Clubs, Value = 2 };
            var Card3 = new Card() { CardNumber = 1, Suit = Model.EnumTypes.SuitType.Spades, Value = 12, Face = EnumTypes.FaceType.Queen };
            

            var cards = new List<Card>();
            cards.Add(Card1);
            cards.Add(Card2);
            cards.Add(Card3);
 

            var expectedStatus = "Bust";
            var expectedHandValue = 22;
            var boolForblackjack = false;
            var boolForBust = true;

            var result = _gameCommands.GetStatus(cards);
            Assert.AreEqual(expectedStatus, result.Status);
            Assert.AreEqual(expectedHandValue, result.HandValue);
            Assert.AreEqual(boolForblackjack, result.Blackjack);
            Assert.AreEqual(boolForBust, result.Bust);
        }
        [TestMethod()]
        public void AceShouldBeEleven()
        {

            var aceCard = new Card() { CardNumber = 1, Suit = Model.EnumTypes.SuitType.Clubs, Value = 11, Face = Model.EnumTypes.FaceType.Ace };

            var acecards = new List<Card>();
            acecards.Add(aceCard);

            var expected = 11;

            var result = _gameCommands.DecideIfAceIsHighOrLow(acecards, 10);
            Assert.AreEqual(expected, result);

        }
        [TestMethod()]
        public void AceShouldBeOne()
        {

            var aceCard = new Card() { CardNumber = 1, Suit = Model.EnumTypes.SuitType.Clubs, Value = 11, Face = Model.EnumTypes.FaceType.Ace };

            var acecards = new List<Card>();
            acecards.Add(aceCard);

            var expected = 1;

            var result = _gameCommands.DecideIfAceIsHighOrLow(acecards, 15);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void FaceCardsShouldBeTen()
        {

            var faceCardKing = new Card() { CardNumber = 1, Suit = Model.EnumTypes.SuitType.Clubs, Value = 13, Face = Model.EnumTypes.FaceType.King };
            var faceCardQueen = new Card() { CardNumber = 1, Suit = Model.EnumTypes.SuitType.Clubs, Value = 12, Face = Model.EnumTypes.FaceType.Queen };
            var faceCardJack = new Card() { CardNumber = 1, Suit = Model.EnumTypes.SuitType.Clubs, Value = 11, Face = Model.EnumTypes.FaceType.Jack };

            var facecards = new List<Card>();
            facecards.Add(faceCardKing);
            facecards.Add(faceCardQueen);
            facecards.Add(faceCardJack);

            var expected = 10;

            var result = _gameCommands.FindFaceCards(facecards);

            Assert.AreEqual(expected, result[0].Value);
            Assert.AreEqual(expected, result[1].Value);
            Assert.AreEqual(expected, result[2].Value);
        }


     
    }
}