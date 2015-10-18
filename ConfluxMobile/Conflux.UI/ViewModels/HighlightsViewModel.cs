using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Conflux.Connectivity.GraphApi;
using Conflux.Core.Extensions;
using Conflux.Core.Models;
using Conflux.Core.Settings;
using Conflux.UI.Models;

namespace Conflux.UI.ViewModels
{
    public class HighlightsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EventsGroup> weeks;

        public ObservableCollection<EventsGroup> Weeks
        {
            get
            {
                return weeks;
            }
            set
            {
                weeks = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoadComplete;

        public event PropertyChangedEventHandler PropertyChanged;

        public HighlightsViewModel()
        {
            Weeks = CreateWeeks();
        }

        public async Task GetHighlights()
        {
            var location = App.User.LocationInfo.Name;
            var startDate = DateTime.Now.AddDays(-7);
            var endDate = DateTime.Now.AddDays(14);

            var events = await App.FacebookClient.GetEventsByKeywordAsync(location, 0, 50, startDate, endDate);
            //Eliminate blacklist events :
            var filteredEvents = events.Where(item => !AppSettings.GetBlacklistEventsIds().Contains(item.Id));

            GroupEvents(filteredEvents.Select(item => new EventDisplayItem
            {
                Event = item
            }));

            OrderEvents();

            IsLoadComplete = true;
        }

        private ObservableCollection<EventsGroup> CreateWeeks()
        {
            // TODO : Refactor week (not weeks) creation in a separate method
            var weeks = new ObservableCollection<EventsGroup>();

            var startOfWeek = DateTimeExtensions.StartOfWeek();
            var thisWeekStartTime = new DateTime(startOfWeek.Year, startOfWeek.Month, startOfWeek.Day);
            var thisWeek = new EventsGroup
            {
                WeekLabel = "this week",
                StartTime = thisWeekStartTime,
                EndTime = thisWeekStartTime.Date.AddDays(7)
            };
            thisWeek.BuildDaysLabel();
            
            var previousWeek = new EventsGroup
            {
                WeekLabel = "previous week",
                StartTime = thisWeek.StartTime.Date.AddDays(-7),
                EndTime = thisWeek.EndTime.Date.AddDays(-7)
            };
            previousWeek.BuildDaysLabel();

            var upcomingWeek = new EventsGroup
            {
                WeekLabel = "upcoming week",
                StartTime = thisWeek.StartTime.Date.AddDays(7),
                EndTime = thisWeek.EndTime.Date.AddDays(7)
            };
            upcomingWeek.BuildDaysLabel();

            weeks.Add(previousWeek);
            weeks.Add(thisWeek);
            weeks.Add(upcomingWeek);

            return weeks;
        }


        private void GroupEvents(IEnumerable<EventDisplayItem> events)
        {
            foreach(var eventItem in events)
            {
                foreach(var week in weeks)
                {
                    if (eventItem.Event.StartTime > week.StartTime &&
                        eventItem.Event.StartTime < week.EndTime)
                    {
                        week.Add(eventItem);
                        break;
                    }
                }
            }
        }

        private void OrderEvents()
        {
            foreach (var eventsInWeek in Weeks)
            {
                eventsInWeek.OrderBy(eventItem => eventItem.Event.StartTime);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
