using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace Conflux.UI.Common
{
    public class StatusBarHandler
    {
        public static async void InitializeAsync(Color foregroundColor)
        {
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ForegroundColor = foregroundColor;
            await statusBar.ShowAsync();
        }

        public static async Task ShowMessageAsync(string message, uint duration, bool isProgressIndicatorActive = false)
        {
            await ShowAsync(message, isProgressIndicatorActive);
            await Task.Delay((int) duration);
            await HideAsync();
        }

        public static async Task ShowMessageAsync(string message, bool isProgressIndicatorActive = false)
        {
            await ShowAsync(message, isProgressIndicatorActive);
        }

        public static async Task HideAsync()
        {
            var statusBar = StatusBar.GetForCurrentView();
            await statusBar.ProgressIndicator.HideAsync();
        }

        private static async Task ShowAsync(string message, bool isProgressIndicatorActive)
        {
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ProgressIndicator.Text = message;

            statusBar.ProgressIndicator.ProgressValue = isProgressIndicatorActive ? (double?) null : 0;

            await statusBar.ProgressIndicator.ShowAsync();
        }
    }
}
