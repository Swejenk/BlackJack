using Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * Jens Malm 
 * 2016-09
 * BlackJackGame
 **/
namespace Model.Commands
{
    public class GameCommands
    {
        public GameCommands()
        { }
        /// <summary>
        /// Return status for dealer
        /// </summary>
        /// <param name="dealerCards"></param>
        /// <returns></returns>
        public GameStatus GetDealerStatus(List<Card> dealerCards)
        {
            return GetStatus(dealerCards);
        }
        /// <summary>
        /// return status for players
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        public ObservableCollection<Player> GetPlayersStatuses(ObservableCollection<Player> players)
        {
            foreach (var player in players)
            {
                player.PlayerStatus = GetStatus(player.PlayerCards.ToList());
            }
            return players;
        }
        /// <summary>
        /// Get status
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public GameStatus GetStatus(List<Card> cards)
        {
            GameStatus gameStatus = new GameStatus();
            gameStatus.Blackjack = IsBlackjack(cards);
            gameStatus.HandValue = GetHandValue(cards);
            gameStatus.Bust = IsBust(gameStatus.HandValue);

            if (gameStatus.Blackjack)
            {
                gameStatus.Status = "Blackjack";
            }
            if (gameStatus.Bust)
            {
                gameStatus.Status = "Bust";
            }
            return gameStatus;
        }
        /// <summary>
        /// Check if bust
        /// </summary>
        /// <param name="handValue"></param>
        /// <returns></returns>
        public bool IsBust(int handValue)
        {
            if (handValue > 21)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Check if blackjack
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public bool IsBlackjack(List<Card> cards)
        {
            if (cards.Count == 2)
            {
                var aceCard = cards.Where(x => x.Face == EnumTypes.FaceType.Ace).Count();
                var valueTenCard = cards.Where(x => x.Face == EnumTypes.FaceType.Jack || x.Face == EnumTypes.FaceType.King || x.Face == EnumTypes.FaceType.Queen).Count();
                if (aceCard == 1 && valueTenCard == 1)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Get value for hand
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public int GetHandValue(List<Card> cards)
        {
            int handValue = 0;

            var aceCards = FindAceCards(cards);
            var faceCards = FindFaceCards(cards);
            var regularCards = FindRegularCards(cards);
            handValue = GetSumOfAllCards(aceCards, faceCards, regularCards);

            return handValue;
        }
        /// <summary>
        /// Get totalsum of cards
        /// </summary>
        /// <param name="aceCards"></param>
        /// <param name="faceCards"></param>
        /// <param name="regularCards"></param>
        /// <returns></returns>
        private int GetSumOfAllCards(List<Card> aceCards, List<Card> faceCards, List<Card> regularCards)
        {
            var sumOfFaceCards = faceCards.Sum(x => x.Value);
            var sumOfRegularCards = regularCards.Sum(x => x.Value);
            int aceValue = DecideIfAceIsHighOrLow(aceCards, sumOfFaceCards + sumOfRegularCards);

            return sumOfFaceCards + sumOfRegularCards + aceValue;
        }
        /// <summary>
        /// Decide if ace is 1 or 11
        /// </summary>
        /// <param name="aceCards"></param>
        /// <param name="sumOfNonAceCards"></param>
        /// <returns></returns>
        public int DecideIfAceIsHighOrLow(List<Card> aceCards, int sumOfNonAceCards)
        {
            int aceValue = 0;

            if (aceCards.Count == 1 && sumOfNonAceCards + 11 <= 21)
            {
                aceValue = 11;
            }
            else if (aceCards.Count == 1 && sumOfNonAceCards + 11 > 21)
            {
                aceValue = 1;
            }

            if (aceCards.Count == 2 && sumOfNonAceCards + 12 <= 21)
            {
                aceValue = 12;
            }
            else if (aceCards.Count == 2 && sumOfNonAceCards + 12 > 21)
            {
                aceValue = 2;
            }

            if (aceCards.Count == 3 && sumOfNonAceCards + 13 <= 21)
            {
                aceValue = 13;
            }
            else if (aceCards.Count == 3 && sumOfNonAceCards + 13 > 21)
            {
                aceValue = 3;
            }

            if (aceCards.Count == 4 && sumOfNonAceCards + 14 <= 21)
            {
                aceValue = 14;
            }
            else if (aceCards.Count == 4 && sumOfNonAceCards + 14 > 21)
            {
                aceValue = 4;
            }
            return aceValue;
        }
        /// <summary>
        /// Find cards 2 to 10
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        private List<Card> FindRegularCards(List<Card> cards)
        {
            var regularCards = cards.Where(x => x.Face == 0).ToList();
            return regularCards;
        }
        /// <summary>
        /// Find face cards
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public List<Card> FindFaceCards(List<Card> cards)
        {
            var faceCards = cards.Where(x => x.Face == EnumTypes.FaceType.Jack || x.Face == EnumTypes.FaceType.King || x.Face == EnumTypes.FaceType.Queen).ToList();
            faceCards = SetvalueTenToFaceCArds(faceCards);
            return faceCards;
        }
        /// <summary>
        /// Set the value 10 to face cards
        /// </summary>
        /// <param name="faceCards"></param>
        /// <returns></returns>
        private List<Card> SetvalueTenToFaceCArds(List<Card> faceCards)
        {
            foreach (var faceCard in faceCards)
            {
                faceCard.Value = 10;
            }
            return faceCards;
        }
        /// <summary>
        /// Find aces
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public List<Card> FindAceCards(List<Card> cards)
        {
            var aceCard = cards.Where(x => x.Face == EnumTypes.FaceType.Ace).ToList();
            return aceCard;
        }
    }
}
