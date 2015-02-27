using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Conflux.Connectivity.GraphApi;

namespace Conflux.UI.ViewModels
{
    public class EventDetailsViewModel : INotifyPropertyChanged
    {
        private const int ShortDescriptionWordsLength = 30;

        private string id;

        private string title;

        private bool isDescriptionAvailable;

        private bool isDescriptionTooLong;

        private string shortDescription;

        private string fullDescription;
        
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

        public bool IsDescriptionTooLong
        {
            get
            {
                return isDescriptionTooLong;
            }
            private set
            {
                isDescriptionTooLong = value;
                OnPropertyChanged();
            }
        }

        public string ShortDescription
        {
            get
            {
                return shortDescription;
            }
            private set
            {
                shortDescription = value;
                OnPropertyChanged();
            }
        }
        public string FullDescription
        {
            get
            {
                return fullDescription;
            }
            private set
            {
                fullDescription = value;
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

        public EventDetailsViewModel()
        {
            IsMapLocationAvailable = false;
        }

        public void Build(Event eventItem)
        {
            var cleanedDescription = eventItem.Description == null ? string.Empty : eventItem.Description.Trim();

            Id = eventItem.Id;
            Title = eventItem.Title.ToUpper();
            IsDescriptionAvailable = string.IsNullOrEmpty(cleanedDescription);
            ShortDescription = GetShortDescription(cleanedDescription);
            FullDescription = cleanedDescription;
            IsDescriptionTooLong = ShortDescription.Length < FullDescription.Length + 3; //Ellipsis included
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

        private string GetShortDescription(string description)
        {
            var words = description.Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (words.Count < ShortDescriptionWordsLength)
            {
                return description;
            }

            var shortenedDescription = string.Join(" ", words.Take(ShortDescriptionWordsLength));

            return string.Format("{0}...", shortenedDescription);
        }
    }
}
