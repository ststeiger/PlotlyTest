
namespace GeoApis
{

    public class Wgs84Coordinates
    {
        public decimal Latitude;
        public decimal Longitude;


        public decimal MinLatitude;
        public decimal MinLongitude;

        public decimal MaxLatitude;
        public decimal MaxLongitude;

        public Wgs84Coordinates(decimal pLatitude, decimal pLongitude)
        {
            this.Latitude = pLatitude;
            this.Longitude = pLongitude;
        }

        public Wgs84Coordinates(decimal pMinLatitude, decimal pMinLongitude
            , decimal pMaxLatitude, decimal pMaxLongitude)
        {
            this.MinLatitude = pMinLatitude;
            this.MinLongitude = pMinLongitude;

            this.MaxLatitude = pMaxLatitude;
            this.MaxLongitude = pMaxLongitude;
        }


        public Wgs84Coordinates(decimal pLatitude, decimal pLongitude
            , decimal pMinLatitude, decimal pMinLongitude
            , decimal pMaxLatitude, decimal pMaxLongitude)
        {
            this.Latitude = pLatitude;
            this.Longitude = pLongitude;

            this.MinLatitude = pMinLatitude;
            this.MinLongitude = pMinLongitude;

            this.MaxLatitude = pMaxLatitude;
            this.MaxLongitude = pMaxLongitude;
        }
    }

}
