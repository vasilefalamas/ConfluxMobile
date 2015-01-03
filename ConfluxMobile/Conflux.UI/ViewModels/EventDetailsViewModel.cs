using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Conflux.UI.ViewModels
{
    public class EventDetailsViewModel : INotifyPropertyChanged
    {
        private string id;

        private string title;

        private string description;

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get
            {
                return description;        
            }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public EventDetailsViewModel(string id, string title)
        {
            Id = id;
            Title = title;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
