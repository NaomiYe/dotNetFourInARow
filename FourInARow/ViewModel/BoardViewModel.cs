using FourInARow.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FourInARow.ViewModel
{
    public class BoardViewModel : ViewModelBase
    {
        // Those values are hard coded and so is the creation of the board, but I think a better solution is to create a class of settings
        // and let the user choose the size of the board, or even determin levels, and the creation of the board should be dynamic by
        // those settings. Same is for colors.
        public int RowsNumber = 6;
        public int ColumnsNumber = 7;

        ObservableCollection<CoinPosition> _allBoardPositions = new ObservableCollection<CoinPosition>();
        public ObservableCollection<CoinPosition> AllBoardPositions
        {
            get
            {
                return _allBoardPositions;
            }
            set
            {
                _allBoardPositions = value;
                OnPropertyChanged("AllBoardPositions");
            }
        }

        public Dictionary<int, int> ColumnsRowPosition { get; set; }

        public List<int> winningCoinsList = new List<int>();

        public BoardViewModel()
        {
            InitBoard();
        }

        void InitBoard()
        {
            AllBoardPositions = new ObservableCollection<CoinPosition>();
            for (int i = 0; i < (RowsNumber * ColumnsNumber); i++)
            {
                AllBoardPositions.Add(new CoinPosition());
            }

            ColumnsRowPosition = new Dictionary<int, int>();
            ColumnsRowPosition.Add(0, 0);
            ColumnsRowPosition.Add(1, 0);
            ColumnsRowPosition.Add(2, 0);
            ColumnsRowPosition.Add(3, 0);
            ColumnsRowPosition.Add(4, 0);
            ColumnsRowPosition.Add(5, 0);
            ColumnsRowPosition.Add(6, 0);

            winningCoinsList = new List<int>();
        }

        public int PlayerMove(int _player, int _column)
        {
            if (((_column < 0) || (_column > 6)) || ((_player < 1) || (_player > 2)) || (ColumnsRowPosition[_column] > 5))
            {
                return -1;
            }
            else
            {
                int indexToChange = (ColumnsRowPosition[_column] * ColumnsNumber) + _column;
                AllBoardPositions[indexToChange].PlayerCoin = _player;
                ColumnsRowPosition[_column]++;
                return indexToChange;
            }
        }

        public bool IsPlayerWin(int _player, int _indexInBoard)
        {
            int rowIndex, colIndex;

            //Find row and column index
            rowIndex = _indexInBoard / ColumnsNumber;
            colIndex = _indexInBoard - (ColumnsNumber * rowIndex);

            return (IsRowOfFour(rowIndex, colIndex, _player) ||
                IsColumnOfFour(rowIndex, colIndex, _player) ||
                IsDiagonalLeftOfFour(rowIndex, colIndex, _player) ||
                IsDiagonalRightOfFour(rowIndex, colIndex, _player));
        }

        bool IsRowOfFour(int _rowIndex, int _colIndex, int _playerID)
        {
            winningCoinsList = new List<int>();
            int playerCoinsInARowCount = 1;
            winningCoinsList.Add((_rowIndex * ColumnsNumber) + _colIndex);
            int tempColIndex;

            //Goto left positions
            tempColIndex = _colIndex - 1;
            while (tempColIndex > -1)
            {
                int indexToCheck = (_rowIndex * ColumnsNumber) + tempColIndex;
                if (AllBoardPositions[indexToCheck].PlayerCoin == _playerID)
                {
                    playerCoinsInARowCount++;
                    tempColIndex--;
                    winningCoinsList.Add(indexToCheck);
                }
                else
                {
                    break;
                }
            }

            //Goto right positions
            tempColIndex = _colIndex + 1;
            while (tempColIndex < ColumnsNumber)
            {
                int indexToCheck = (_rowIndex * ColumnsNumber) + tempColIndex;
                if (AllBoardPositions[indexToCheck].PlayerCoin == _playerID)
                {
                    playerCoinsInARowCount++;
                    tempColIndex++;
                    winningCoinsList.Add(indexToCheck);
                }
                else
                {
                    break;
                }
            }

            if (playerCoinsInARowCount < 4)
            {
                winningCoinsList.Clear();
                return false;
            }
            return true;
        }

        bool IsColumnOfFour(int _rowIndex, int _colIndex, int _playerID)
        {
            winningCoinsList = new List<int>();
            int playerCoinsInARowCount = 1;
            winningCoinsList.Add((_rowIndex * ColumnsNumber) + _colIndex);
            int tempRowIndex;

            //Goto down positions
            tempRowIndex = _rowIndex - 1;
            while (tempRowIndex > -1)
            {
                int indexToCheck = (tempRowIndex * ColumnsNumber) + _colIndex;
                if (AllBoardPositions[indexToCheck].PlayerCoin == _playerID)
                {
                    playerCoinsInARowCount++;
                    tempRowIndex--;
                    winningCoinsList.Add(indexToCheck);
                }
                else
                {
                    break;
                }
            }

            //Goto up positions
            tempRowIndex = _rowIndex + 1;
            while (tempRowIndex < RowsNumber)
            {
                int indexToCheck = (tempRowIndex * ColumnsNumber) + _colIndex;
                if (AllBoardPositions[indexToCheck].PlayerCoin == _playerID)
                {
                    playerCoinsInARowCount++;
                    tempRowIndex++;
                    winningCoinsList.Add(indexToCheck);
                }
                else
                {
                    break;
                }
            }

            if (playerCoinsInARowCount < 4)
            {
                winningCoinsList.Clear();
                return false;
            }
            return true;
        }

        bool IsDiagonalLeftOfFour(int _rowIndex, int _colIndex, int _playerID) //from up right to down left
        {
            winningCoinsList = new List<int>();
            int playerCoinsInARowCount = 1;
            winningCoinsList.Add((_rowIndex * ColumnsNumber) + _colIndex);
            int tempRowIndex, tempColIndex;

            //Goto down left positions
            tempRowIndex = _rowIndex - 1;
            tempColIndex = _colIndex - 1;
            while ((tempRowIndex > -1) && (tempColIndex > -1))
            {
                int indexToCheck = (tempRowIndex * ColumnsNumber) + tempColIndex;
                if (AllBoardPositions[indexToCheck].PlayerCoin == _playerID)
                {
                    playerCoinsInARowCount++;
                    tempRowIndex--;
                    tempColIndex--;
                    winningCoinsList.Add(indexToCheck);
                }
                else
                {
                    break;
                }
            }

            //Goto up right positions
            tempRowIndex = _rowIndex + 1;
            tempColIndex = _colIndex + 1;
            while ((tempRowIndex < RowsNumber) && (tempColIndex < ColumnsNumber))
            {
                int indexToCheck = (tempRowIndex * ColumnsNumber) + tempColIndex;
                if (AllBoardPositions[indexToCheck].PlayerCoin == _playerID)
                {
                    playerCoinsInARowCount++;
                    tempRowIndex++;
                    tempColIndex++;
                    winningCoinsList.Add(indexToCheck);
                }
                else
                {
                    break;
                }
            }

            if (playerCoinsInARowCount < 4)
            {
                winningCoinsList.Clear();
                return false;
            }
            return true;
        }

        bool IsDiagonalRightOfFour(int _rowIndex, int _colIndex, int _playerID) //from up left to down right
        {
            winningCoinsList = new List<int>();
            int playerCoinsInARowCount = 1;
            winningCoinsList.Add((_rowIndex * ColumnsNumber) + _colIndex);
            int tempRowIndex, tempColIndex;

            //Goto down right positions
            tempRowIndex = _rowIndex - 1;
            tempColIndex = _colIndex + 1;
            while ((tempRowIndex > -1) && (tempColIndex < ColumnsNumber))
            {
                int indexToCheck = (tempRowIndex * ColumnsNumber) + tempColIndex;
                if (AllBoardPositions[indexToCheck].PlayerCoin == _playerID)
                {
                    playerCoinsInARowCount++;
                    tempRowIndex--;
                    tempColIndex++;
                    winningCoinsList.Add(indexToCheck);
                }
                else
                {
                    break;
                }
            }

            //Goto up left positions
            tempRowIndex = _rowIndex + 1;
            tempColIndex = _colIndex - 1;
            while ((tempRowIndex < RowsNumber) && (tempColIndex > -1))
            {
                int indexToCheck = (tempRowIndex * ColumnsNumber) + tempColIndex;
                if (AllBoardPositions[indexToCheck].PlayerCoin == _playerID)
                {
                    playerCoinsInARowCount++;
                    tempRowIndex++;
                    tempColIndex--;
                    winningCoinsList.Add(indexToCheck);
                }
                else
                {
                    break;
                }
            }

            if (playerCoinsInARowCount < 4)
            {
                winningCoinsList.Clear();
                return false;
            }
            return true;
        }
    }
}
