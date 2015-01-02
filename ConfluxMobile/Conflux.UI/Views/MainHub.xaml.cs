using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Conflux.Connectivity;
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

        private readonly FacebookProvider facebookClient;

        public MainHub()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            navigationHelper = new NavigationHelper(this);

            confluxHubViewModel = new ConfluxHubViewModel();
            facebookClient = new FacebookProvider();

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

        private async Task<IEnumerable<Event>> GetNewestEvents(AccessToken accessToken, string searchedKeyword, int offset = 0, int? limit = null)
        {
            IEnumerable<Event> newestEvents = await facebookClient.GetEventsByKeywordAsync(accessToken, searchedKeyword, offset, limit);

            return newestEvents;
        }

        private async void OnNewestEventsSectionLoaded(object sender, RoutedEventArgs e)
        {
            string currentLocation = App.User.LocationInfo.Name;

            var newestEvents = (await GetNewestEvents(App.AccessToken, currentLocation, 0, 10)).ToList();
            
            //TODO : Find a different approach and do not clear the List
            confluxHubViewModel.NewestEvents.Clear();

            foreach (var newEvent in newestEvents)
            {
                confluxHubViewModel.NewestEvents.Add(newEvent);   
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
