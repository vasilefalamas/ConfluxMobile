using Conflux.Core.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Conflux.UI.Models
{
    public class Week : INotifyPropertyChanged
    {
        private string monthPeriod;

        public string MonthPeriod
        {
            get
            {
                return monthPeriod;
            }
            set
            {
                monthPeriod = value;
                OnPropertyChanged();
            }
        }

        private string daysPeriod;
        
        public string DaysPeriod
        {
            get
            {
                return daysPeriod;
            }
            set
            {
                daysPeriod = value;
                OnPropertyChanged();
            }
        }

        private bool isCompact;

        public bool IsCompact
        {
            get
            {
                return isCompact;
            }
            set
            {
                isCompact = value;
                OnPropertyChanged();
            }
        }

        private List<EventDisplayItem> events;

        public List<EventDisplayItem> Events
        {
            get { return events; }
            set
            {
                events = value;
                OnPropertyChanged();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
