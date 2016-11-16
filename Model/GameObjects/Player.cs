using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
/**
 * Jens Malm 
 * 2016-09
 * BlackJackGame
 **/
namespace Model.GameObjects
{
    public class Player: INotifyPropertyChanged
    {
        private int _nr;
        private string _name;
        private GameStatus _gameStatus;

        public int Nr { get { return _nr; } set { _nr = value; RaisePropertyChanged("Nr"); } }
        public string Name { get { return _name; } set { _name = value; RaisePropertyChanged("Name"); } }
        
        public GameStatus PlayerStatus { get { return _gameStatus; } set { _gameStatus = value; RaisePropertyChanged("PlayerStatus"); } }
    

        public ObservableCollection<Card> PlayerCards { get; set; }

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
        public Player()
        {
            PlayerCards = new ObservableCollection<Card>();
            PlayerStatus = new GameStatus();
        }
    }
}
