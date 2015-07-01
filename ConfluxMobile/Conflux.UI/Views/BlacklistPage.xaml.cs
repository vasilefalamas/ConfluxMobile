using Windows.UI.Xaml.Navigation;
using Conflux.UI.Common;
using Conflux.UI.ViewModels;
using Windows.UI.Xaml;
using Conflux.Core.Models;
using Conflux.Core.Settings;
using System.Collections.Generic;

namespace Conflux.UI.Views
{
    public sealed partial class BlacklistPage
    {
        private readonly BlacklistViewModel viewModel;

        private readonly NavigationHelper navigationHelper;

        public BlacklistPage()
        {
            InitializeComponent();

            navigationHelper = new NavigationHelper(this);

            viewModel = new BlacklistViewModel();
            DataContext = viewModel;
        }

        public NavigationHelper NavigationHelper
        {
            get
            {
                return navigationHelper;
            }
        }
        
        private void OnRemoveClick(object sender, RoutedEventArgs e)
        {
            var selectedItems = new List<EventDisplayItem>();

            foreach(var item in viewModel.Events)
            {
                if (item.Selected)
                {
                    selectedItems.Add(item);
                }
            }

            foreach(var item in selectedItems)
            {
                AppSettings.RemoveBlacklistEvent(item.Event.Id);
                viewModel.Events.Remove(item);
            }
        }


        #region NavigationHelper registration

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);

            await viewModel.GetItemsAsync();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
