using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Conflux.Connectivity.GraphApi;

namespace Conflux.UI.Controls
{
    public sealed partial class EventItem
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof (String), typeof (EventItem), null);
        
        public string Title
        {
            get
            {
                return (string) GetValue(TitleProperty);
            }
            set
            {
                SetDependencyPropertyValue(TitleProperty, value);
            }
        }

        public static readonly DependencyProperty StartTimeProperty = DependencyProperty.Register("StartTime", typeof(object), typeof(EventItem), null);
        
        public DateTime? StartTime
        {
            get
            {
                return (DateTime?) GetValue(StartTimeProperty);
            }
            set
            {
                SetDependencyPropertyValue(StartTimeProperty, value);
            }
        }

        public static readonly DependencyProperty EndTimeProperty = DependencyProperty.Register("EndTime", typeof(object), typeof(EventItem), null);

        public DateTime? EndTime
        {
            get
            {
                return (DateTime?)GetValue(EndTimeProperty);
            }
            set
            {
                SetDependencyPropertyValue(EndTimeProperty, value);
            }
        }

        public static readonly DependencyProperty LocationProperty = DependencyProperty.Register("Location", typeof(LocationInfo), typeof(EventItem), null);

        public LocationInfo Location
        {
            get
            {
                return (LocationInfo)GetValue(LocationProperty);
            }
            set
            {
                SetDependencyPropertyValue(LocationProperty, value);
            }
        }

        public static readonly DependencyProperty VisitedProperty = DependencyProperty.Register("Visited", typeof(bool), typeof(EventItem), null);

        public bool Visited
        {
            get
            {
                return (bool)GetValue(VisitedProperty);
            }
            set
            {
                SetDependencyPropertyValue(VisitedProperty, value);
            }
        }

        public EventItem()
        {
            InitializeComponent();
            ((FrameworkElement) Content).DataContext = this;
        }

        private void OnAddToBlacklist(object sender, RoutedEventArgs e)
        {

        }

        private event PropertyChangedEventHandler PropertyChanged;
        
        private void SetDependencyPropertyValue(DependencyProperty dependencyProperty, object value, [CallerMemberName] String p = null)
        {
            SetValue(dependencyProperty, value);

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }
    }
}
