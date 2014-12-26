using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace Conflux.Core.Maps
{
    public static class LocationFinder
    {
        /// <summary>
        /// Returns information concerning the user's current location.
        /// </summary>
        /// <param name="positionAccuracy">Position accuracy level.</param>
        /// <returns>Location, containing country, city, street, longitude and latitude result.</returns>
        public static async Task<Location> GetLocationInfoAsync(PositionAccuracy positionAccuracy = PositionAccuracy.Default)
        {
            var currentCoordinates = await GetCurrentCoordinatesAsync(positionAccuracy);

            var geopoint = new Geopoint(new BasicGeoposition
            {
                Latitude = currentCoordinates.Coordinate.Latitude,
                Longitude = currentCoordinates.Coordinate.Longitude
            });

            var location = await ReverseGeocodingAsync(geopoint);

            location.Longitude = geopoint.Position.Longitude;
            location.Latitude = geopoint.Position.Latitude;

            return location;
        }

        private static async Task<Geoposition> GetCurrentCoordinatesAsync(PositionAccuracy positionAccuracy)
        {
            var geolocator = new Geolocator
            {
                DesiredAccuracy = positionAccuracy
            };

            Geoposition position = await geolocator.GetGeopositionAsync();
            
            return position;
        }

        private static async Task<Location> ReverseGeocodingAsync(Geopoint geopoint)
        {
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(geopoint);

            if (result.Status == MapLocationFinderStatus.Success)
            {
                return Location.FromAddress(result.Locations[0].Address);
            }

            return null;
        }
    }
}
