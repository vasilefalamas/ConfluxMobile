using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Conflux.Core.Models;

namespace Conflux.UI.Models
{
    public class EventsGroup : ObservableCollection<EventDisplayItem>
    {
        public string Key { get; set; }

        public string ShortDay { get; set; }

        public new IEnumerator<EventDisplayItem> GetEnumerator()
        {
            return base.GetEnumerator();
        } 
    }
}
