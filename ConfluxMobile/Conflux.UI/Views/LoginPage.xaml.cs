using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
                await new MessageDialog("You need a network connection to log in. Please check your network settings and activate your Wi-Fi or cellular data.", "No connection").ShowAsync();
                LoginAreaGrid.StartAnimation("FadeIn");

                return;
            }

            var loginResult = await TryLogin(FacebookUriProvider.GetConnectionUri());

            if (!loginResult)
            {
                await new MessageDialog("Something wrong happened and the login couldn't complete succesfuly. Please try again later.", "Login unsuccesful").ShowAsync();
                LoginAreaGrid.StartAnimation("FadeIn");

                return;
            }

            ShowWaitingMessage();
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

        private void ShowWaitingMessage()
        {
            LoginAreaGrid.Hide();
            WaitMessageTextBlock.Show();
            WaitMessageTextBlock.Text = string.Format("Getting started...");
        }

        private void OnMoreInfoHyperlinkClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SharedInfo));
        }

        #region UI-related logic

        private void StartGlyphAnimation()
        {
            var glyphStoryboard = LogoGrid.Resources["GlyphStoryboard"] as Storyboard;

            if (glyphStoryboard != null)
            {
                glyphStoryboard.Begin();
            }
        }

        private void StopGlyphAnimation()
        {
            var glyphStoryboard = LogoGrid.Resources["GlyphStoryboard"] as Storyboard;

            if (glyphStoryboard != null)
            {
                glyphStoryboard.Stop();
            }
        }

        #endregion
    }
}
