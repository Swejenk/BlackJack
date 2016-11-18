using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;
/**
 * Jens Malm 
 * 2016-09
 * BlackJackGame
 **/
namespace BlackJackGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _mainWindowViewModel = (MainWindowViewModel)base.DataContext;
        }
        /// <summary>
        /// Start round click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStartRound_Click(object sender, RoutedEventArgs e)
        {
            _mainWindowViewModel.StartRound();
            BtnStartRound.Visibility = Visibility.Hidden;

            BtnNewPlayerJoin.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// new player join click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNewPlayerJoin_Click(object sender, RoutedEventArgs e)
        {
            _mainWindowViewModel.AddPlayer();
            if (BtnStartRound.IsEnabled == false)
            {
                BtnStartRound.Content = "Start round";
                BtnStartRound.IsEnabled = true;
            }
            if (_mainWindowViewModel.TableIsFull())
            {
                BtnNewPlayerJoin.Visibility = Visibility.Hidden;              
            }

        }
        /// <summary>
        /// save name click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveName_Click(object sender, RoutedEventArgs e)
        {
            int PlayerNr = int.Parse(((Button)sender).Tag.ToString());
            TextBox txtName = (TextBox)FindName("Player" + PlayerNr + "TxtName");
            Button BtnSaveName = (Button)FindName("Player" + PlayerNr + "BtnSaveName");
            Label LblName = (Label)FindName("Player" + PlayerNr + "LblName");

            _mainWindowViewModel.PlayerNameChanged(txtName.Text, PlayerNr);

            txtName.Visibility = Visibility.Hidden;
            BtnSaveName.Visibility = Visibility.Hidden;
            LblName.Content = txtName.Text;
            LblName.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Deal cards click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeal_Click(object sender, RoutedEventArgs e)
        {
            int PlayerNr = int.Parse(((Button)sender).Tag.ToString());
            _mainWindowViewModel.PlayerActionDeal(PlayerNr);
            if (!_mainWindowViewModel.RoundStarted)
            {
                BtnStartRound.Visibility = Visibility.Visible;
                BtnNewPlayerJoin.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// hold click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHold_Click(object sender, RoutedEventArgs e)
        {
            int PlayerNr = int.Parse(((Button)sender).Tag.ToString());
            _mainWindowViewModel.PlayerActionHold(PlayerNr);
            if (!_mainWindowViewModel.RoundStarted)
            {
                BtnStartRound.Visibility = Visibility.Visible;
                if (!_mainWindowViewModel.TableIsFull())
                {
                    BtnNewPlayerJoin.Visibility = Visibility.Visible;
                }
            }
        }
        /// <summary>
        /// Shuffle deck click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShuffleDeck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_mainWindowViewModel.InitiatePlayerShuffle() && LblShuffleInfo.Visibility == Visibility.Hidden)
                {
                    LblShuffleInfo.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            { }



        }
    }
}
