using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;
using Conflux.Connectivity.GraphApi;
using Conflux.Core.Models;
using Conflux.UI.Common;
using Conflux.UI.ViewModels;

namespace Conflux.UI.Views
{
    public sealed partial class EventDetails
    {
        private readonly NavigationHelper navigationHelper;

        private readonly EventDetailsViewModel viewModel;

        public EventDetails()
        {
            InitializeComponent();

            viewModel = new EventDetailsViewModel(App.FacebookProvider, App.AccessToken);
            DataContext = viewModel;

            navigationHelper = new NavigationHelper(this);
            navigationHelper.LoadState += NavigationHelper_LoadState;
            navigationHelper.SaveState += NavigationHelper_SaveState;
        }

        public NavigationHelper NavigationHelper
        {
            get { return navigationHelper; }
        }


        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);

            var passedEventDetailedItem = (EventDisplayItem)e.Parameter;

            if (passedEventDetailedItem != null)
            {
                LoadingModalGrid.StartAnimation("FadeIn");
                await viewModel.GetEventData(passedEventDetailedItem.Event.Id);
                LoadingModalGrid.StartAnimation("FadeOut");
            }

            if (!viewModel.IsMapLocationAvailable)
            {
                if (EventDetailsPivot.Items != null)
                {
                    EventDetailsPivot.Items.RemoveAt(1);
                }
            }

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        
        private async void OnEventPivotSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!viewModel.IsMapLocationAvailable)
            {
                return;
            }

            var pivot = sender as Pivot;

            if (pivot == null)
            {
                return;
            }

            if (pivot.SelectedIndex == 1)
            {
                await ShowEventOnMapAsync(viewModel.Location);
            }
        }

        private async Task ShowEventOnMapAsync(LocationInfo location)
        {
            var geoposition = new BasicGeoposition
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };

            var mapCenterPoint = new Geopoint(geoposition);

            var pin = new MapIcon
            {
                Location = mapCenterPoint,
                Title = location.Name,
                Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pin128.png")),
                NormalizedAnchorPoint = new Point {X = 0.32, Y = 0.78}
            };

            LocationMap.MapElements.Add(pin);

            await LocationMap.TrySetViewAsync(mapCenterPoint, 15);
        }
    }
}
