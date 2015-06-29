using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Conflux.Core.Models;

namespace Conflux.UI.ViewModels
{
    public class MyEventsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EventDisplayItem> myEvents;

        public ObservableCollection<EventDisplayItem> MyEvents
        {
            get
            {
                return myEvents;
            }
            set
            {
                myEvents = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MyEventsViewModel()
        {
            MyEvents = new ObservableCollection<EventDisplayItem>();
        }

        public async Task GetMyEvents()
        {
            var events = await App.FacebookClient.GetMyEventsAsync();

            foreach (var item in events)
            {
                MyEvents.Add(new EventDisplayItem
                {
                    Event = item
                });
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
