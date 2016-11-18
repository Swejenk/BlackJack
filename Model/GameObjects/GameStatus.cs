using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * Jens Malm 
 * 2016-09
 * BlackJackGame
 **/
namespace Model.GameObjects
{
    public class GameStatus: INotifyPropertyChanged
    {
        private string _status;
        private int _handValue;

        public int HandValue { get { return _handValue; } set { _handValue = value; RaisePropertyChanged("HandValue"); } }
        public string Status { get { return _status; } set { _status = value; RaisePropertyChanged("Status"); } }
        public bool Blackjack { get; set; }
        public bool Bust { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
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
