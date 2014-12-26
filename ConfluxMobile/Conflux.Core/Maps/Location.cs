using Windows.Services.Maps;

namespace Conflux.Core.Maps
{
    public class Location
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public static Location FromAddress(MapAddress address)
        {
            return new Location
            {
                Country = address.Country,
                City = address.Town,
                Street = address.Street,
            };
        }
    }
}
