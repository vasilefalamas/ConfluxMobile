using System.ComponentModel;
using System.Runtime.CompilerServices;
using Conflux.Core.Settings;

namespace Conflux.UI.ViewModels
{
    class SearchPreferencesViewModel : INotifyPropertyChanged
    {
        public bool IsLastKnownLocationUsed
        {
            get
            {
                return AppSettings.GetLastKnownLocationUsage();
            }
            set
            {
                AppSettings.SetLastKnownLocationUsage(value);

                OnPropertyChanged();
            }
        }

        public bool IsImageDownloadEnabled
        {
            get
            {
                return AppSettings.GetAllowImagesDownloadStatus();
            }
            set
            {
                AppSettings.SetImagesDownloadAllowedStatus(value);

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
