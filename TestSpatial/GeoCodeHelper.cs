
using TestPlotly;


namespace TestSpatial
{


    public class Wgs84Coordinates
    {
        public decimal Latitude;
        public decimal Longitude;


        public decimal MinLatitude;
        public decimal MinLongitude;

        public decimal MaxLatitude;
        public decimal MaxLongitude;

        public Wgs84Coordinates()
        { }


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
            ,decimal pMinLatitude, decimal pMinLongitude
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


    public class Wgs84Point : Wgs84Coordinates
    {
        public int Sort;

        public Wgs84Point(decimal pLatitude, decimal pLongitude, int pSort)
            : base(pLatitude, pLongitude)
        {
            this.Sort = pSort;
        } // End Constructor 


    } // End Class Wgs84Point 



    class Helper 
    {


        public static Wgs84Coordinates GeoCode(string address)
        {
            // https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=YOUR_API_KEY
            address = System.Web.HttpUtility.UrlEncode(address);

#if HAVE_NO_API_KEY 
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}"; ;
#else
            string YOUR_API_KEY = TestPlotly.SecretManager.GetSecret<string>("GoogleGeoCodingApiKey");
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={YOUR_API_KEY}";
#endif

            string resp = PostRequest(url);
            // System.IO.File.WriteAllText(@"D:\username\Documents\visual studio 2017\Projects\TestPlotly\TestSpatial\geoCodeResponse.json", resp, System.Text.Encoding.UTF8);
            // string resp = System.IO.File.ReadAllText(@"D:\username\Documents\visual studio 2017\Projects\TestPlotly\TestSpatial\geoCodeResponse.json", System.Text.Encoding.UTF8);

            Google.API.Geocode.GeoCodedAddress adr = Google.API.Geocode.GeoCodedAddress.FromJson(resp);

            if (adr != null && System.StringComparer.OrdinalIgnoreCase.Equals(adr.Status, "OVER_QUERY_LIMIT"))
            {
                if(adr.ErrorMessage != null)
                    throw new System.Exception(adr.ErrorMessage);
                else
                    throw new System.Exception("You have exceeded your daily request quota for this API.");
            } // End if (adr != null && System.StringComparer.OrdinalIgnoreCase.Equals(adr.Status, "OVER_QUERY_LIMIT"))


            if (adr == null
                || adr.Status == null
                || !System.StringComparer.OrdinalIgnoreCase.Equals(adr.Status, "OK")
                || adr.Results == null
                || adr.Results.Count < 1
                || adr.Results[0].Geometry == null
                || adr.Results[0].Geometry.Location == null
                )
            {
                throw new System.Exception("Geocode Fail");
            }

            decimal lat = adr.Results[0].Geometry.Location.Latitude;
            decimal lng = adr.Results[0].Geometry.Location.Longitude;

            if (adr.Results[0].Geometry.Bounds != null)
            {
                decimal minLat = adr.Results[0].Geometry.Bounds.Southwest.Latitude;
                decimal minLng = adr.Results[0].Geometry.Bounds.Southwest.Longitude;

                decimal maxLat = adr.Results[0].Geometry.Bounds.Northeast.Latitude;
                decimal maxLng = adr.Results[0].Geometry.Bounds.Northeast.Longitude;
                return new Wgs84Coordinates(lat, lng, minLat, minLng, maxLat, maxLng);
            }

            return new Wgs84Coordinates(lat, lng);
        }
        

        public static void ReverseGeoCode(string latlong)
        {
            // https://maps.googleapis.com/maps/api/geocode/json?latlng=40.714224,-73.961452&key=YOUR_API_KEY
            string API = "https://maps.googleapis.com/maps/api/geocode/json?latlng=";
            string url = API + System.Web.HttpUtility.UrlEncode(latlong);
        }


