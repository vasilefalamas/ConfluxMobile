using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Conflux.UI.Common
{
    public static class FrameworkElementExtensions
    {
        public static void Hide(this FrameworkElement element)
        {
            element.Visibility = Visibility.Collapsed;
        }

        public static void Show(this FrameworkElement element)
        {
            element.Visibility = Visibility.Visible;
        }

        public static void StartAnimation(this FrameworkElement element, string storyboardName)
        {
            var storyboard = element.Resources[storyboardName] as Storyboard;

            if (storyboard != null)
            {
                storyboard.Begin();
            }
        }
        public static void StopAnimation(this FrameworkElement element, string storyboardName)
        {
            var storyboard = element.Resources[storyboardName] as Storyboard;

            if (storyboard != null)
            {
                storyboard.Stop();
            }
        }
    }
}
