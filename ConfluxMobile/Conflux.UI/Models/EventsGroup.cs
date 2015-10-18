using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Conflux.Core.Models;

namespace Conflux.UI.Models
{
    public class EventsGroup : ObservableCollection<EventDisplayItem>
    {
        public string Key { get; set; }
        
        public string WeekLabel { get; set; }

        public string DaysLabel { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public new IEnumerator<EventDisplayItem> GetEnumerator()
        {
            return base.GetEnumerator();
        }

        public void BuildDaysLabel()
        {
            DaysLabel = string.Format("{0} - {1}", StartTime.ToString("dd MM"), EndTime.ToString("dd MM"));
        }
    }
}
