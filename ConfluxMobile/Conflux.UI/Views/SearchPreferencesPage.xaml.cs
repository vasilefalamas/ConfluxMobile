using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Conflux.Core.Settings;
using Conflux.UI.Common;

namespace Conflux.UI.Views
{
    public sealed partial class SearchPreferencesPage 
    {
        private readonly NavigationHelper navigationHelper;

        public SearchPreferencesPage()
        {
            InitializeComponent();

            navigationHelper = new NavigationHelper(this);


        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        
        #region NavigationHelper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void OnLastKnownLocationChecked(object sender, RoutedEventArgs e)
        {
            AppSettings.SetLocationCacheStatus(true); 
        }


        private void OnLastKnownLocationUnchecked(object sender, RoutedEventArgs e)
        {
            AppSettings.SetLocationCacheStatus(false); 
        }

        private void OnSaveDataTransferToggled(object sender, RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;

            if (toggleSwitch != null)
            {
                var toggleState = toggleSwitch.IsOn;

                AppSettings.SetImageDownloadAllowedStatus(toggleState);
            }
        }
    }
}
