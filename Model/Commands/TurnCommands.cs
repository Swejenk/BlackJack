using Model.GameObjects;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System;
/**
* Jens Malm 
* 2016-09
* BlackJackGame
**/
namespace Model.Commands
{
    public class TurnCommands : DealerCommands
    {

        public TurnCommands()
        {
        }

        public int CardsLeftInDeckAfterDeal()
        {
            return CardsLeftInActiveDeck();
        }

        public Card DealHidden()
        {
            return DealHiddenCard();
        }

        public bool ManualShuffle()
        {
            ShuffleDecks();
            return true;
        }

        public bool CheckIfDealersTurn(List<string> playersStatuses)
        {

            return playersStatuses.All(x => x == "Hold");
        }
        public bool EndTurn(List<Player> players, List<Card> dealerCards)
        {
            var playersReady = players.Where(x => x.PlayerStatus.Status == "Hold");
            if (playersReady.Count() == players.Count())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Card Deal()
        {
            return DealCard();
        }

        public bool DealerDecision(int handValue)
        {

            if (handValue <= 17)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ObservableCollection<Player> GetEndTurnStatusForPlayers(ObservableCollection<Player> players, GameStatus dealerStatus)
        {
            if (dealerStatus.Blackjack)
            {
                foreach (var player in players)
                {
                    player.PlayerStatus.Status = "Lost";
                }
            }
            else
            {
                foreach (var player in players)
                {
                    if (!player.PlayerStatus.Blackjack)
                    {
                        player.PlayerStatus.Status = GetIfPlayerWonByScore(player.PlayerStatus, dealerStatus);
                    }
                }
            }
            return players;
        }

        private string GetIfPlayerWonByScore(GameStatus playerStatus, GameStatus dealerStatus)
        {
            if (dealerStatus.Bust && playerStatus.HandValue <= 21)
            {
                return "Won!";
            }
            if (dealerStatus.HandValue < playerStatus.HandValue && !playerStatus.Bust)
            {
                return "Won!";
            }
            if (dealerStatus.HandValue == playerStatus.HandValue && !playerStatus.Bust)
            {
                return "Tie!";
            }

            return "Lost";
        }
    }
}
