using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;
using Conflux.Core.Models;

namespace Conflux.UI.ViewModels
{
    public class MyEventsViewModel : INotifyPropertyChanged
    {
        private readonly IFacebookProvider facebookProvider;

        private readonly AccessToken accessToken;

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

        public MyEventsViewModel(IFacebookProvider facebookProvider, AccessToken accessToken)
        {
            this.facebookProvider = facebookProvider;
            this.accessToken = accessToken;

            MyEvents = new ObservableCollection<EventDisplayItem>();
        }

        public async Task GetMyEvents()
        {
            var events = await facebookProvider.GetMyEvents(accessToken);

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
