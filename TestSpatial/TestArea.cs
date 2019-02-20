
using DotSpatial.Projections;
using DotSpatial.Topology;


namespace TestSpatial
{

    public static class CoordinateExtension
    {
        // var c = new DotSpatial.Topology.Coordinate(x, y, z);


        public static Coordinate FromWgs84(double lat, double lon) //, int zoom)
        {
            Coordinate coord = new Coordinate();

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
        }


        public static Wgs84Coordinates2 ToWgs84(double x, double y) //, int z)
        {
            Wgs84Coordinates2 coord = new Wgs84Coordinates2();
            // coord.ZoomLevel = z;

            // coord.Longitude = (decimal)(x / System.Math.Pow(2, z) * 360 - 180);
            coord.Longitude = x * 360.0 - 180.0;

            //double n = System.Math.PI - 2 * System.Math.PI * y / System.Math.Pow(2, z);
            double n = System.Math.PI - 2 * System.Math.PI * y;

            coord.Latitude = (double)(180.0 / System.Math.PI * System.Math.Atan(0.5 *
                    (System.Math.Exp(n) - System.Math.Exp(-n))
                ))
            ;

            return coord;
        }

        public static Wgs84Coordinates2 ToWgs84(Coordinate coord) //, int z)
        {
            return ToWgs84(coord.X, coord.Y);
        }

    }


    public class Wgs84Coordinates2
    {
        public double Latitude;
        public double Longitude;
        public int ZoomLevel;





        public Wgs84Coordinates2(double lat, double lng, int zoom)
        {
            this.Latitude = lat;
            this.Longitude = lng;
            this.ZoomLevel = zoom;
        }

        public Wgs84Coordinates2(double lat, double lng)
            : this(lat, lng, 0)
        {}

        public Wgs84Coordinates2()
            :this(0,0)
        { }


