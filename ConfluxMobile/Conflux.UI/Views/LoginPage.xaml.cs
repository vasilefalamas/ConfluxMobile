using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Conflux.Connectivity;
using Conflux.Connectivity.GraphApi;
using Conflux.UI.Common;

namespace Conflux.UI.Views
{
    public sealed partial class LoginPage
    {
        private readonly NavigationHelper navigationHelper;

        public LoginPage()
        {
            InitializeComponent();

            navigationHelper = new NavigationHelper(this);

            StatusBarHandler.InitializeAsync(Colors.Teal);
        }

        public NavigationHelper NavigationHelper
        {
            get { return navigationHelper; }
        }
        
        #region NavigationHelper registration
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);

            StartGlyphAnimation();

            Frame.BackStack.Clear();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);

            StopGlyphAnimation();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            StopGlyphAnimation();
        }

        #endregion

        private async void OnLoginClick(object sender, RoutedEventArgs e)
        {
            LoginAreaGrid.StartAnimation("FadeOut");

            if (!NetworkManager.HasInternetAccess)
            {
                await StatusBarHandler.ShowMessageAsync("Please check your connection before you login.", 3000);
                LoginAreaGrid.StartAnimation("FadeIn");

                return;
            }

            var loginResult = await TryLogin(FacebookUriCollection.GetConnectionUri());

            if (!loginResult)
            {
                await StatusBarHandler.ShowMessageAsync("Couldn't launch Facebook. Please retry.", 3000);
                LoginAreaGrid.StartAnimation("FadeIn");
            }
        }

        private async Task<bool> TryLogin(Uri loginUri)
        {
            try
            {
                await Launcher.LaunchUriAsync(loginUri);

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void OnMoreInfoHyperlinkClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SharedInfo));
        }

        #region UI-related logic

        private void StartGlyphAnimation()
        {
            var glyphStoryboard = (Storyboard) LogoGrid.Resources["GlyphStoryboard"];
            glyphStoryboard.Begin();
        }

        private void StopGlyphAnimation()
        {
            var glyphStoryboard = (Storyboard) LogoGrid.Resources["GlyphStoryboard"];
            glyphStoryboard.Stop();
        }
        #endregion
    }
}
