using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Conflux.Connectivity;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;
using Conflux.Core.Maps;
using Conflux.UI.Common;

namespace Conflux.UI.Views
{
    public sealed partial class LoadingPage
    {
        private IFacebookProvider facebookProvider;

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

            RemovedPageTransition();

            facebookProvider = new FacebookProvider();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private void RemovedPageTransition()
        {
            var mainFrame = Window.Current.Content as Frame;
            if (mainFrame != null)
            {
                mainFrame.ContentTransitions = null;
            }
        }

        private async void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            await GetUserData(App.AccessToken);

            Frame.Navigate(typeof (MainHub));
        }

        private async Task GetUserData(AccessToken accessToken)
        {
            var userInfoTask = facebookProvider.GetUserNameInfoAsync(accessToken);
            var profilePictureTask = facebookProvider.GetProfilePictureAsync(accessToken);

            NotifyStatus("Connecting to Facebook...");

            var userInfo = await userInfoTask;
            var profilePicture = await profilePictureTask;

            NotifyStatus("Finding your location...");

            App.User = userInfo;
            App.User.ProfilePicture = profilePicture;
            //App.User.LocationInfo = await GetUserLocationAsync();
            App.User.LocationInfo = new LocationInfo {Name = "Cugir"}; //TODO : Mock location
            
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
