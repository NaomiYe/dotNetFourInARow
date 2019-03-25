using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FourInARow.Model
{
    public class CoinPosition : ModelBase
    {
        int _playerCoin = 0;
        public int PlayerCoin
        {
            get
            {
                return _playerCoin;
            }
            set
            {
                _playerCoin = value;

                switch(_playerCoin)
                {
                    //If the value is not 1 or 2 than it is white anyway, but the 0 and default here, is for future use of
                    //having a warning of unexpected value somewhere, to write to a log file for example.
                    case 0:
                        CoinColor = Brushes.White;
                        break;

                    case 1:
                        CoinColor = Brushes.Salmon;
                        break;

                    case 2:
                        CoinColor = Brushes.Gold;
                        break;

                    default:
                        CoinColor = Brushes.White;
                        break;
                }

                OnPropertyChanged("PlayerCoin");
            }
        }

        Brush _coinColor = Brushes.White;
        public Brush CoinColor
        {
            get
            {
                return _coinColor;
            }
            set
            {
                _coinColor = value;
                OnPropertyChanged("CoinColor");
            }
        }
    }
}
