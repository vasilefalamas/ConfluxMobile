﻿using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Conflux.Core.Models;
using Conflux.Core.Settings;
using Conflux.UI.Common;
using Conflux.UI.Helpers;
using Conflux.UI.Models;
using Conflux.UI.ViewModels;

namespace Conflux.UI.Views
{
    public sealed partial class EventDetails
    {
        private readonly NavigationHelper navigationHelper;

        private readonly EventDetailsViewModel viewModel;

        private Grid shortDescriptionPanel;

        private Grid fullDescriptionPanel;

        public EventDetails()
        {
            InitializeComponent();

            viewModel = new EventDetailsViewModel(AppSettings.GetAllowImagesDownloadStatus());
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

                await viewModel.InitializeAsync(passedEventDetailedItem.Event.Id);
                LoadingModalGrid.StartAnimation("FadeOut");
            }

            if (viewModel.IsMapLocationAvailable)
            {
                MapGrid.Visibility = Visibility.Visible;
                await ShowEventOnMapAsync();
            }

            if (AppSettings.GetAllowImagesDownloadStatus())
            {
                await viewModel.LoadPhotosAsync();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (viewModel.IsMapSelectionActive)
            {
                HideMapSelection();

                e.Cancel = true;
            }

            base.OnNavigatingFrom(e);
        }

        #endregion

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            MapGrid.Visibility = Visibility.Collapsed;

            shortDescriptionPanel = AboutHubSection.FindChildControl<Grid>("ShortDescriptionPanel") as Grid;
            fullDescriptionPanel = AboutHubSection.FindChildControl<Grid>("FullDescriptionPanel") as Grid;
        }

        private void OnShowMoreTapped(object sender, TappedRoutedEventArgs e)
        {
            shortDescriptionPanel.Hide();
            fullDescriptionPanel.Show();
        }


        private void OnShowLessTapped(object sender, TappedRoutedEventArgs e)
        {
            shortDescriptionPanel.Show();
            fullDescriptionPanel.Hide();
        }

        private async Task ShowEventOnMapAsync()
        {
            var mapCenterPoint = viewModel.GetEventPosition();

            var pin = new MapIcon
            {
                Location = mapCenterPoint,
                Title = viewModel.Location,
                Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pin128.png")),
                NormalizedAnchorPoint = new Point { X = 0.32, Y = 0.78 }
            };

            LocationMap.MapElements.Add(pin);

            await LocationMap.TrySetViewAsync(mapCenterPoint, 17);
        }

        private void OnGetEventDirectionClicked(object sender, RoutedEventArgs e)
        {
            ShowMapSelection();
        }

        private void OnMapSelectionCancelTapped(object sender, TappedRoutedEventArgs e)
        {
            HideMapSelection();
        }

        private async void OnAttendClicked(object sender, RoutedEventArgs e)
        {
            var attendSuccesful = await App.FacebookClient.PostEventAttendanceAsync(viewModel.Id);

            if (attendSuccesful)
            {
                await new MessageDialog("Your request to attend this event has been handled with success. You're now one of the attendees!", "Attend status").ShowAsync();
            }
            else
            {
                await new MessageDialog("There was a problem trying to attend to this event. Please try again later.", "Attend status").ShowAsync();
            }
        }

        //TODO : Extract this into a separate helper class. Solve dependency with VM usage.
        private void ShowMapSelection()
        {
            viewModel.IsMapSelectionActive = true;

            var jumpOutAnimation = MainContent.Resources["JumpOut"] as Storyboard;
            var jumpInAnimation = MapAppSelectionGrid.Resources["JumpIn"] as Storyboard;

            if (jumpInAnimation == null || jumpOutAnimation == null)
            {
                return;
            }

            jumpOutAnimation.Begin();
            jumpInAnimation.Begin();
        }

        //TODO : Extract this into a separate helper class. Solve dependency with VM usage.
        private void HideMapSelection()
        {
            viewModel.IsMapSelectionActive = false;

            var jumpOutAnimation = MainContent.Resources["JumpIn"] as Storyboard;
            var jumpInAnimation = MapAppSelectionGrid.Resources["JumpOut"] as Storyboard;


            if (jumpInAnimation == null || jumpOutAnimation == null)
            {
                return;
            }

            jumpOutAnimation.Begin();
            jumpInAnimation.Begin();
        }

        private async void OnMapAppItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null)
            {
                return;
            }

            var selectedItem = listView.SelectedItem as MapAppItem;
            if (selectedItem == null)
            {
                return;
            }

            listView.SelectedItem = null;

            var mapUri = new Uri(selectedItem.UriString);
            await Launcher.LaunchUriAsync(mapUri);

            HideMapSelection();
        }
    }
}
