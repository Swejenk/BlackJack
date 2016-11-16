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
        public void AddPlayer()
        {
            int playerNr = Players.Count() + 1;
            Player newPlayer = new Player() { Nr = playerNr,PlayerStatus = new GameStatus() {Status= "Joined" }, Name = "Player" + playerNr };
            Players.Add(newPlayer);
        }
        public void StartRound()
        {
            ClearCardsFromDealerAndPlayer();
            ResetPlayerStatus();
            ResetDealerStatus();
            RoundStartDealCards();
        }

        private void ResetPlayerStatus()
        {
            foreach (var player in Players)
            {
                player.PlayerStatus.Status = "Ready";
                player.PlayerStatus.HandValue = 0;
            }
        }
        private void ResetDealerStatus()
        {
            DealerStatus = new GameStatus();
            RaisePropertyChanged("DealerStatus");
        }
        private void ClearCardsFromDealerAndPlayer()
        {
            DealerCards.Clear();
            foreach (var player in Players)
            {
                player.PlayerCards.Clear();
                player.PlayerStatus = new GameStatus();
            }

        }

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

        public void PlayerNameChanged(string playerName, int playerNr)
        {
            var player = Players.Where(x => x.Nr == playerNr).FirstOrDefault();
            player.Name = playerName;
        }
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

        private void FinishTurn()
        {
            DealerStatus = _gameCommands.GetDealerStatus(DealerCards.ToList());
            Players = _gameCommands.GetPlayersStatuses(Players);
            Players = _turnCommands.GetEndTurnStatusForPlayers(Players, DealerStatus);
            RaisePropertyChanged("DealerStatus");
            RoundStarted = false;
        }

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
