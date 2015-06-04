using System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Conflux.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TermsOfUsePage
    {
        public TermsOfUsePage()
        {
            InitializeComponent();

            var applicationView = ApplicationView.GetForCurrentView();
            applicationView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void OnContinueClick(object sender, RoutedEventArgs e)
        {
            FadeOut();
        }

        private void FadeOut()
        {
            var storyboard = (Storyboard) ContentGrid.Resources["FadeOut"];
            storyboard.Begin();
        }
        
        private void OnFadeOutCompleted(object sender, object e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
