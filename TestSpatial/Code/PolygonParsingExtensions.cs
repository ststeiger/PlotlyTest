
namespace TestSpatial
{


    public static class PolygonParsingExtensions
    {


        public static bool IsClockwise(System.Collections.Generic.List<Wgs84Coordinates> poly)
        {
            decimal sum = 0;

            for (int i = 0; i < poly.Count - 1; i++)
            {
                Wgs84Coordinates cur = poly[i], next = poly[i + 1];
                sum += (next.Latitude - cur.Latitude) * (next.Longitude + cur.Longitude);
            } // Next i 

            return sum > 0;
        } // End Function isClockwise 


        // MSSQL is CLOCKWISE (MS-SQL polygon wants the points clockwise) 
        public static System.Collections.Generic.List<Wgs84Coordinates> ToClockWise(System.Collections.Generic.List<Wgs84Coordinates> poly)
        {
            if (!IsClockwise(poly))
                poly.Reverse();

            return poly;
        } // End Function toClockWise 


        // OSM is COUNTER-clockwise
        public static System.Collections.Generic.List<Wgs84Coordinates> ToCounterClockWise(System.Collections.Generic.List<Wgs84Coordinates> poly)
        {
            if (IsClockwise(poly))
                poly.Reverse();

            return poly;
        } // End Function toCounterClockWise 


        public static Wgs84Coordinates[] PolygonStringToCoordinates(string polygonString)
        {
            System.Collections.Generic.List<Wgs84Coordinates> ls = new System.Collections.Generic.List<Wgs84Coordinates>();


            var match = System.Text.RegularExpressions.Regex.Match(polygonString, @"\s*POLYGON\s*\(\s*\(\s*(.*?)\s*\)\s*\)\s*", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (!match.Success)
                return null;

            polygonString = match.Groups[1].Value;
            polygonString = System.Text.RegularExpressions.Regex.Replace(polygonString, @"\s*,\s", ",");

            string[] allPoints = polygonString.Split(',');

            for (int i = 0; i < allPoints.Length; ++i)
            {
                string[] pointComponents = allPoints[i].Split(' ');
                ls.Add(new Wgs84Coordinates(decimal.Parse(pointComponents[1]), decimal.Parse(pointComponents[0])));
                // ls.Add(new Wgs84Coordinates(decimal.Parse(pointComponents[0]), decimal.Parse(pointComponents[1])));
            } // Next i 

            // Close Polygon 
            ls.Add(new Wgs84Coordinates(decimal.Parse(allPoints[0].Split(' ')[1]), decimal.Parse(allPoints[0].Split(' ')[0])));
            // ls.Add(new Wgs84Coordinates(decimal.Parse(allPoints[0].Split(' ')[0]), decimal.Parse(allPoints[0].Split(' ')[1])));

            ls = ToCounterClockWise(ls); // OSM is COUNTER-clockwise
            // ls = ToClockWise(ls);

            return ls.ToArray();
        } // End Function PolygonStringToCoordinates 


    }
}
