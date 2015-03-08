using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Conflux.UI.Models;

namespace Conflux.UI.ViewModels
{
    public class MapAppSelectionViewModel : INotifyPropertyChanged
    {
        private string eventTitle;

        private ObservableCollection<MapAppItem> mapAppsOptions;

        private string hereMapsIncompleteUriString = "explore-maps://v2.0/show/place/?latlon={0},{1}&title={2}";

        private string defaultMapsIncompleteUriString = "bingmaps:?cp={0}~{1}";

        public string EventTitle
        {
            get
            {
                return eventTitle;
            }
            set
            {
                eventTitle = value;
                OnPropertyChanged();
            }
        }

        public double EventLatitude { get; private set; }

        public double EventLongitude { get; private set; }

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

        public MapAppSelectionViewModel(string eventTitle, double eventLatitude, double eventLongitude)
        {
            EventTitle = eventTitle;
            EventLatitude = eventLatitude;
            EventLongitude = eventLongitude;

            MapAppsOptions = new ObservableCollection<MapAppItem>();

            AddMapApps();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddMapApps()
        {
            var hereMaps = new MapAppItem
            {
                Name = "Here Maps",
                Description = "Opens a map using Here Maps and displays the event. If this app is not installed on your current device, you will be prompted to download it from the Store.",
                UriString = GetCompleteEventUriString(hereMapsIncompleteUriString, EventTitle, EventLatitude, EventLongitude)
            };

            var defaultMaps = new MapAppItem
            {
                Name = "Windows Phone Maps",
                Description = "Opens the event using the built-in Maps app in Windows Phone. This option is also a fallback as a default app in case other apps are not available on the current device.",
                UriString = GetCompleteEventUriString(defaultMapsIncompleteUriString, EventTitle, EventLatitude, EventLongitude)
            };

            MapAppsOptions.Add(hereMaps);
            MapAppsOptions.Add(defaultMaps);
        }

        private static string GetCompleteEventUriString(string baseUriString, string title, double latitude, double longitude)
        {
            return string.Format(baseUriString,
                Uri.EscapeDataString(latitude.ToString()),
                Uri.EscapeDataString(longitude.ToString()),
                Uri.EscapeDataString(title));
        }
    }
}
