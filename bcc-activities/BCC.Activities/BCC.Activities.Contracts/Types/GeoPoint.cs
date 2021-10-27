using System;
namespace BCC.Activities.Contracts.Types
{

    public class GeoPoint
    {
        private double latitude;
        private double longitude;


        public double Latitude
        {
            get
            {
                return latitude;
            }

            set
            {
                latitude = value >= -90 && value <= 90 ? value : throw new ArgumentOutOfRangeException("latitude", "The latitude must be in the range [-90, 90]");
            }
        }

        public double Longitude
        {
            get
            {
                return longitude;
            }

            set
            {
                longitude = value >= -180 && value <= 180 ? value : throw new ArgumentOutOfRangeException("longitude", "The longitude must be in the range [-180, 180]");
            }
        }
    }
}
