using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Conflux.Core.Models;
using Conflux.UI.ViewModels;
using Conflux.UI.Views;

namespace Conflux.UI.Controls
{
    public sealed partial class HighlightEvents
    {
        private readonly HighlightsViewModel viewModel;

        public HighlightEvents()
        {
            InitializeComponent();

            viewModel = new HighlightsViewModel();
            DataContext = viewModel;

        }

        private async void OnControlLoaded(object sender, RoutedEventArgs e)
        {
            if (viewModel.EventsGroups.Count == 0) //OneTime load of highlights on a cached page.
            {
                await viewModel.GetHighlights();
            }
        }

        private void OnHighlightsEventsListSelectionChanged(object sender, SelectionChangedEventArgs e)
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

                var frame = (Frame) Window.Current.Content;
                frame.Navigate(typeof(EventDetails), selectedItem);

                listView.SelectedItem = null;
            }
        }
    }
}
