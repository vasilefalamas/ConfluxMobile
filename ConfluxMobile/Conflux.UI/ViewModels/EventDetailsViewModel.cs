using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;

namespace Conflux.UI.ViewModels
{
    public class EventDetailsViewModel : INotifyPropertyChanged
    {
        private string id;

        private string title;

        private string description;

        private DateTime? startTime;

        private DateTime? endTime;

        private bool isMapLocationAvailable;
        
        private readonly IFacebookProvider facebookProvider;

        private readonly AccessToken accessToken;

        public string Id
        {
            get
            {
                return id;
            }
            private set
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
            private set
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
            private set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public DateTime? StartTime
        {
            get
            {
                return startTime;
            }
            private set
            {
                startTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime? EndTime
        {
            get
            {
                return endTime;
            }
            private set
            {
                endTime = value;
                OnPropertyChanged();
            }
        }

        public bool IsMapLocationAvailable
        {
            get
            {
                return isMapLocationAvailable;
            }
            private set
            {
                isMapLocationAvailable = value;
                OnPropertyChanged();
            }
        }

        public LocationInfo Location { get; private set; }

        public EventDetailsViewModel(IFacebookProvider facebookProvider, AccessToken accessToken)
        {
            this.facebookProvider = facebookProvider;
            this.accessToken = accessToken;
        }

        public async Task GetEventData(string eventId)
        {
            var eventItem = await facebookProvider.GetEventAsync(accessToken, eventId);
            
            Id = eventItem.Id;
            Title = eventItem.Title;
            Description = eventItem.Description;
            StartTime = eventItem.StartTime;
            EndTime = eventItem.EndTime;
            IsMapLocationAvailable = eventItem.Location != null && eventItem.Location.Id != 0;
            Location = eventItem.Location;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
