using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Conflux.Connectivity;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;
using Conflux.Core.Maps;
using Conflux.Core.Settings;
using Conflux.UI.Common;

namespace Conflux.UI.Views
{
    public sealed partial class LoadingPage
    {
        private FacebookDataAccess facebookDataAccess;

        private readonly NavigationHelper navigationHelper;

        public NavigationHelper NavigationHelper
        {
            get
            {
                return navigationHelper;
            }
        }

        public LoadingPage()
        {
            navigationHelper = new NavigationHelper(this);

            InitializeComponent();
            
            facebookDataAccess = new FacebookDataAccess(App.FacebookProvider);
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }
        
        private async void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await GetUserData(App.AccessToken);

                Frame.Navigate(typeof(MainHub));
            }
            catch (HttpRequestException)
            {
                Frame.Navigate(typeof(LoginPage));
            }
            
        }

        private async Task GetUserData(AccessToken accessToken)
        {
            var userInfoTask = facebookDataAccess.GetUserNameInfoAsync(accessToken);
            var profilePictureTask = facebookDataAccess.GetProfilePictureAsync(accessToken);

            NotifyStatus("Connecting to Facebook...");

            var userInfo = await userInfoTask;
            var profilePicture = await profilePictureTask;

            NotifyStatus("Finding your location...");

            App.User = userInfo;
            App.User.ProfilePicture = profilePicture;

            if (AppSettings.GetLastKnownLocationUsage())
            {
                App.User.LocationInfo = AppSettings.GetLastKnownLocationInfo();
            }
            else
            {
                App.User.LocationInfo = await GetUserLocationAsync();
                AppSettings.SetLastKnownLocationInfo(App.User.LocationInfo);
            }
            
            NotifyStatus("Getting ready...");
        }

        private async Task<LocationInfo> GetUserLocationAsync()
        {
            Location currentLocation = await LocationFinder.GetLocationInfoAsync();
            
            var locationInfo = new LocationInfo
            {
                Name = currentLocation.City,
                Longitude = currentLocation.Longitude,
                Latitude = currentLocation.Latitude
            };

            return locationInfo;
        }

        private void NotifyStatus(string statusMessage)
        {
            StatusTextBlock.Text = statusMessage;
        }
    }
}
