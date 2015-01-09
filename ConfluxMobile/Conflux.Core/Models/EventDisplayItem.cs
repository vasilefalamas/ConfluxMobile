using System.ComponentModel;
using System.Runtime.CompilerServices;
using Conflux.Connectivity.GraphApi;

namespace Conflux.Core.Models
{
    public class EventDisplayItem : INotifyPropertyChanged
    {
        public Event Event { get; set; }

        private bool visited;

        public bool Visited
        {
            get
            {
                return visited;
            }
            set
            {
                visited = value;
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
