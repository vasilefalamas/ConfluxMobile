﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
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


        public async Task BuildGroupsAsync(List<EventDisplayItem> events)
        {
            var groups = await GroupEvents(events);

            var dispatcher = Window.Current.Dispatcher;
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (var item in groups)
                {
                    EventsGroups.Add(item);
                }
            });
        }

        private async Task<List<EventsGroup>> GroupEvents(List<EventDisplayItem> events)
        {
            return await Task.Run(() =>
            {
                var resultGroups = new List<EventsGroup>();

                foreach (var eventItem in events)
                {
                    var startTime = eventItem.Event.StartTime;
                    var dayKey = startTime != null ? startTime.Value.ToString("dddd") : "Unknown";

                    //Event doesn't fit in any existing group. Create a new one.
                    if (resultGroups.All(g => g.Key != dayKey))
                    {
                        var newGroup = new EventsGroup { Key = dayKey };
                        newGroup.Add(eventItem);

                        resultGroups.Add(newGroup);
                    }
                    //Event can be be added to an existing group.
                    else
                    {
                        var belongingGroup = resultGroups.SingleOrDefault(group => group.Key == dayKey);
                        if (belongingGroup != null)
                        {
                            belongingGroup.Add(eventItem);
                        }
                    }
                }

                return resultGroups;
            });
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
