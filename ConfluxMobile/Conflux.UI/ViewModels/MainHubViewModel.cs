using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media.Imaging;
using Conflux.Core.Models;
using Conflux.UI.Views;

namespace Conflux.UI.ViewModels
{
    public class MainHubViewModel : INotifyPropertyChanged
    {
        private string name;

        private string location;

        private BitmapImage profilePicture;

        private List<NavigationLink> userSectionLinks;

        private IncrementalLoadingCollection<EventDisplayItem> newestEvents;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage ProfilePicture
        {
            get
            {
                return profilePicture;
            }
            set
            {
                profilePicture = value;
                OnPropertyChanged();
            }
        }

        public List<NavigationLink> UserSectionLinks
        {
            get
            {
                return userSectionLinks;
            }
            set
            {
                userSectionLinks = value;
                OnPropertyChanged();
            }
        }

        public IncrementalLoadingCollection<EventDisplayItem> NewestEvents
        {
            get
            {
                return newestEvents;
            }
            set
            {
                newestEvents = value;
                OnPropertyChanged();
            }
        }

        public MainHubViewModel(string location)
        {
            Location = location;

            var eventsSource = new EventsSource(App.FacebookClient, location);

            NewestEvents = new IncrementalLoadingCollection<EventDisplayItem>(eventsSource);
            NewestEvents.LoadMoreItemsStarted += OnLoadMoreItemsStarted;
            NewestEvents.LoadMoreItemsCompleted += OnLoadMoreItemsCompleted;

            UserSectionLinks = new List<NavigationLink>();

            AddUserSectionLinks(UserSectionLinks);
        }
        
        /// <summary>
        /// Marks the event as "blacklisted" by removing events list.
        /// </summary>
        /// <param name="eventItem"></param>
        public void MarkBlacklistEvent(EventDisplayItem eventItem)
        {
            if (NewestEvents.Contains(eventItem))
            {
                NewestEvents.Remove(eventItem);
            }
        }

        private void AddUserSectionLinks(IList<NavigationLink> links)
        {
            links.Add(new NavigationLink
            {
                Title = "my events",
                Destination = typeof(MyEventsPage)
            });

            links.Add(new NavigationLink
            {
                Title = "blacklist",
                Destination = typeof(BlacklistPage)
            });

            links.Add(new NavigationLink
            {
                Title = "search preferences",
                Destination = typeof(SearchPreferencesPage)
            });
        }
        
        private async void OnLoadMoreItemsStarted()
        {
            var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();

            statusBar.ProgressIndicator.Text = "Searching events...";
            await statusBar.ProgressIndicator.ShowAsync();
        }
        
        private async void OnLoadMoreItemsCompleted()
        {
            var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();

            await statusBar.ProgressIndicator.HideAsync();
        }

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
