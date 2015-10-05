using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Conflux.UI.Helpers;
using Conflux.UI.Models;
using Conflux.UI.ViewModels;

namespace Conflux.UI.Controls
{
    public sealed partial class EventsCalendar
    {
        public EventCalendarViewModel viewModel;

        public EventsCalendar()
        {
            InitializeComponent();
            viewModel = new EventCalendarViewModel();

            DataContext = viewModel;

            InitializeCalendarItems();
        }

        private void InitializeCalendarItems()
        {
            viewModel.Weeks.Add(new Week
            {
                Days = new List<Day>
                {
                    new Day("1", Colors.Gray),
                    new Day("2", Colors.Gray),
                    new Day("3", Colors.Gray),
                    new Day("4", Colors.Teal),
                    new Day("5", Colors.Teal),
                    new Day("6", Colors.Teal),
                    new Day("7", Colors.Teal),
                }
            });

            viewModel.Weeks.Add(new Week
            {
                Days = new List<Day>
                {
                    new Day("8", Colors.Teal),
                    new Day("9", Colors.Teal),
                    new Day("10", Colors.Teal),
                    new Day("11", Colors.Teal),
                    new Day("12", Colors.Teal),
                    new Day("13", Colors.Gray),
                    new Day("14", Colors.Gray),
                }
            });
        }

        private void OnCalendarItemClick(object sender, RoutedEventArgs e)
        {
            var daysGrid = VisualTreeHelper.GetParent(sender as DependencyObject);
            var weekGrid = VisualTreeHelper.GetParent(daysGrid);
            
            var eventsPanel = weekGrid.FindChildControl<StackPanel>("DayEvents");

            if (eventsPanel == null)
            {
                return;
            }

            var element = (FrameworkElement)eventsPanel;

            if (element.Height > 0)
            {
                var fadeOut = (Storyboard)element.Resources["FadeOut"];
                fadeOut.Begin();
            }
            else
            {
                var fadeIn = (Storyboard)element.Resources["FadeIn"];
                fadeIn.Begin();
            }
        }
    }
}
