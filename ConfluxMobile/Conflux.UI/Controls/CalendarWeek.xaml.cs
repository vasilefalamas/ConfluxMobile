using Conflux.Core.Models;
using Conflux.UI.Common;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Conflux.UI.Controls
{
    public sealed partial class CalendarWeek : UserControl
    {
        public CalendarWeek()
        {
            InitializeComponent();
            ((FrameworkElement)Content).DataContext = this;
        }

        public static readonly DependencyProperty IsCompactProperty = DependencyProperty.Register("IsCompact", typeof(bool), typeof(CalendarWeek), null);

        public bool IsCompact
        {
            get
            {
                return (bool)GetValue(IsCompactProperty);
            }
            set
            {
                SetDependencyPropertyValue(IsCompactProperty, value);

                if (IsCompact)
                {
                    EventsListView.StartAnimation("Expand");
                }
                else
                {
                    EventsListView.StartAnimation("Collapse");
                }
            }
        }

        public static readonly DependencyProperty MonthPeriodProperty = DependencyProperty.Register("MonthPeriod", typeof(string), typeof(CalendarWeek), null);

        public string MonthPeriod
        {
            get
            {
                return (string)GetValue(MonthPeriodProperty);
            }
            set
            {
                SetDependencyPropertyValue(MonthPeriodProperty, value);
            }
        }

        public static readonly DependencyProperty DaysPeriodProperty = DependencyProperty.Register("DaysPeriod", typeof(string), typeof(CalendarWeek), null);

        public string DaysPeriod
        {
            get
            {
                return (string)GetValue(DaysPeriodProperty);
            }
            set
            {
                SetDependencyPropertyValue(DaysPeriodProperty, value);
            }
        }

        public static readonly DependencyProperty EventsProperty = DependencyProperty.Register("Events", typeof(List<EventDisplayItem>), typeof(CalendarWeek), null);

        public List<EventDisplayItem> Events
        {
            get
            {
                return (List<EventDisplayItem>)GetValue(EventsProperty);
            }
            set
            {
                SetDependencyPropertyValue(EventsProperty, value);
            }
        }

        private event PropertyChangedEventHandler PropertyChanged;

        private void SetDependencyPropertyValue(DependencyProperty dependencyProperty, object value, [CallerMemberName] string p = null)
        {
            SetValue(dependencyProperty, value);

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

        private void OnHyperlinkClicked(object sender, RoutedEventArgs e)
        {
            IsCompact = !IsCompact;
        }
    }
}
