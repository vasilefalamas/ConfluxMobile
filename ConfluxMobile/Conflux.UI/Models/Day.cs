using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Conflux.UI.Models
{
    public class Day : INotifyPropertyChanged
    {
        private string dayValue;

        private SolidColorBrush color;

        public string Value
        {
            get
            {
                return dayValue;
            }
            set
            {
                dayValue = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                OnPropertyChanged();
            }
        }

        public bool IsActive { get; set; }

        public Day(string dayValue, Color color)
        {
            Value = dayValue;
            Color = new SolidColorBrush(color);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
