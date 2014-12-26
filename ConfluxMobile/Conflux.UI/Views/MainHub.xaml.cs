using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Conflux.Connectivity;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;
using Conflux.Core;
using Conflux.Core.Models;
using Conflux.UI.Common;
using Conflux.UI.ViewModels;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

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
            navigationHelper.LoadState += NavigationHelper_LoadState;
            navigationHelper.SaveState += NavigationHelper_SaveState;

            confluxHubViewModel = new ConfluxHubViewModel();
            facebookClient = new FacebookProvider();

            DataContext = confluxHubViewModel;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            confluxHubViewModel.Name = App.User.FullName;
            confluxHubViewModel.Location = App.User.LocationInfo.Name;
            confluxHubViewModel.ProfilePicture = App.User.ProfilePicture;

            navigationHelper.OnNavigatedTo(e);

            //Clear navigation history.
            Frame.BackStack.Clear();
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
            var newestEvents = await facebookClient.GetEventsByKeywordAsync(accessToken, searchedKeyword, offset, limit);

            return newestEvents;
        }

        private async void OnNewestEventsSectionLoaded(object sender, RoutedEventArgs e)
        {
            var newestEvents = await GetNewestEvents(App.AccessToken, "Sibiu", 0, 10);
            confluxHubViewModel.NewestEvents = newestEvents;
        }

        private void OnLogOutButtonClick(object sender, RoutedEventArgs e)
        {
            AppSettings.SetAccessToken(new AccessToken("", DateTime.Now));
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
