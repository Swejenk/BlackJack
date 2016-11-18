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
        /// <summary>
        /// Get cards left in active deck from DealerCommands
        /// </summary>
        /// <returns></returns>
        public int CardsLeftInDeckAfterDeal()
        {
            return CardsLeftInActiveDeck();
        }
        /// <summary>
        /// Get hidden card from DealerCommands
        /// </summary>
        /// <returns></returns>
        public Card DealHidden()
        {
            return DealHiddenCard();
        }
        /// <summary>
        /// Initiate player shuffle from DealerCommands
        /// </summary>
        /// <returns></returns>
        public bool ManualShuffle()
        {
            ShuffleDecks();
            return true;
        }
        /// <summary>
        /// Check if dealers turn
        /// </summary>
        /// <param name="playersStatuses"></param>
        /// <returns></returns>
        public bool CheckIfDealersTurn(List<string> playersStatuses)
        {

            return playersStatuses.All(x => x == "Hold");
        }
        /// <summary>
        /// Check if end of turn
        /// </summary>
        /// <param name="players"></param>
        /// <param name="dealerCards"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get card from DealerCommands
        /// </summary>
        /// <returns></returns>
        public Card Deal()
        {
            return DealCard();
        }
        /// <summary>
        /// Check if dealer should hold or get new card
        /// </summary>
        /// <param name="handValue"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get status of players by end of turn
        /// </summary>
        /// <param name="players"></param>
        /// <param name="dealerStatus"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Check if player won or lost to dealer
        /// </summary>
        /// <param name="playerStatus"></param>
        /// <param name="dealerStatus"></param>
        /// <returns></returns>
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
