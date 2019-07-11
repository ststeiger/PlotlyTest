
namespace GeoApis
{
    public class Wgs84Point : Wgs84Coordinates
    {
        public int Sort;

        public Wgs84Point(decimal pLatitude, decimal pLongitude, int pSort)
            : base(pLatitude, pLongitude)
        {
            this.Sort = pSort;
        } // End Constructor 


    } // End Class Wgs84Point 
}
