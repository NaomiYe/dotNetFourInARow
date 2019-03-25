using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region PropertyChange

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string _sPropertyChanged) // Event to refresh ViewModel with every change
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_sPropertyChanged));
        }

        #endregion PropertyChange

        #region Commands

        // Virtual method for returning the needed commands from buttons in the application
        protected virtual Dictionary<string, RelayCommand> CreateCommands()
        {
            return new Dictionary<string, RelayCommand>();
        }

        //Dictionary that contains all the commands from buttons in the application
        Dictionary<string, RelayCommand> m_commands;
        public Dictionary<string, RelayCommand> Commands
        {
            get
            {
                if (m_commands == null)
                {
                    Dictionary<string, RelayCommand> cmds = this.CreateCommands();
                    m_commands = new Dictionary<string, RelayCommand>(cmds);
                }
                return m_commands;
            }
        }

        #endregion Commands
    }
}
