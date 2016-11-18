using Model.GameObjects;
using System.Collections.Generic;
using System.Linq;
using System;
/**
 * Jens Malm 
 * 2016-09
 * BlackJackGame
 **/
namespace Model.Commands
{
    public class PlayerCommands
    {
        private List<Player> players;

        public PlayerCommands()
        {
            players = new List<Player>();
        }
        /// <summary>
        /// Check if player exist
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private bool PlayerNameExists(Player player)
        {
                var result = players.Where(x => x.Name == player.Name);
                if (result != null)
                {
                    return true;
                }
            return false;
        }
        /// <summary>
        /// Add new player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool AddNewPlayer(Player player)
        {
            if(!PlayerNameExists(player))
            { 
            players.Add(player);
                return true;
            }

            return false;
        }

      
      
    }
}
