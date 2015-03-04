using System;
using System.Globalization;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Conflux.Core.Models;

namespace Conflux.UI.VisualControls
{
    public sealed partial class OpenMapChoiceDialog
    {
        private readonly string eventName;
        
        private double eventLongitude;

        private double eventLatitude;

        private const int MapZoomLevel = 17;

        public OpenMapChoiceDialog(EventDisplayItem eventItem)
        {
            InitializeComponent();

            eventName = eventItem.Event.Title;
            eventLatitude = eventItem.Event.Location.Latitude;
            eventLongitude = eventItem.Event.Location.Longitude;

            EventTitleTextRun.Text = eventName;
        }
        
        private void OnContentLoaded(object sender, RoutedEventArgs e)
        {
            //var installedPackages =  InstallationManager.FindPackages()

        }

        private void OnMapsItemTapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private async void OnHereDriveItemTapped(object sender, TappedRoutedEventArgs e)
        {
            var longitude = eventLongitude.ToString(CultureInfo.InvariantCulture);
            var latitude = eventLatitude.ToString(CultureInfo.InvariantCulture);

            var uriString = GetHereMapsEventUriString(eventName, longitude, latitude);

            await Launcher.LaunchUriAsync(new Uri(uriString));
        }

        private string GetHereMapsEventUriString(string name, string longitude, string latitude)
        {
            return string.Format("explore-maps://v2.0/show/place/?latlon={0},{1}&title={2}&zoom={3}", 
                Uri.EscapeDataString(latitude), 
                Uri.EscapeDataString(longitude), 
                Uri.EscapeDataString(name), 
                Uri.EscapeDataString(MapZoomLevel.ToString(CultureInfo.InvariantCulture)));
        }
    }
}
