using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;
using Conflux.Core.Models;
using Conflux.Core.Settings;
using Conflux.UI.Common;
using Conflux.UI.ViewModels;

namespace Conflux.UI.Views
{
    public sealed partial class MainHub
    {
        private readonly NavigationHelper navigationHelper;

        private readonly ConfluxHubViewModel confluxHubViewModel;
        
        public MainHub()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            navigationHelper = new NavigationHelper(this);

            confluxHubViewModel = new ConfluxHubViewModel(App.FacebookProvider, App.AccessToken);
            
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

        private async void OnNewestEventsSectionLoaded(object sender, RoutedEventArgs e)
        {
            if (confluxHubViewModel.NewestEvents.Count == 0)
            {
                await confluxHubViewModel.SearchEvents();
            }
        }

        private void OnLogOutButtonClick(object sender, RoutedEventArgs e)
        {
            AppSettings.SetAccessToken(new AccessToken(string.Empty, DateTime.Now));
            Frame.Navigate(typeof(LoginPage));
        }

        private void OnNewestEventContextMenuClicked(object sender, RoutedEventArgs e)
        {
            var item = (e.OriginalSource as FrameworkElement);

            if (item != null)
            {
                var selection = (Event) item.DataContext;

                confluxHubViewModel.NewestEvents.Remove(selection);

                //TODO : Remove from serialized data
            }
        }
    }
}
