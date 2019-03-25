using FourInARow.Model;
using FourInARow.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FourInARow.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        BoardViewModel GameBoard = new BoardViewModel();

        ObservableCollection<Brush> _boardPositionsColors;
        public ObservableCollection<Brush> BoardPositionsColors
        {
            get
            {
                return _boardPositionsColors;
            }
            set
            {
                _boardPositionsColors = value;
                OnPropertyChanged("BoardPositionsColors");
            }
        }

        public PlayersData PlayersData { get; set; }
        int playerID = 0;
        bool isWinner = false;

        string _statusMsg = "";
        public string StatusMsg
        {
            get
            {
                return _statusMsg;
            }
            set
            {
                _statusMsg = value;
                OnPropertyChanged("StatusMsg");
            }
        }

        public MainWindowViewModel()
        {
            PlayersData = new PlayersData();
            OnPropertyChanged("PlayersData");

            InitNewGame();
        }

        void InitNewGame()
        {
            GameBoard = new BoardViewModel();

            BoardPositionsColors = new ObservableCollection<Brush>();
            foreach (var item in GameBoard.AllBoardPositions)
            {
                BoardPositionsColors.Add(item.CoinColor);
            }

            playerID = 1;
            StatusMsg = "Player's turn:\n" + PlayersData.Player1Name;
            isWinner = false;
        }

        void SwitchPlayer()
        {
            playerID = (playerID == 1) ? 2 : 1;

            //Another solution for those messages can be a class managing them.
            StatusMsg = (playerID == 1) ? "Player's turn:\n" + PlayersData.Player1Name : "Player's turn:\n" + PlayersData.Player2Name;
        }

        int PlayTheMove(int ColumnNumber)
        {
            int positionMove = GameBoard.PlayerMove(playerID, ColumnNumber);
            if (positionMove == -1)
            {
                MessageBox.Show("Invalid move, please try again.");
            }
            else
            {
                BoardPositionsColors[positionMove] = GameBoard.AllBoardPositions[positionMove].CoinColor;

                if (GameBoard.IsPlayerWin(playerID, positionMove))
                {
                    foreach (int item in GameBoard.winningCoinsList)
                    {
                        if (playerID == 1)
                        {
                            BoardPositionsColors[item] = Brushes.Firebrick;
                            StatusMsg = PlayersData.Player1Name + " wins!";
                        }
                        else
                        {
                            BoardPositionsColors[item] = Brushes.DarkGoldenrod;
                            StatusMsg = PlayersData.Player2Name + " wins!";
                        }
                    }
                    isWinner = true;

                    if (playerID == 1)
                    {
                        PlayersData.Player1Victories++;
                        OnPropertyChanged("PlayersData");
                    }
                    else
                    {
                        PlayersData.Player2Victories++;
                        OnPropertyChanged("PlayersData");
                    }
                    playerID = 0;
                }
                else
                {
                    SwitchPlayer();
                }
            }

            return positionMove;
        }

        //*************************************** C O M M A N D S ****************************************************
        // Override method for returning the needed commands from buttons in the application
        protected override Dictionary<string, RelayCommand> CreateCommands()
        {
            return new Dictionary<string, RelayCommand>
            {
                { "StartNewGame", new RelayCommand(param => this.StartNewGame()) },
                { "SetPlayersData", new RelayCommand(param => this.SetPlayersData()) },
                { "CloseGame", new RelayCommand(param => this.CloseGame()) },
                { "ThrowCoin", new RelayCommand(param => this.ThrowCoin(int.Parse(param.ToString())), param => this.ThrowCoinCanExecute(int.Parse(param.ToString()))) }
            };
        }

        void ThrowCoin(int ColumnNumber)
        {
            int playedMove = PlayTheMove(ColumnNumber);
        }

        public bool ThrowCoinCanExecute(int ColumnNumber)
        {
            if ((GameBoard.ColumnsRowPosition[ColumnNumber] > 5) || (isWinner))
                return false;
            return true;
        }

        void StartNewGame()
        {
            InitNewGame();
        }

        void SetPlayersData()
        {
            PlayersDataView newWin = new PlayersDataView();
            //Another solution for passing the PlayersData, is to have the PlayersData as a singleton class.
            PlayersDataViewModel viewModel = new PlayersDataViewModel(PlayersData);
            newWin.DataContext = viewModel;
            newWin.ShowDialog();

            if (newWin.DialogResult == true)
            {
                PlayersData.Player1Name = viewModel.LocalPlayersData.Player1Name;
                PlayersData.Player2Name = viewModel.LocalPlayersData.Player2Name;
            }

            StatusMsg = (playerID == 1) ? "Player's turn:\n" + PlayersData.Player1Name : "Player's turn:\n" + PlayersData.Player2Name;

            OnPropertyChanged("PlayersData");
        }

        void CloseGame()
        {
            Application.Current.Shutdown();
        }
    }
}
