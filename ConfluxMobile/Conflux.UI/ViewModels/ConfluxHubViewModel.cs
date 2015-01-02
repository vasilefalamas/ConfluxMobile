using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media.Imaging;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;
using Conflux.Core.Models;
using Conflux.UI.Views;

namespace Conflux.UI.ViewModels
{
    public class ConfluxHubViewModel : INotifyPropertyChanged
    {
        private string _name;

        private string _location;

        private BitmapImage _profilePicture;

        private List<NavigationLink> _userSectionLinks;

        private IncrementalLoadingCollection<Event> _newestEvents;

        //////

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage ProfilePicture
        {
            get
            {
                return _profilePicture;
            }
            set
            {
                _profilePicture = value;
                OnPropertyChanged();
            }
        }

        public List<NavigationLink> UserSectionLinks
        {
            get
            {
                return _userSectionLinks;
            }
            set
            {
                _userSectionLinks = value;
                OnPropertyChanged();
            }
        }

        public IncrementalLoadingCollection<Event> NewestEvents
        {
            get
            {
                return _newestEvents;
            }
            set
            {
                _newestEvents = value;
                OnPropertyChanged();
            }
        }

        public ConfluxHubViewModel(IFacebookProvider facebookProvider, AccessToken accessToken, string location)
        {
            Location = location;

            var eventsSource = new EventsSource(facebookProvider, accessToken, location);

            NewestEvents = new IncrementalLoadingCollection<Event>(eventsSource);
            UserSectionLinks = new List<NavigationLink>();
            
            AddUserSectionLinks(UserSectionLinks);
        }

        /// <summary>
        /// Marks the event as "blacklisted" by removing events list.
        /// </summary>
        /// <param name="eventItem"></param>
        public void MarkBlacklistEvent(Event eventItem)
        {
            if (NewestEvents.Contains(eventItem))
            {
                NewestEvents.Remove(eventItem);
            }
        } 

        private void AddUserSectionLinks(IList<NavigationLink> userSectionLinks)
        {
            userSectionLinks.Add(new NavigationLink
            {
                Title = "my events",
                Destination = typeof (MyEventsPage)
            });

            userSectionLinks.Add(new NavigationLink
            {
                Title = "blacklist",
                Destination = typeof (BlacklistPage)
            });

            userSectionLinks.Add(new NavigationLink
            {
                Title = "search preferences",
                Destination = typeof (SearchPreferencesPage)
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
