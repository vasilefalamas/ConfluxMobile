using Windows.UI.Xaml.Navigation;
using Conflux.UI.Common;

namespace Conflux.UI.Views
{
    public sealed partial class BlacklistPage
    {
        private readonly NavigationHelper navigationHelper;

        public BlacklistPage()
        {
            InitializeComponent();

            navigationHelper = new NavigationHelper(this);
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
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
