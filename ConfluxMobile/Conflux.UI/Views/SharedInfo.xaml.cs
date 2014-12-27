using Windows.UI.Xaml.Navigation;
using Conflux.UI.Common;
using Conflux.UI.ViewModels;

namespace Conflux.UI.Views
{
    public sealed partial class SharedInfo
    {
        private readonly NavigationHelper navigationHelper;

        public SharedInfo()
        {
            navigationHelper = new NavigationHelper(this);

            InitializeComponent();

            var sharedInfoViewModel = new SharedInfoViewModel();

            DataContext = sharedInfoViewModel;
        }

        public NavigationHelper NavigationHelper
        {
            get
            {
                return navigationHelper;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }
    }
}
