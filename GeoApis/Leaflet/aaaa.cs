
namespace GeoApis
{

    public static class ArrayExtensions
    {

        public static LatLng[] Reverse(this LatLng[] array)
        {
            LatLng[] result = new LatLng[array.Length];

            for (int i = 0; i < array.Length; ++i)
            {
                result[i] = array[array.Length - 1 - i].Clone();
            }

            return result;
        }


        public static string Join(this object[] mem, string separator)
        {
            string retValue = null;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < mem.Length; ++i)
            {
                if (i != 0)
                    sb.Append(separator);

                sb.Append(System.Convert.ToString(mem[i], System.Globalization.CultureInfo.InvariantCulture));
            } // Next i 

            retValue = sb.ToString();
            sb.Clear();
            sb = null;

            return retValue;
        } // End Function Join 


        public static string Join(this decimal[] mem, string separator)
        {
            string retValue = null;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < mem.Length; ++i)
            {
                if (i != 0)
                    sb.Append(separator); 

                sb.Append(mem[i].ToString(System.Globalization.CultureInfo.InvariantCulture)); 
            } // Next i 

            retValue = sb.ToString(); 
            sb.Clear(); 
            sb = null; 

            return retValue; 
        } // End Function Join 


    } // End Class ArrayExtensions 


    [System.Diagnostics.DebuggerDisplay("{lat} {lng}, {alt}")]
    public class LatLng
    {
        public decimal lat;
        public decimal lng;
        public decimal? alt;

        public LatLng(decimal latitude, decimal longitude)
        {
            this.lat = latitude;
            this.lng = longitude;
        }

        public LatLng()
            :this(0,0)
        { }


        public LatLng Clone()
        {
            return new LatLng(this.lat, this.lng);
        }

        public override string ToString()
        {
            return lat.ToString(System.Globalization.CultureInfo.InvariantCulture) + ", " + lng.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
        
    }


    [System.Diagnostics.DebuggerDisplay("{North} {West}, {South} {East}")]
    public class LatLngBounds
    {
        decimal North;
        decimal South;
        decimal East;
        decimal West;



        public LatLngBounds(decimal north, decimal south, decimal east, decimal west)
        {
            this.North = north;
            this.South = south;
            this.East = east;
            this.West = west;
        }


        public LatLngBounds(LatLng southWest, LatLng northEast)
            :this(northEast.lat, southWest.lat, northEast.lng, southWest.lng)
        { }

        public LatLngBounds(LatLng[] latlongs)
            :this(latlongs[0], latlongs[1])
        { }

        public LatLngBounds()
            : this(0m, 0m, 0m, 0m)
        { }


        public LatLng Center{ get { return new LatLng() { lat = (North +South)/2.0m, lng = (West+East)/2.0m }; } }

        public LatLng NorthEast { get{ return new LatLng() { lat = North, lng = East }; } }
        public LatLng NorthWest { get { return new LatLng() { lat = North, lng = West }; } }
        public LatLng SouthWest { get{ return new LatLng() { lat=South, lng = West }; } }
        public LatLng SouthEast { get { return new LatLng() { lat = South, lng = East }; } }
        

        public string ToBBoxString()
        {
            decimal[] a = new decimal[]{this.West, this.South, this.East, this.North};
            
            return a.Join(",");
        }



    }


}
