﻿using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Conflux.Core.Models;
using Conflux.UI.Common;
using Conflux.UI.ViewModels;

namespace Conflux.UI.Views
{
    public sealed partial class MyEventsPage
    {
        private readonly MyEventsViewModel viewModel;

        private readonly NavigationHelper navigationHelper;

        public MyEventsPage()
        {
            InitializeComponent();

            viewModel = new MyEventsViewModel();
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
                selectedItem.Visited = true;

                Frame.Navigate(typeof(EventDetails), selectedItem);

                listView.SelectedItem = null;
            }
        }
    }
}
