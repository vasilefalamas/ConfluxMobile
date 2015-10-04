using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Conflux.UI.Models;

namespace Conflux.UI.ViewModels
{
    public class EventCalendarViewModel : INotifyPropertyChanged
    {
        private List<Week> weeks;

        public List<Week> Weeks
        {
            get
            {
                return weeks;
            }
            set
            {
                weeks = value;
                OnPropertyChanged();
            }
        }

        public EventCalendarViewModel()
        {
            Weeks = new List<Week>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
