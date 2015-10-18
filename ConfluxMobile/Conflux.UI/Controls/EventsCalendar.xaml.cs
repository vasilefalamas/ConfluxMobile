using System.Collections.Generic;
using Conflux.UI.Models;
using Conflux.UI.ViewModels;
using Conflux.Core.Models;
using Conflux.Connectivity.GraphApi;
using System;

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
            var sampleEvent = new EventDisplayItem
            {
                Event = new Event
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddDays(5),
                    Title = "Some beautiful test event",
                    Location = new LocationInfo
                    {
                        Name = "Sibiu",
                        Longitude = 1,
                        Latitude = 1,
                        Id = 111
                    }
                }
            };


            viewModel.Weeks.Add(new Week
            {
               MonthPeriod = "October",
               DaysPeriod = "10-17",
               Events = new List<EventDisplayItem>
               {
                   sampleEvent
               }
            });

            viewModel.Weeks.Add(new Week
            {
                MonthPeriod = "October",
                DaysPeriod = "17-24",
                Events = new List<EventDisplayItem>
               {
                   sampleEvent
               }
            });
        }
    }
}
