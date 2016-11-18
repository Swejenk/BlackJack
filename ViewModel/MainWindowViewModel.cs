using Model.Commands;
using Model.GameObjects;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System;
/**
* Jens Malm 
* 2016-09
* BlackJackGame
**/
namespace ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private TurnCommands _turnCommands;
        private GameCommands _gameCommands;
        
        public ObservableCollection<Card> DealerCards { get; set; }
        public bool RoundStarted { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Player> Players { get; set; }
        public GameStatus DealerStatus { get; set; }
        public int CardsLeftInDeckCounter { get { return _turnCommands.CardsLeftInDeckAfterDeal(); } }

        public MainWindowViewModel()
        {
            Players = new ObservableCollection<Player>();
            DealerCards = new ObservableCollection<Card>();
            DealerStatus = new GameStatus();
            _turnCommands = new TurnCommands();
            _gameCommands = new GameCommands();
        }
        /// <summary>
        /// Check if table is full
        /// </summary>
        /// <returns></returns>
        public bool TableIsFull()
        {
            if (Players.Count == 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Add new player
        /// </summary>
        public void AddPlayer()
        {
            int playerNr = Players.Count() + 1;
            Player newPlayer = new Player() { Nr = playerNr,PlayerStatus = new GameStatus() {Status= "Joined" }, Name = "Player" + playerNr };
            Players.Add(newPlayer);
        }
        /// <summary>
        /// Start round
        /// </summary>
        public void StartRound()
        {
            ClearCardsFromDealerAndPlayer();
            ResetPlayerStatus();
            ResetDealerStatus();
            RoundStartDealCards();
        }
        /// <summary>
        /// Reset players status by start of new round
        /// </summary>
        private void ResetPlayerStatus()
        {
            foreach (var player in Players)
            {
                player.PlayerStatus.Status = "Ready";
                player.PlayerStatus.HandValue = 0;
            }
        }
        /// <summary>
        /// Reset dealer status by start of new round
        /// </summary>
        private void ResetDealerStatus()
        {
            DealerStatus = new GameStatus();
            RaisePropertyChanged("DealerStatus");
        }
        /// <summary>
        /// Clear all cards from dealer and player by start of new round
        /// </summary>
        private void ClearCardsFromDealerAndPlayer()
        {
            DealerCards.Clear();
            foreach (var player in Players)
            {
                player.PlayerCards.Clear();
                player.PlayerStatus = new GameStatus();
            }

        }
        /// <summary>
        /// Deal card by start of new round
        /// </summary>
        private void RoundStartDealCards()
        {
            foreach (var player in Players)
            {
                player.PlayerCards.Clear();
                player.PlayerCards.Add(_turnCommands.Deal());
                player.PlayerCards.Add(_turnCommands.Deal());              
                player.PlayerStatus.Status = "Ready";
              
            }

            DealerCards.Add(_turnCommands.Deal());
            DealerCards.Add(_turnCommands.DealHidden());            
            RaisePropertyChanged("CardsLeftInDeckCounter");
            RoundStarted = true;
        }
        /// <summary>
        /// Player has changed name
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="playerNr"></param>
        public void PlayerNameChanged(string playerName, int playerNr)
        {
            var player = Players.Where(x => x.Nr == playerNr).FirstOrDefault();
            player.Name = playerName;
        }
        /// <summary>
        /// Player clicks deal
        /// </summary>
        /// <param name="playerNr"></param>
        public void PlayerActionDeal(int playerNr)
        {
            if (RoundStarted)
            {
                var player = Players.Where(x => x.Nr == playerNr).FirstOrDefault();
                if (player != null)
                {
                    player.PlayerCards.Add(_turnCommands.Deal());
                    RaisePropertyChanged("CardsLeftInDeckCounter");
                    player.PlayerStatus.Status = "Thinking";
                }
            }

        }
        /// <summary>
        /// Player clicks hold
        /// </summary>
        /// <param name="playerNr"></param>
        public void PlayerActionHold(int playerNr)
        {
            if (RoundStarted)
            {
                var player = Players.Where(x => x.Nr == playerNr).FirstOrDefault();
                if (player != null)
                {
                    player.PlayerStatus.Status = "Hold";
                    DealerAction();
                }
            }
        }
        /// <summary>
        /// Check if dealer should get cards or end turn
        /// </summary>
        private void DealerAction()
        {
            if (_turnCommands.CheckIfDealersTurn(Players.Select(x=>x.PlayerStatus.Status).ToList()))
            {
                DealerTurnAction();
            }

            if (_turnCommands.EndTurn(Players.ToList(), DealerCards.ToList()))
            {
                FinishTurn();
            }
        }
        /// <summary>
        /// Get status for dealer and player by end of turn
        /// </summary>
        private void FinishTurn()
        {
            DealerStatus = _gameCommands.GetDealerStatus(DealerCards.ToList());
            Players = _gameCommands.GetPlayersStatuses(Players);
            Players = _turnCommands.GetEndTurnStatusForPlayers(Players, DealerStatus);
            RaisePropertyChanged("DealerStatus");
            RoundStarted = false;
        }
        /// <summary>
        /// Dealer reveal card and deals new card if handvalue is lower than 17
        /// </summary>
        private void DealerTurnAction()
        {
            var dealerHasHiddenCard = DealerCards.Any(x => x.HideCard == true);
            if (dealerHasHiddenCard)
            {
                RevealHiddenDealerCard();
            }
            while (_turnCommands.DealerDecision(_gameCommands.GetHandValue(DealerCards.ToList())))
            {
                DealerCards.Add(_turnCommands.Deal());
            }

            RaisePropertyChanged("CardsLeftInDeckCounter");
        }
        /// <summary>
        /// Set hidden card to true
        /// </summary>
        private void RevealHiddenDealerCard()
        {
            var hiddenCard = DealerCards.Where(x => x.HideCard == true).ToList();
            foreach (var card in hiddenCard)
            {
                DealerCards.Remove(card);
                card.HideCard = false;
                DealerCards.Add(card);
            }
        }
        /// <summary>
        /// Player clicks shuffle
        /// </summary>
        /// <returns></returns>
        public bool InitiatePlayerShuffle()
        {
            return _turnCommands.ManualShuffle();
        }

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
