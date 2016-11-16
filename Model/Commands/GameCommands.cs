using Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Commands
{
    public class GameCommands
    {
        public GameCommands()
        { }

        public GameStatus GetDealerStatus(List<Card> dealerCards)
        {
            return GetStatus(dealerCards);
        }
        public ObservableCollection<Player> GetPlayersStatuses(ObservableCollection<Player> players)
        {
            foreach (var player in players)
            {
                player.PlayerStatus = GetStatus(player.PlayerCards.ToList());
            }
            return players;
        }

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

        public bool IsBust(int handValue)
        {
            if (handValue > 21)
            {
                return true;
            }
            return false;
        }
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

        public int GetHandValue(List<Card> cards)
        {
            int handValue = 0;

            var aceCards = FindAceCards(cards);
            var faceCards = FindFaceCards(cards);
            var regularCards = FindRegularCards(cards);
            handValue = GetSumOfAllCards(aceCards, faceCards, regularCards);

            return handValue;
        }

        private int GetSumOfAllCards(List<Card> aceCards, List<Card> faceCards, List<Card> regularCards)
        {
            var sumOfFaceCards = faceCards.Sum(x => x.Value);
            var sumOfRegularCards = regularCards.Sum(x => x.Value);
            int aceValue = DecideIfAceIsHighOrLow(aceCards, sumOfFaceCards + sumOfRegularCards);

            return sumOfFaceCards + sumOfRegularCards + aceValue;
        }

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
        private List<Card> FindRegularCards(List<Card> cards)
        {
            var regularCards = cards.Where(x => x.Face == 0).ToList();
            return regularCards;
        }
        public List<Card> FindFaceCards(List<Card> cards)
        {
            var faceCards = cards.Where(x => x.Face == EnumTypes.FaceType.Jack || x.Face == EnumTypes.FaceType.King || x.Face == EnumTypes.FaceType.Queen).ToList();
            faceCards = SetvalueTenToFaceCArds(faceCards);
            return faceCards;
        }

        private List<Card> SetvalueTenToFaceCArds(List<Card> faceCards)
        {
            foreach (var faceCard in faceCards)
            {
                faceCard.Value = 10;
            }
            return faceCards;
        }

        public List<Card> FindAceCards(List<Card> cards)
        {
            var aceCard = cards.Where(x => x.Face == EnumTypes.FaceType.Ace).ToList();
            return aceCard;
        }
    }
}
