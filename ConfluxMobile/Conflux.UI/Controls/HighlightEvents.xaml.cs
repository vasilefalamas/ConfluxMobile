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
            if (viewModel.EventsGroups.Count == 0) //OneTime load of highlights on a cached page.
            {
                await viewModel.GetHighlights();
            }
        }
    }
}
