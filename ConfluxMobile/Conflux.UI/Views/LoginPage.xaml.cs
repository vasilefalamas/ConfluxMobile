using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
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

            InitializeStatusBar();
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

        private static async void InitializeStatusBar()
        {
            var applicationView = ApplicationView.GetForCurrentView();
            applicationView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);

            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ForegroundColor = Colors.Teal;
            await statusBar.ShowAsync();
        }

        private async void OnLoginClick(object sender, RoutedEventArgs e)
        {
            LoginAreaGrid.StartAnimation("FadeOut");

            if (!NetworkManager.HasInternetAccess)
            {
                await DisplayStatusMessageAsync("Please check your connection before you login.");
                LoginAreaGrid.StartAnimation("FadeIn");
                await HideStatusMessageAsync(3000);

                return;
            }

            var loginResult = await TryLogin(FacebookUriProvider.GetConnectionUri());

            if (!loginResult)
            {
                await DisplayStatusMessageAsync("Couldn't launch Facebook. Please retry.");
                LoginAreaGrid.StartAnimation("FadeIn");
                await HideStatusMessageAsync(3000);
            }
        }

        private async Task DisplayStatusMessageAsync(string message)
        {
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ProgressIndicator.Text = message;

            await statusBar.ProgressIndicator.ShowAsync();
        }

        private async Task HideStatusMessageAsync(int waitingMilliseconds)
        {
            await Task.Delay(waitingMilliseconds);

            var statusBar = StatusBar.GetForCurrentView();
            await statusBar.ProgressIndicator.HideAsync();
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
