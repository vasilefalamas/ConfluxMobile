using Windows.UI.Xaml.Navigation;
using Conflux.UI.Common;
using Conflux.UI.ViewModels;

namespace Conflux.UI.Views
{
    public sealed partial class SearchPreferencesPage 
    {
        private readonly NavigationHelper navigationHelper;

        public SearchPreferencesPage()
        {
            InitializeComponent();

            navigationHelper = new NavigationHelper(this);

            var viewModel = new SearchPreferencesViewModel();
            DataContext = viewModel;
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
    }
}
