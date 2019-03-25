using FourInARow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.ViewModel
{
    class PlayersDataViewModel : ViewModelBase
    {
        PlayersData _localPlayersData = new PlayersData();
        public PlayersData LocalPlayersData
        {
            get
            {
                return _localPlayersData;
            }
            set
            {
                _localPlayersData = value;
                OnPropertyChanged("LocalPlayersData");
            }
        }

        //Another solution for passing the PlayersData, is to have the PlayersData as a singleton class.
        public PlayersDataViewModel(PlayersData playersData)
        {
            LocalPlayersData.Player1Name = playersData.Player1Name;
            LocalPlayersData.Player2Name = playersData.Player2Name;
        }

        //*************************************** C O M M A N D S ****************************************************
        // Override method for returning the needed commands from buttons in the application
        protected override Dictionary<string, RelayCommand> CreateCommands()
        {
            return new Dictionary<string, RelayCommand>
            {
                { "SavePlayersData", new RelayCommand(param => this.SavePlayersData(), param => this.SavePlayersDataCanExecute()) },
                { "CancelSavePlayersData", new RelayCommand(param => this.CancelSavePlayersData()) }
            };
        }

        void SavePlayersData()
        {
            // No special conditions for now
        }

        public bool SavePlayersDataCanExecute()
        {
            if ((LocalPlayersData.Player1Name.Trim().Equals("")) || 
                (LocalPlayersData.Player2Name.Trim().Equals("")) || 
                (LocalPlayersData.Player1Name.Equals(LocalPlayersData.Player2Name)))
                return false;
            return true;
        }

        void CancelSavePlayersData()
        {
            // No special conditions for now
        }
    }
}
