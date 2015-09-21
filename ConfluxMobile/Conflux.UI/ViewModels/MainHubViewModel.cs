using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media.Imaging;
using Conflux.Core.Models;
using Conflux.UI.Common;

namespace Conflux.UI.ViewModels
{
    public class MainHubViewModel : INotifyPropertyChanged
    {
        private string name;

        private string location;

        private BitmapImage profilePicture;

        private IncrementalLoadingCollection<EventDisplayItem> newestEvents;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage ProfilePicture
        {
            get
            {
                return profilePicture;
            }
            set
            {
                profilePicture = value;
                OnPropertyChanged();
            }
        }

        public IncrementalLoadingCollection<EventDisplayItem> NewestEvents
        {
            get
            {
                return newestEvents;
            }
            set
            {
                newestEvents = value;
                OnPropertyChanged();
            }
        }
        
        public MainHubViewModel(string location)
        {
            Location = location;

            var eventsSource = new NewestEventsSource(App.FacebookClient, location);

            NewestEvents = new IncrementalLoadingCollection<EventDisplayItem>(eventsSource);
            NewestEvents.LoadMoreItemsStarted += OnLoadMoreItemsStarted;
            NewestEvents.LoadMoreItemsCompleted += OnLoadMoreItemsCompleted;
        }
        
        private async void OnLoadMoreItemsStarted()
        {
            await StatusBarHandler.ShowMessageAsync(string.Format("Searching events in {0}...", Location), true);
        }
        
        private async void OnLoadMoreItemsCompleted()
        {
            await StatusBarHandler.HideAsync();
        }

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
