using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Conflux.Connectivity.GraphApi;
using Conflux.UI.Models;

namespace Conflux.UI.ViewModels
{
    public class EventDetailsViewModel : INotifyPropertyChanged
    {
        private const int ShortDescriptionWordsLength = 30;

        private string id;

        private string title;

        private bool isDescriptionAvailable;

        private bool isDescriptionTooLong;

        private string shortDescription;

        private string fullDescription;
        
        private DateTime? startTime;

        private DateTime? endTime;

        private LocationInfo locationInfo;
        
        private ObservableCollection<MapAppItem> mapAppsOptions;

        private string hereMapsIncompleteUriString = "explore-maps://v2.0/show/place/?latlon={0},{1}&title={2}&zoom={3}";

        private string defaultMapsIncompleteUriString = "bingmaps:?collection=point.{0}_{1}_{2}&lvl={3}";
        
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

        public bool IsDescriptionAvailable
        {
            get
            {
                return isDescriptionAvailable;
            }
            private set
            {
                isDescriptionAvailable = value;
                OnPropertyChanged();
            }
        }

        public bool IsDescriptionTooLong
        {
            get
            {
                return isDescriptionTooLong;
            }
            private set
            {
                isDescriptionTooLong = value;
                OnPropertyChanged();
            }
        }

        public string ShortDescription
        {
            get
            {
                return shortDescription;
            }
            private set
            {
                shortDescription = value;
                OnPropertyChanged();
            }
        }
        public string FullDescription
        {
            get
            {
                return fullDescription;
            }
            private set
            {
                fullDescription = value;
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
                return locationInfo != null && locationInfo.Id != 0;
            }
        }

        public bool IsMapSelectionActive { get; set; }

        public LocationInfo Location { get; private set; }

        public EventDetailsViewModel()
        {
            MapAppsOptions = new ObservableCollection<MapAppItem>();
        }

        public ObservableCollection<MapAppItem> MapAppsOptions
        {
            get
            {
                return mapAppsOptions;
            }
            private set
            {
                mapAppsOptions = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Build(Event eventItem)
        {
            AddEventData(eventItem);

            if (Location != null)
            {
                AddMapApps();
            }
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddEventData(Event eventItem)
        {
            var cleanedDescription = eventItem.Description == null ? string.Empty : eventItem.Description.Trim();

            Id = eventItem.Id;
            Title = eventItem.Title.ToUpper();
            IsDescriptionAvailable = !string.IsNullOrEmpty(cleanedDescription);
            ShortDescription = GetShortDescription(cleanedDescription);
            FullDescription = cleanedDescription;
            IsDescriptionTooLong = DetermineIsDescriptionTooLong();
            StartTime = eventItem.StartTime;
            EndTime = eventItem.EndTime;
            locationInfo = eventItem.Location;
            Location = eventItem.Location;
        }

        private string GetShortDescription(string description)
        {
            var words = description.Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (words.Count <= ShortDescriptionWordsLength)
            {
                return description;
            }

            var shortenedDescription = string.Join(" ", words.Take(ShortDescriptionWordsLength));

            return string.Format("{0}...", shortenedDescription);
        }

        private bool DetermineIsDescriptionTooLong()
        {
            if (ShortDescription.Length == FullDescription.Length)
            {
                return false;
            }

            return ShortDescription.Length < FullDescription.Length + 3; //Ellipsis included
        }
        private void AddMapApps()
        {
            var hereMaps = new MapAppItem
            {
                Name = "Here Maps",
                Description = "Displays the event using Here Maps (if the app is available).",
                UriString = GetCompleteEventUriString(hereMapsIncompleteUriString, Title, locationInfo.Latitude, locationInfo.Longitude)
            };

            var defaultMaps = new MapAppItem
            {
                Name = "Windows Phone Maps",
                Description = "Displays the event using the built-in Maps app in Windows Phone.",
                UriString = GetCompleteEventUriString(defaultMapsIncompleteUriString, Title, locationInfo.Latitude, locationInfo.Longitude)
            };

            MapAppsOptions.Add(hereMaps);
            MapAppsOptions.Add(defaultMaps);
        }

        private static string GetCompleteEventUriString(string baseUriString, string title, double latitude, double longitude, int zoomLevel = 17)
        {
            return string.Format(baseUriString,
                Uri.EscapeDataString(latitude.ToString()),
                Uri.EscapeDataString(longitude.ToString()),
                Uri.EscapeDataString(title),
                Uri.EscapeDataString(zoomLevel.ToString()));
        }
    }
}
