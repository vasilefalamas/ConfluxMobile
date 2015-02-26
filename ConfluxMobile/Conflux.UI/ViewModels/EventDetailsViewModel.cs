using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Conflux.Connectivity.GraphApi;

namespace Conflux.UI.ViewModels
{
    public class EventDetailsViewModel : INotifyPropertyChanged
    {
        private string id;

        private string title;

        private string description;

        private bool isDescriptionAvailable;

        private DateTime? startTime;

        private DateTime? endTime;

        private bool isMapLocationAvailable;
        
        public string Id
        {
            get
            {
                return id;
            }
            private set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            private set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get
            {
                return description;        
            }
            private set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public bool IsDescriptionAvailable
        {
            get
            {
                return isDescriptionAvailable;
            }
            private set
            {
                isDescriptionAvailable = value;
                OnPropertyChanged();
            }
        }

        public DateTime? StartTime
        {
            get
            {
                return startTime;
            }
            private set
            {
                startTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime? EndTime
        {
            get
            {
                return endTime;
            }
            private set
            {
                endTime = value;
                OnPropertyChanged();
            }
        }

        public bool IsMapLocationAvailable
        {
            get
            {
                return isMapLocationAvailable;
            }
            private set
            {
                isMapLocationAvailable = value;
                OnPropertyChanged();
            }
        }
        
        public LocationInfo Location { get; private set; }
        
        public void Build(Event eventItem)
        {
            var cleanedDescription = eventItem.Description == null ? string.Empty : eventItem.Description.Trim();

            Id = eventItem.Id;
            Title = eventItem.Title.ToUpper();
            IsDescriptionAvailable = string.IsNullOrEmpty(cleanedDescription);
            Description = cleanedDescription;
            StartTime = eventItem.StartTime;
            EndTime = eventItem.EndTime;
            IsMapLocationAvailable = eventItem.Location != null && eventItem.Location.Id != 0;
            Location = eventItem.Location;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
