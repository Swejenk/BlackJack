using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Commands;
using Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTest.CommandsTest
{
    [TestClass()]
    public class TurnCommandsTests
    {
        TurnCommands _turnCommands = new TurnCommands();
   
        ObservableCollection<Player> _players;
        Player _player;
        GameStatus _dealerStatus;

        public void Inititalize()
        {
            _players = new ObservableCollection<Player>();
            _player =  new Player();
            var player = new Player()
            {
                Name = "Player1",
                Nr = 1,
               PlayerStatus = new GameStatus() { Status = "Hold"}
              
            };
            _players.Add(player);

            _dealerStatus = new GameStatus();
        }

        [TestMethod()]
        public void PlayerStatusShouldBeBlackJack()
        {
            Inititalize();
            _players[0].PlayerStatus.Blackjack = true;
            _players[0].PlayerStatus.Bust = false;
            _players[0].PlayerStatus.Status = "Blackjack";
            _players[0].PlayerStatus.HandValue = 21;

            _dealerStatus.Blackjack = false;
            _dealerStatus.Bust = false;
            _dealerStatus.HandValue = 19;

            var statusText = "Blackjack";
            var boolBlackJack = true;

            var result = _turnCommands.GetEndTurnStatusForPlayers(_players, _dealerStatus);

            Assert.AreEqual(statusText, result[0].PlayerStatus.Status);
            Assert.AreEqual(boolBlackJack, result[0].PlayerStatus.Blackjack);
        }

        [TestMethod()]
        public void PlayerStatusShouldBeTie()
        {
            Inititalize();

            _players[0].PlayerStatus.Blackjack = false;
            _players[0].PlayerStatus.Bust = false;
            _players[0].PlayerStatus.Status = "";
            _players[0].PlayerStatus.HandValue = 19;

            _dealerStatus.Blackjack = false;
            _dealerStatus.Bust = false;
            _dealerStatus.HandValue = 19;

            var expected = "Tie!";
            var result = _turnCommands.GetEndTurnStatusForPlayers(_players, _dealerStatus).FirstOrDefault().PlayerStatus.Status;

            Assert.AreEqual(expected, result);
     
        }
        [TestMethod()]
        public void DealerBustPlayerShouldHaveStatusWon()
        {
            Inititalize();

            _players[0].PlayerStatus.Blackjack = false;
            _players[0].PlayerStatus.Bust = false;
            _players[0].PlayerStatus.Status = "";
            _players[0].PlayerStatus.HandValue = 19;

            _dealerStatus.Blackjack = false;
            _dealerStatus.Bust = true;
            _dealerStatus.HandValue = 25;

            var expected = "Won!";
            var result = _turnCommands.GetEndTurnStatusForPlayers(_players, _dealerStatus).FirstOrDefault().PlayerStatus.Status;

            Assert.AreEqual(expected, result);

        }



    }
}