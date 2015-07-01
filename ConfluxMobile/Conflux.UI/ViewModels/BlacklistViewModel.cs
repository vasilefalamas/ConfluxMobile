using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Conflux.Core.Models;
using Conflux.Core.Settings;

namespace Conflux.UI.ViewModels
{
    public class BlacklistViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EventDisplayItem> events; 

        private bool isEmptyBlacklist;

        public bool IsEmptyBlacklist
        {
            get
            {
                return isEmptyBlacklist;
            }
            set
            {
                isEmptyBlacklist = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EventDisplayItem> Events { get; set; }

        public BlacklistViewModel()
        {
            Events = new ObservableCollection<EventDisplayItem>();
        }

        public async Task GetItemsAsync()
        {
            var blacklistIds = AppSettings.GetBlacklistEventsIds();

            IsEmptyBlacklist = blacklistIds.Count == 0;

            foreach (var blacklistId in blacklistIds)
            {
                var blacklistEvent = await App.FacebookClient.GetEventAsync(blacklistId);

                Events.Add(new EventDisplayItem
                {
                    Event = blacklistEvent,
                    Visited = true
                });
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
