using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Conflux.Connectivity.GraphApi;
using Conflux.Core.Models;
using Conflux.UI.Models;

namespace Conflux.UI.ViewModels
{
    public class HighlightsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EventsGroup> eventsGroups;

        public ObservableCollection<EventsGroup> EventsGroups
        {
            get
            {
                return eventsGroups;
            }
            set
            {
                eventsGroups = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public HighlightsViewModel()
        {
            EventsGroups = new ObservableCollection<EventsGroup>();
        }

        public async Task GetHighlights()
        {
            var location = App.User.LocationInfo.Name;
            var startDate = DateTime.Now.AddDays(-7);
            var endDate = DateTime.Now.AddDays(7);

            var events = await App.FacebookClient.GetEventsByKeywordAsync(location, 0, 50, startDate, endDate);

            AddRange(events);
        }

        public void Add(Event eventItem)
        {
            var newEvent = new EventDisplayItem
            {
                Event = eventItem
            };

            AddEventToGroup(newEvent, EventsGroups);
        }

        public void AddRange(IEnumerable<Event> events)
        {
            foreach (var item in events)
            {
                Add(item);
            }
        }

        private void AddEventToGroup(EventDisplayItem eventItem, ICollection<EventsGroup> eventsGroup)
        {
            var dayKey = GetDayKey(eventItem);

            //Event doesn't fit in any existing group. Create a new one.
            if (eventsGroup.All(g => g.Key != dayKey))
            {
                var newGroup = new EventsGroup { Key = dayKey };
                newGroup.Add(eventItem);

                eventsGroup.Add(newGroup);
            }
            //Event can be be added to an existing group.
            else
            {
                var belongingGroup = eventsGroup.SingleOrDefault(group => group.Key == dayKey);
                if (belongingGroup != null)
                {
                    belongingGroup.Add(eventItem);
                }
            }
        }

        private string GetDayKey(EventDisplayItem eventItem)
        {
            var startTime = eventItem.Event.StartTime;
            var dayKey = startTime != null ? startTime.Value.ToString("dddd") : "Unknown";

            return dayKey;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
