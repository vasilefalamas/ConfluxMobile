using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Conflux.Connectivity.Authentication;
using Conflux.Core.Models;
using Conflux.Core.Settings;
using Conflux.UI.Common;
using Conflux.UI.ViewModels;

namespace Conflux.UI.Views
{
    public sealed partial class MainHub
    {
        private readonly NavigationHelper navigationHelper;

        private readonly MainHubViewModel confluxHubViewModel;
        
        public MainHub()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            navigationHelper = new NavigationHelper(this);

            var currentLocation = App.User.LocationInfo.Name;
            confluxHubViewModel = new MainHubViewModel(currentLocation);
            
            DataContext = confluxHubViewModel;
        }

        public NavigationHelper NavigationHelper
        {
            get
            {
                return navigationHelper;
            }
        }
        

        #region NavigationHelper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            confluxHubViewModel.Name = App.User.FullName;
            confluxHubViewModel.Location = App.User.LocationInfo.Name;
            confluxHubViewModel.ProfilePicture = App.User.ProfilePicture;

            Frame.BackStack.Clear();

            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        
        private void OnLogOutButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationCacheMode = NavigationCacheMode.Disabled;

            AppSettings.SetAccessToken(new AccessToken(string.Empty, DateTime.Now));
            Frame.Navigate(typeof(LoginPage));
        }

        private void OnNearbyEventsListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;

            if (listView == null)
            {
                return;
            }

            var selectedItem = (EventDisplayItem) listView.SelectedItem;
            
            if (selectedItem != null)
            {
                selectedItem.Visited = true;
                
                Frame.Navigate(typeof (EventDetails), selectedItem);

                listView.SelectedItem = null;
            }
        }

        private void OnMyEventsClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (MyEventsPage));
        }

        private void OnSearchPreferencesClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (SearchPreferencesPage));
        }
    }
}
