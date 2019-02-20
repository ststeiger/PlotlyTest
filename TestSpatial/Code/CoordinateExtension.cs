
namespace TestSpatial 
{


    public static class CoordinateExtension 
    {


        public static DotSpatial.Topology.Coordinate FromWgs84(double lat, double lon) //, int zoom)
        {
            DotSpatial.Topology.Coordinate coord = new DotSpatial.Topology.Coordinate();

            coord.X = (lon + 180.0) / 360.0; // * System.Math.Pow(2, zoom);
            coord.Y =
                    (1 - System.Math.Log(
                         System.Math.Tan(lat * System.Math.PI / 180)
                         + 1 / System.Math.Cos(lat * System.Math.PI / 180)
                        )
                        / System.Math.PI
                     ) / 2
            // * System.Math.Pow(2, zoom)
            ;

            coord.Z = 0;
            // coord.Z = zoom;
            return coord;
        } // End Function FromWgs84 


        public static Wgs84Coordinates ToWgs84(double x, double y) //, int z)
        {
            Wgs84Coordinates coord = new Wgs84Coordinates();
            // coord.ZoomLevel = z;

            // coord.Longitude = (decimal)(x / System.Math.Pow(2, z) * 360 - 180);
            coord.Longitude =(decimal)( x * 360.0 - 180.0);

            //double n = System.Math.PI - 2 * System.Math.PI * y / System.Math.Pow(2, z);
            double n = System.Math.PI - 2 * System.Math.PI * y;

            coord.Latitude = (System.Decimal)(180.0 / System.Math.PI * System.Math.Atan(0.5 *
                    (System.Math.Exp(n) - System.Math.Exp(-n))
                ))
            ;

            return coord;
        } // End Function ToWgs84 


        public static DotSpatial.Topology.Coordinate FromWgs84(this Wgs84Coordinates coords) //, int zoom)
        {
            return FromWgs84((double)coords.Latitude, (double)coords.Longitude); //, int zoom)
        }


        public static Wgs84Coordinates ToWgs84(this DotSpatial.Topology.Coordinate coord) //, int z)
        {
            return ToWgs84(coord.X, coord.Y);
        } // End Function ToWgs84 


        public static DotSpatial.Topology.Coordinate[] ToNetTopologyCoordinates(this Wgs84Coordinates[] coords) //, int z)
        {
            DotSpatial.Topology.Coordinate[] coordinates = new DotSpatial.Topology.Coordinate[coords.Length];

            for (int i = 0; i < coords.Length; ++i)
            {
                // coordinates[i] = FromWgs84(coords[i]);
                coordinates[i] = new DotSpatial.Topology.Coordinate((double)coords[i].Latitude, (double)coords[i].Longitude, 0.0);
            }

            return coordinates;
        }


    } // End Class CoordinateExtension 


} // End Namespace TestTransform 
