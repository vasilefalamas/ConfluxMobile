using System.Linq;
using Windows.Networking.Connectivity;

namespace Conflux.Connectivity
{
    public class NetworkManager
    {
        /// <summary>
        /// Returns true if the device is connection to the internet, or false, otherwise.
        /// </summary>
        public static bool HasInternetAccess
        {
            get { return GetNetworkConnectionStatus(); }
        }

        private static bool GetNetworkConnectionStatus()
        {
            var connectionProfiles = NetworkInformation.GetConnectionProfiles();

            return connectionProfiles.Any(profile => profile.GetNetworkConnectivityLevel() != NetworkConnectivityLevel.None);
        }
    }
}
