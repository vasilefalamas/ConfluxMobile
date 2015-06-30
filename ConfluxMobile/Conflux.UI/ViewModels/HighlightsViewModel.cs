using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public List<EventDisplayItem> GetMockEvents()
        {
            return new List<EventDisplayItem>
            {
                new EventDisplayItem
                {
                    Event = new Event
                    {
                        Title = "Title " + Guid.NewGuid().ToString().Substring(0, 5),
                        Description = "Description 1 " + Guid.NewGuid().ToString().Substring(0, 5),
                        StartTime = DateTime.Now.AddDays(1),
                        EndTime = DateTime.Now.AddDays(11)
                    }
                },
                new EventDisplayItem
                {
                    Event = new Event
                    {
                        Title = "Title " + Guid.NewGuid().ToString().Substring(0, 5),
                        Description = "Description 2 " + Guid.NewGuid().ToString().Substring(0, 5),
                        StartTime = DateTime.Now.AddDays(4),
                        EndTime = DateTime.Now.AddDays(11)
                    }
                },
                new EventDisplayItem
                {
                    Event = new Event
                    {
                        Title = "Title " + Guid.NewGuid().ToString().Substring(0, 5),
                        Description = "Description 3 " + Guid.NewGuid().ToString().Substring(0, 5),
                        StartTime = DateTime.Now.AddDays(1),
                        EndTime = DateTime.Now.AddDays(11)
                    }
                },
                new EventDisplayItem
                {
                    Event = new Event
                    {
                        Title = "Title " + Guid.NewGuid().ToString().Substring(0, 5),
                        Description = "Description 4 " + Guid.NewGuid().ToString().Substring(0, 5),
                        StartTime = DateTime.Now.AddDays(3),
                        EndTime = DateTime.Now.AddDays(11)
                    }
                },
                new EventDisplayItem
                {
                    Event = new Event
                    {
                        Title = "Title " + Guid.NewGuid().ToString().Substring(0, 5),
                        Description = "Description 5 " + Guid.NewGuid().ToString().Substring(0, 5),
                        StartTime = DateTime.Now.AddDays(3),
                        EndTime = DateTime.Now.AddDays(11)
                    }
                },
                new EventDisplayItem
                {
                    Event = new Event
                    {
                        Title = "Title " + Guid.NewGuid().ToString().Substring(0, 5),
                        Description = "Description 5 " + Guid.NewGuid().ToString().Substring(0, 5),
                        StartTime = DateTime.Now.AddDays(6),
                        EndTime = DateTime.Now.AddDays(6)
                    }
                },
                new EventDisplayItem
                {
                    Event = new Event
                    {
                        Title = "Title " + Guid.NewGuid().ToString().Substring(0, 5),
                        Description = "Description 6 " + Guid.NewGuid().ToString().Substring(0, 5),
                        StartTime = DateTime.Now.AddDays(6),
                        EndTime = DateTime.Now.AddDays(6)
                    }
                }
            };
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
