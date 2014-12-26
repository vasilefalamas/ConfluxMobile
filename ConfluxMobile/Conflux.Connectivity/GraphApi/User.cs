using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;

namespace Conflux.Connectivity.GraphApi
{
    public class User
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        
        public string LastName { get; set; }

        public string FullName { get; set; }

        public BitmapImage ProfilePicture { get; set; }

        public LocationInfo LocationInfo { get; set; }
    }
}
