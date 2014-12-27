using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Conflux.UI.ViewModels
{
    public class SharedInfoViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SharedInfoItemList> _items;

        public ObservableCollection<SharedInfoItemList> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public SharedInfoViewModel()
        {
            PopulateWithItems();
        }

        private void PopulateWithItems()
        {
            Items = new ObservableCollection<SharedInfoItemList>
            {
                new SharedInfoItemList
                {
                    IconUnicodeValue = "\uE13D",
                    Title = "Basic user data",
                    Details = "Includes first and last name, birthplace and profile picture."
                },
                new SharedInfoItemList
                {
                    IconUnicodeValue = "\uE125",
                    Title = "Friends list",
                    Details = "Includes their names and the events they will attend."
                },
                new SharedInfoItemList
                {
                    IconUnicodeValue = "\uE1D2",
                    Title = "Location",
                    Details = "Includes current position used to search for nearby events."
                }
            };
        }

        #region INotifiyPropertyChangedMembersImplementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class SharedInfoItemList
    {
        public string IconUnicodeValue { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }
    }
}
