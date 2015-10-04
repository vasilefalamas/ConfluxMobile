using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Conflux.UI.Models
{
    public class Week : INotifyPropertyChanged
    {
        private Day monday;
        private Day tuesday;
        private Day wednesday;
        private Day thursday;
        private Day friday;
        private Day saturday;
        private Day sunday;

        public Day Monday
        {
            get { return monday; }
            set
            {
                monday = value;
                OnPropertyChanged();
            }
        }

        public Day Tuesday
        {
            get { return tuesday; }
            set
            {
                tuesday = value;
                OnPropertyChanged();
            }
        }

        public Day Wednesday
        {
            get { return wednesday; }
            set
            {
                wednesday = value;
                OnPropertyChanged();
            }
        }

        public Day Thursday
        {
            get { return thursday; }
            set
            {
                thursday = value; 
                OnPropertyChanged();
            }
        }

        public Day Friday
        {
            get { return friday; }
            set
            {
                friday = value;
                OnPropertyChanged();
            }
        }

        public Day Saturday
        {
            get
            {
                return saturday;
            }
            set
            {
                saturday = value;
                OnPropertyChanged();
            }
        }

        public Day Sunday
        {
            get
            {
                return sunday;
            }
            set
            {
                sunday = value;
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
