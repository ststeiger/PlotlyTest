
using TestPlotly;


namespace TestSpatial
{


    public class Wgs84Point
    {
        public decimal Lat;
        public decimal Long;
        public int Sort;

        public Wgs84Point(decimal pLatitude, decimal pLongitude, int pSort)
        {
            this.Lat = pLatitude;
            this.Long = pLongitude;
            this.Sort = pSort;
        } // End Constructor 


    } // End Class Wgs84Point 


    static class Program
    {
        private static readonly System.Globalization.NumberFormatInfo s_webNumberFormat = CreateWebNumberFormat();



        private static System.Globalization.NumberFormatInfo CreateWebNumberFormat()
        {
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo()
            {
                NumberGroupSeparator = "",
                NumberDecimalSeparator = ".",
                CurrencyGroupSeparator = "",
                CurrencyDecimalSeparator = ".",
                CurrencySymbol = ""
            };

            return nfi;
        } // End Function SetupNumberFormatInfo


        // https://wiki.openstreetmap.org/wiki/Overpass_API/Overpass_QL
        public static string GetQuery(int distance, decimal latitude, decimal longitude)
        {
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo()
            {
                NumberGroupSeparator = "",
                NumberDecimalSeparator = ".",
                CurrencyGroupSeparator = "",
                CurrencyDecimalSeparator = ".",
                CurrencySymbol = ""
            };

            // [out: json];
            // way(around:25, 47.360867, 8.534703)["building"];
            // out ids geom meta;

            string oqlQuery = @"[out:json];
way(around:" + distance.ToString(nfi) + ", "
    + latitude.ToString(nfi) + ", " + longitude.ToString(nfi)
    + @")[""building""];
out ids geom;"; // ohne meta - ist minimal

            return oqlQuery;
        }


        public static void GetBuildingData(int distance, decimal latitude, decimal longitude)
        {
            System.Collections.Generic.List<Wgs84Point> ls = null;

            // https://wiki.openstreetmap.org/wiki/Overpass_API
            string URI = "http://overpass.osm.ch/api/interpreter";
            URI = "http://overpass-api.de/api/interpreter";


            System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("data", GetQuery(distance, latitude, longitude));


            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                wc.Headers[System.Net.HttpRequestHeader.Pragma] = "no-cache";

                // wc.Headers[System.Net.HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                // string myParameters = "param1=value1&param2=value2&param3=value3";
                // string HtmlResult = wc.UploadString(URI, myParameters);

                byte[] responsebytes = wc.UploadValues(URI, "POST", reqparm);
                string resp = System.Text.Encoding.UTF8.GetString(responsebytes);
                System.IO.File.WriteAllText(@"D:\username\Documents\visual studio 2017\Projects\TestPlotly\TestSpatial\testResponse.json", resp, System.Text.Encoding.UTF8);
                System.Console.WriteLine(resp);

                ls = GetWgs84Points(resp);
            } // End Using wc 


            using (System.Data.Common.DbCommand cmd = SQL.fromFile("Insert_WGS84.sql"))
            {
                // SQL.AddParameter(cmd, "ZO_OBJ_WGS84_UID", System.Guid.NewGuid());
                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_GB_UID", System.Guid.NewGuid().ToString());
                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_SO_UID", System.Guid.NewGuid().ToString());

                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_Sort", 123);
                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_GM_Lat", ls[0].Lat);
                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_GM_Lng", ls[0].Long);

                SQL.InsertList<Wgs84Point>(cmd, ls, delegate (System.Data.IDbCommand cmd2, Wgs84Point p)
                {
                    SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_SO_UID", "87EA0418-C8FA-4F14-B28D-120D47F3F482");
                    SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_GB_UID", "39DF8C49-F877-437B-BA9A-026A052F3616");

                    SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_Sort", ls[0].Sort);
                    SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_GM_Lat", ls[0].Lat);
                    SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_GM_Lng", ls[0].Long);
                }
                );

            } // End Using cmd 


        } // End Function GetBuildingData 


        public static void GetBuildingData()
        {
            int distance = 25;
            decimal latitude = 47.360867M;
            decimal longitude = 8.534703M;

            GetBuildingData(distance, latitude, longitude);
        }


        public static System.Collections.Generic.List<Wgs84Point> GetWgs84Points(string resp)
        {
            System.Collections.Generic.List<Wgs84Point> ls = null;

            Overpass.Building.BuildingInfo ro = Overpass.Building.BuildingInfo.FromJson(resp);

            if (ro != null && ro.Elements != null && ro.Elements.Count > 0 && ro.Elements[0].Geometry != null)
            {
                ls = new System.Collections.Generic.List<Wgs84Point>();

                for (int i = 0; i < ro.Elements[0].Geometry.Count; ++i)
                {
                    ls.Add(new Wgs84Point(ro.Elements[0].Geometry[i].Latitude, ro.Elements[0].Geometry[i].Longitude, i));
                } // Next i 

            } // End if (ro != null && ro.Elements != null && ro.Elements.Count > 0 && ro.Elements[0].Geometry != null) 

            return ls;
        } // End Function GetWgs84Points 


        public static void Test()
        {
            string resp = System.IO.File.ReadAllText(@"D:\username\Documents\visual studio 2017\Projects\TestPlotly\TestSpatial\testResponse.json", System.Text.Encoding.UTF8);
            System.Collections.Generic.List<Wgs84Point> ls = GetWgs84Points(resp);
            System.Console.WriteLine(ls);
        } // End Sub Test 


        [System.STAThread]
        static void Main(string[] args)
        {
#if false
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
#endif
            // TestPolygonArea.Test();
            GetBuildingData();

            Test();
            

            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        }
    }
}
