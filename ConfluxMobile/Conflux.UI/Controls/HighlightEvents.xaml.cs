using System.Linq;
using Windows.UI.Xaml;
using Conflux.UI.ViewModels;

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
            var mockEvents = viewModel.GetMockEvents();
            await viewModel.BuildGroupsAsync(mockEvents);

            viewModel.AddRange(mockEvents.Select(item => item.Event));

        }
    }
}
