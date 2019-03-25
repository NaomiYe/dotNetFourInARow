using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Model
{
    public class PlayersData : ModelBase
    {
        string _player1Name = "Player 1";
        public string Player1Name
        {
            get
            {
                return _player1Name;
            }
            set
            {
                _player1Name = value;
                OnPropertyChanged("Player1Name");
            }
        }

        string _player2Name = "Player 2";
        public string Player2Name
        {
            get
            {
                return _player2Name;
            }
            set
            {
                _player2Name = value;
                OnPropertyChanged("Player2Name");
            }
        }

        int _player1Victories = 0;
        public int Player1Victories
        {
            get
            {
                return _player1Victories;
            }
            set
            {
                _player1Victories = value;
                OnPropertyChanged("Player1Victories");
            }
        }

        int _player2Victories = 0;
        public int Player2Victories
        {
            get
            {
                return _player2Victories;
            }
            set
            {
                _player2Victories = value;
                OnPropertyChanged("Player2Victories");
            }
        }

        bool _player2TypeHuman = true;
        public bool Player2TypeHuman
        {
            get
            {
                return _player2TypeHuman;
            }
            set
            {
                _player2TypeHuman = value;

                if (_player2TypeHuman)
                {
                    Player2Type = "Human";
                }
                else
                {
                    Player2Type = "Computer";
                }

                OnPropertyChanged("Player2TypeHuman");
            }
        }

        string _player2Type = "Human";
        public string Player2Type
        {
            get
            {
                return _player2Type;
            }
            set
            {
                _player2Type = value;
                OnPropertyChanged("Player2Type");
            }
        }
    }
}