        public static System.Collections.Generic.List<Wgs84Point> GetWgs84PolygonPoints(int distance, decimal latitude, decimal longitude)
        {
            string[] overpass_services = new string[] {
                "http://overpass.osm.ch/api/interpreter",
                "http://overpass.openstreetmap.fr/api/interpreter",
                "http://overpass-api.de/api/interpreter",
                "http://overpass.osm.rambler.ru/cgi/interpreter",
                // "https://overpass.osm.vi-di.fr/api/interpreter", // offline...
            };

            // string url = "http://overpass.osm.ch/api/interpreter";
            // string url = "http://overpass-api.de/api/interpreter";
            string url = overpass_services[s_rnd.Next(0, overpass_services.Length)];


            System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("data", GetOqlBuildingQuery(distance, latitude, longitude));

            string resp = PostRequest(url, reqparm);
            // System.IO.File.WriteAllText(@"D:\username\Documents\visual studio 2017\Projects\TestPlotly\TestSpatial\testResponse.json", resp, System.Text.Encoding.UTF8);
            // System.Console.WriteLine(resp);
            // string resp = System.IO.File.ReadAllText(@"D:\username\Documents\visual studio 2017\Projects\TestPlotly\TestSpatial\testResponse.json", System.Text.Encoding.UTF8);

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


        public static void InsertBuildingData(string uid, string geocodeName)
        {

            Wgs84Coordinates coords = Helper.GeoCode(geocodeName);


            using (System.Data.Common.DbCommand cmd = SQL.fromFile("Update_Building_Coordinates.sql"))
            {
                SQL.AddParameter(cmd, "building", uid);
                SQL.AddParameter(cmd, "lat", coords.Latitude);
                SQL.AddParameter(cmd, "lng", coords.Longitude);

                SQL.ExecuteNonQuery(cmd);
            }

            InsertBuildingData(uid, geocodeName, coords);
        }


        public static void InsertBuildingData(string uid,string geocodeName, Wgs84Coordinates coords)
        {
            System.Collections.Generic.List<Wgs84Point> ls = GetWgs84PolygonPoints(1, coords.Latitude, coords.Longitude);

            for (int i = 1; (ls == null) && i < 7; i++)
            {
                int distance = i * 5;
                System.Console.WriteLine(distance);
                ls = GetWgs84PolygonPoints(i * 5, coords.Latitude, coords.Longitude);
            }

            // Manual polygon for:
            {
                // Helper.InsertBuildingData("A1C10E45-CA2B-4796-BCB7-931546D44667", "Bahnhofstrasse 4, 3073 Gümligen");
                //ls = new System.Collections.Generic.List<Wgs84Point>();

                //ls.Add(new Wgs84Point(46.934577M, 7.505874M, 0));
                //ls.Add(new Wgs84Point(46.934637M, 7.505912M, 1));
                //ls.Add(new Wgs84Point(46.934848M, 7.505963M, 2));
                //ls.Add(new Wgs84Point(46.934770M, 7.506341M, 3));
                //ls.Add(new Wgs84Point(46.934658M, 7.506644M, 4));
                //ls.Add(new Wgs84Point(46.934318M, 7.506338M, 5));
                //ls.Add(new Wgs84Point(46.934577M, 7.505874M, 6));
            }

            if (ls == null || ls.Count < 3)
                // throw new System.Exception("No polygon data");
                return;
                


            
            using (System.Data.Common.DbCommand cmd = SQL.CreateCommand("DELETE FROM T_ZO_Objekt_Wgs84Polygon WHERE ZO_OBJ_WGS84_GB_UID = @building"))
            {
                SQL.ResetParameter(cmd, "building", uid);
                SQL.ExecuteNonQuery(cmd);
            }


            using (System.Data.Common.DbCommand cmd = SQL.fromFile("Insert_WGS84.sql"))
            {
                // SQL.AddParameter(cmd, "ZO_OBJ_WGS84_UID", System.Guid.NewGuid());
                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_GB_UID", System.Guid.NewGuid().ToString());
                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_SO_UID", System.Guid.NewGuid().ToString());

                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_Sort", ls[0].Sort);
                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_GM_Lat", ls[0].Latitude);
                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_GM_Lng", ls[0].Longitude);

                SQL.InsertList<Wgs84Point>(cmd, ls,
                    delegate (System.Data.IDbCommand cmd2, Wgs84Point p)
                    {
                        SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_SO_UID", null);
                        SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_GB_UID", uid);

                        SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_Sort", p.Sort);
                        SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_GM_Lat", p.Latitude);
                        SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_GM_Lng", p.Longitude);
                    }
                );

            } // End Using cmd 

        } // End Function GetBuildingData 


        // https://wiki.openstreetmap.org/wiki/Overpass_API/Overpass_QL
        private static string GetOqlBuildingQuery(int distance, decimal latitude, decimal longitude)
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


        private static string PostRequest(string url)
        {
            System.Collections.Specialized.NameValueCollection parameters = new System.Collections.Specialized.NameValueCollection();
            return PostRequest(url, parameters);
        }


        private class WebClientWithCustomTimeout : System.Net.WebClient
        {
            protected override System.Net.WebRequest GetWebRequest(System.Uri uri)
            {
                System.Net.WebRequest w = base.GetWebRequest(uri);
                w.Timeout = 5 * 60 * 1000;
                return w;
            }
        }


        private static System.Random s_rnd = new System.Random();

        private static string PostRequest(string url, System.Collections.Specialized.NameValueCollection parameters)
        {
            string resp = null;
            string data = null;

            string[] proxyList = new string[] {
                /*
                "http://45.79.0.108:1080",
                "http://206.127.141.67:80",
                "http://50.232.30.218:3128",
                "http://208.83.106.105:9999",
                "http://47.89.241.103:3128",
                "http://54.212.54.101:80",
                "http://52.7.233.25:80",
                "http://96.84.57.209:3128",
                "http://204.14.188.53:7004",
                "http://104.131.61.142:8118",
                "http://107.17.92.148:8080",
                "http://69.73.167.76:80",
                "http://209.141.47.120:80",
                "http://207.55.61.176:9999",
                "http://52.40.10.125:80",
                "http://18.221.211.18:3128",
                "http://54.177.186.237:80",
                "http://98.102.161.210:80",
                "http://54.202.8.138:80",
                "http://162.243.138.193:80",
                "http://192.187.124.196:3128",
                "http://96.44.148.86:80",
                "http://104.236.175.143:80",
                "http://205.158.57.2:53281",
                "http://104.196.255.174:3128",
                "http://76.75.55.225:65103",
                "http://108.165.2.110:80",
                "http://209.141.61.84:80",
                "http://64.237.61.242:80",
                "http://130.211.92.157:443",
                "http://45.55.9.179:80",
                "http://155.94.224.175:80",
                "http://74.207.225.106:80",
                "http://98.124.121.102:53281",
                "http://107.170.214.74:80",
                "http://104.131.61.119:8118",
                "http://104.236.48.178:8080"
                */



                 "http://104.236.175.143:80",
                 "http://209.141.61.84:80",

                "http://104.236.48.178:8080",
"http://143.0.189.82:80",
"http://203.146.217.107:8080",
"http://80.83.20.14:80",
"http://222.124.152.138:80",
"http://94.154.22.193:53281",
"http://152.231.81.122:53281",
"http://87.228.29.154:53281",
"http://165.84.167.54:8080",
"http://202.40.177.230:53281",
"http://202.63.242.135:53281",
"http://128.201.186.183:53005",
"http://190.152.19.190:62225",
"http://92.27.91.253:53281",
"http://114.47.66.135:8088",
"http://27.50.49.22:3128",
"http://109.161.48.228:53281",
"http://61.216.60.206:8080",
"http://143.0.188.39:80",
"http://54.177.186.237:80",
"http://117.52.91.248:80",

            };

            int repeatCount = 0;

            lol:
            string proxy = proxyList[s_rnd.Next(0, proxyList.Length)];
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();



            try
            {

                using (System.Net.WebClient wc = new WebClientWithCustomTimeout())
                {
                    wc.Headers[System.Net.HttpRequestHeader.Pragma] = "no-cache";
                    // wc.Headers[System.Net.HttpRequestHeader.Referer] = "https://overpass-turbo.eu/";
                    // wc.Headers[System.Net.HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
                    wc.Headers[System.Net.HttpRequestHeader.UserAgent] = "Lord Vishnu/Transcendental (Vaikuntha;Supreme Personality of Godness)";
                    // wc.Headers["X-Requested-With"] = "overpass-ide";


                    // wc.Headers[System.Net.HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    // string myParameters = "param1=value1&param2=value2&param3=value3";
                    // string HtmlResult = wc.UploadString(URI, myParameters);

                    // if(url.IndexOf("google") == -1) wc.Proxy = new System.Net.WebProxy(proxy);

                    sw.Start();
                    byte[] responsebytes = wc.UploadValues(url, "POST", parameters);
                    sw.Stop();

                    System.Console.WriteLine(System.Environment.NewLine);
                    System.Console.WriteLine(new string('/', System.Console.WindowWidth));
                    
                    System.Console.WriteLine("Elapsed: " + sw.Elapsed.TotalSeconds.ToString());
                    System.Console.WriteLine(proxy);
                    System.Console.WriteLine(new string('/', System.Console.WindowWidth));

                    resp = System.Text.Encoding.UTF8.GetString(responsebytes);
                } // End Using wc 
            }
            catch (System.Net.WebException ex)
            {
                sw.Stop();
                System.Console.WriteLine(System.Environment.NewLine);
                System.Console.WriteLine(new string('=', System.Console.WindowWidth));
                System.Console.WriteLine(ex.GetType().ToString());
                System.Console.WriteLine(proxy);
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine("Elapsed: " + sw.Elapsed.TotalSeconds.ToString());

                if (parameters.Count > 0)
                    data = parameters["data"];
                System.Console.WriteLine(data);
                System.Console.WriteLine(new string('=', System.Console.WindowWidth));
                repeatCount++;
                if (repeatCount > 10)
                    return null;

                goto lol;
            }
            catch (System.Exception ex)
            {
                sw.Stop();
                System.Console.WriteLine(System.Environment.NewLine);
                System.Console.WriteLine(new string('=', System.Console.WindowWidth));
                System.Console.WriteLine(ex.GetType().ToString());
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine("Elapsed: " + sw.Elapsed.TotalSeconds.ToString());

                if (parameters.Count > 0)
                    data = parameters["data"];
                System.Console.WriteLine(data);
                System.Console.WriteLine(new string('=', System.Console.WindowWidth));
            }

            return resp;
        }

    }
}