        public override string ToString()
        {
            return "( Latitude: " + this.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)
                                  + "    Longitude: " + this.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)
                                  + " )";
        }

    }


    public class TestPolygonArea
    {



        public static void foo()
        {
            // https://github.com/NetTopologySuite/NetTopologySuite/issues/139
            // https://stackoverflow.com/questions/32838720/create-polygon-from-point-collection-with-nettopologysuite

            // Coordinate[] coordinatesArray = null; // YourMethodToGetCoordinates
            // GeometryFactory geomFactory = new GeometryFactory();
            // var poly = geomFactory.CreatePolygon(coordinatesArray);

            // DotSpatial.Topology.Polygon.FromBasicGeometry

            System.Collections.Generic.List<Coordinate> ls = 
                new System.Collections.Generic.List<Coordinate>();

            var cc = CoordinateExtension.FromWgs84(0.5, 0.5);
            var wgs = CoordinateExtension.ToWgs84(cc);


            double x = 0.5;
            double y = 0.5;
            double z = 0.3;
            var c = new DotSpatial.Topology.Coordinate(x,y,z);

            var po = new DotSpatial.Topology.Polygon(ls);

            // po.Coordinates
            Polygon polygon1 = null;
            Polygon polygon2 = null;
            Polygon polygon3 = polygon1.Union(polygon2) as Polygon;
        }


        
         public Polygon TwoPieces(Polygon polygon1, Polygon polygon2, LineString line1, LineString line2)
         {
         
         
             if (polygon1.Intersects(polygon2) || polygon1.Touches(polygon2))
             {
                 return polygon1.Union(polygon2) as Polygon;
             }
             else
             {
                 var pg = GetConvexHull(line1, line2);
                 return pg.Union(polygon1).Union(polygon2) as Polygon;
             }
         }


        public Polygon GetConvexHull(LineString line1, LineString line2)
        {
            var coords1 = new System.Collections.Generic.List<Coordinate>
            {
                line1.StartPoint.Coordinate,
                line1.EndPoint.Coordinate,
                line2.StartPoint.Coordinate,
                line2.EndPoint.Coordinate,
                line1.StartPoint.Coordinate
            };
            var pg1 = new Polygon(coords1);

            var coords2 = new System.Collections.Generic.List<Coordinate>
            {
                line1.StartPoint.Coordinate,
                line1.EndPoint.Coordinate,
                line2.EndPoint.Coordinate,
                line2.StartPoint.Coordinate,
                line1.StartPoint.Coordinate
            };
            var pg2 = new Polygon(coords2);
            return pg1.Area > pg2.Area ? pg1 : pg2;
            //return line1.Union(line2).ConvexHull() as Polygon;
        }


        public static double CalculateArea(double[] latLonPoints)
        {
            // source projection is WGS1984
            // https://productforums.google.com/forum/#!msg/gec-data-discussions/FxwUP7bd59g/02tvMDD3vtEJ
            // https://epsg.io/3857
            ProjectionInfo projFrom = KnownCoordinateSystems.Geographic.World.WGS1984;

            // most complicated problem - you have to find most suitable projection
            ProjectionInfo projTo = KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone37N;
            projTo = KnownCoordinateSystems.Projected.Europe.EuropeAlbersEqualAreaConic; // 6350.9772005155683
            // projTo= KnownCoordinateSystems.Geographic.World.WGS1984; // 5.215560750019806E-07
            projTo = KnownCoordinateSystems.Projected.WorldSpheroid.EckertIVsphere; // 6377.26664171461
            projTo = KnownCoordinateSystems.Projected.World.EckertIVworld; // 6391.5626849671826
            projTo = KnownCoordinateSystems.Projected.World.CylindricalEqualAreaworld; // 6350.6506013739854
            /*
            projTo = KnownCoordinateSystems.Projected.WorldSpheroid.CylindricalEqualAreasphere; // 6377.2695087222382
            projTo = KnownCoordinateSystems.Projected.WorldSpheroid.EquidistantCylindricalsphere; // 6448.6818862780929
            projTo = KnownCoordinateSystems.Projected.World.Polyconicworld; // 8483.7701716953889
            projTo = KnownCoordinateSystems.Projected.World.EquidistantCylindricalworld; // 6463.1380225215107
            projTo = KnownCoordinateSystems.Projected.World.EquidistantConicworld; // 8197.4427198320627
            projTo = KnownCoordinateSystems.Projected.World.VanderGrintenIworld; // 6537.3942984174937
            projTo = KnownCoordinateSystems.Projected.World.WebMercator; // 6535.5119516421109
            projTo = KnownCoordinateSystems.Projected.World.Mercatorworld; // 6492.7180733950809
            projTo = KnownCoordinateSystems.Projected.SpheroidBased.Lambert2; // 9422.0631835013628
            projTo = KnownCoordinateSystems.Projected.SpheroidBased.Lambert2Wide; // 9422.0614012926817
            projTo = KnownCoordinateSystems.Projected.TransverseMercator.WGS1984lo33; // 6760.01638841012
            projTo = KnownCoordinateSystems.Projected.Europe.EuropeAlbersEqualAreaConic; // 6350.9772005155683
            projTo = KnownCoordinateSystems.Projected.UtmOther.EuropeanDatum1950UTMZone37N; // 6480.7883094931021
            */


            // ST_Area(g, false)     6379.25032051953
            // ST_Area(g, true)      6350.65051177517
            // ST_Area(g)            5.21556075001092E-07


            // prepare for ReprojectPoints (it's mutate array)
            double[] z = new double[latLonPoints.Length / 2];
            // double[] pointsArray = latLonPoints.ToArray();

            // Reproject.ReprojectPoints(latLonPoints, z, projFrom, projTo, 0, latLonPoints.Length / 2);

            // assemblying new points array to create polygon
            System.Collections.Generic.List<Coordinate> points =
                new System.Collections.Generic.List<Coordinate>(latLonPoints.Length / 2);

            for (int i = 0; i < latLonPoints.Length / 2; i++)
                points.Add(new Coordinate(latLonPoints[i * 2], latLonPoints[i * 2 + 1]));

            Polygon poly = new Polygon(points);
            return poly.Area;
        }



        // https://stackoverflow.com/questions/46159499/calculate-area-of-polygon-having-wgs-coordinates-using-dotspatial
        // pfff wrong...
        public static void Test()
        {
            // this feature can be see visually here http://www.allhx.ca/on/toronto/westmount-park-road/25/
            string feature = "-79.525542519049552,43.691278124243432 -79.525382520578987,43.691281097414787 -79.525228855617627,43.69124858593392 -79.525096151437353,43.691183664769774 -79.52472799258571,43.690927163079735 -79.525379447437814,43.690771996666641 -79.525602330675355,43.691267524226838 -79.525542519049552,43.691278124243432";
            feature = "47.3612503,8.5351944 47.3612252,8.5342631 47.3610145,8.5342755 47.3610212,8.5345227 47.3606405,8.5345451 47.3606350,8.5343411 47.3604067,8.5343545 47.3604120,8.5345623 47.3604308,8.5352457 47.3606508,8.5352328 47.3606413,8.5348784 47.3610383,8.5348551 47.3610477,8.5352063 47.3612503,8.5351944";

            string[] coordinates = feature.Split(' ');
            // System.Array.Reverse(coordinates);


            // dotspatial takes the x,y in a single array, and z in a separate array.  I'm sure there's a 
            // reason for this, but I don't know what it is.
            double[] xy = new double[coordinates.Length * 2];
            double[] z = new double[coordinates.Length];
            for (int i = 0; i < coordinates.Length; i++)
            {
                double lon = double.Parse(coordinates[i].Split(',')[0]);
                double lat = double.Parse(coordinates[i].Split(',')[1]);
                xy[i * 2] = lon;
                xy[i * 2 + 1] = lat;
                z[i] = 0;
            }

            double area = CalculateArea(xy);
            System.Console.WriteLine(area);
        }


    }
}
