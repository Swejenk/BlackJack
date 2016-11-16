﻿using Model.GameObjects;
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
        private bool PlayerNameExists(Player player)
        {
                var result = players.Where(x => x.Name == player.Name);
                if (result != null)
                {
                    return true;
                }
            return false;
        }
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
