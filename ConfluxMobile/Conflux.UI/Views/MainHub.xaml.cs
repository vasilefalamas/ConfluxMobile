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

        private void OnLinkSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemsList = sender as ListView;

            if (itemsList != null)
            {
                var selectedLink = (NavigationLink)itemsList.SelectedItem;

                if (selectedLink != null)
                {
                    Frame.Navigate(selectedLink.Destination);
                }

                itemsList.SelectedItem = null;
            }
        }

        private void OnLogOutButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationCacheMode = NavigationCacheMode.Disabled;

            AppSettings.SetAccessToken(new AccessToken(string.Empty, DateTime.Now));
            Frame.Navigate(typeof(LoginPage));
        }

        private void OnAddToBlacklist(object sender, RoutedEventArgs e)
        {
            var item = (e.OriginalSource as FrameworkElement);

            if (item != null)
            {
                var selection = (EventDisplayItem) item.DataContext;

                confluxHubViewModel.NewestEvents.Remove(selection);

                //TODO : Remove from serialized data
            }
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
    }
}
