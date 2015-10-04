using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
                Monday = new Day("1", Colors.Gray),
                Tuesday = new Day("2", Colors.Gray),
                Wednesday = new Day("3", Colors.Gray),
                Thursday = new Day("4", Colors.Teal),
                Friday = new Day("5", Colors.Teal),
                Saturday = new Day("6", Colors.Teal),
                Sunday = new Day("7", Colors.Teal)
            });

            viewModel.Weeks.Add(new Week
            {
                Monday = new Day("8", Colors.Gray),
                Tuesday = new Day("9", Colors.Gray),
                Wednesday = new Day("10", Colors.Gray),
                Thursday = new Day("11", Colors.Teal),
                Friday = new Day("12", Colors.Teal),
                Saturday = new Day("13", Colors.Teal),
                Sunday = new Day("14", Colors.Teal)
            });
        }


        private async void OnCalendarItemTapped(object sender, TappedRoutedEventArgs e)
        {
            var text = (sender as Button).Content;

            await new MessageDialog(text.ToString()).ShowAsync();

        }
    }
}
