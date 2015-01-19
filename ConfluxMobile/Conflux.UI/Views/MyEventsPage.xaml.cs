using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Conflux.Core.Models;
using Conflux.UI.Common;
using Conflux.UI.ViewModels;

namespace Conflux.UI.Views
{
    public sealed partial class MyEventsPage
    {
        private MyEventsViewModel viewModel;

        private readonly NavigationHelper navigationHelper;

        public MyEventsPage()
        {
            InitializeComponent();

            viewModel = new MyEventsViewModel(App.FacebookProvider, App.AccessToken);
            DataContext = viewModel;

            navigationHelper = new NavigationHelper(this);
        }

        public NavigationHelper NavigationHelper
        {
            get { return navigationHelper; }
        }

        #region NavigationHelper registration

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);

            //TODO : avoid getting the items each time the View is loaded. Perhaps cache view.
            await viewModel.GetMyEvents();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void OnMyEventsListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;

            if (listView == null)
            {
                return;
            }

            var selectedItem = (EventDisplayItem)listView.SelectedItem;

            if (selectedItem != null)
            {
                Frame.Navigate(typeof(EventDetails), selectedItem);
            }
        }
    }
}
