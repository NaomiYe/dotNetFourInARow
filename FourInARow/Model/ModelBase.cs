using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Model
{
    public class ModelBase
    {
        #region PropertyChange

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string _sPropertyChanged) // Event to refresh properties with every change
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_sPropertyChanged));
        }

        #endregion PropertyChange
    }
}
