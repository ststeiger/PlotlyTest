
using TestPlotly;

namespace GeoApis
{

    internal class WebClientWithCustomTimeout : System.Net.WebClient
    {
        protected override System.Net.WebRequest GetWebRequest(System.Uri uri)
        {
            System.Net.WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 5 * 60 * 1000;
            return w;
        }
    }


    class Program
    {
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

        private static string PostRequest(string url)
        {
            System.Collections.Specialized.NameValueCollection parameters = new System.Collections.Specialized.NameValueCollection();
            return PostRequest(url, parameters);
        }


        private static string CommaEncode(string address)
        {
            if (string.IsNullOrEmpty(address))
                return address;

            string[] components = address.Split(',');
            for (int i = 0; i < components.Length; ++i)
            {
                components[i] = System.Web.HttpUtility.UrlEncode(components[i]);
            }

            string retValue = string.Join(',', components);
            return retValue;
        }


        // GeoCode("Châtel-Saint-Denis, FR, Schweiz");
        public static Wgs84Coordinates GeoCode(string address)
        {
            // https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=YOUR_API_KEY
            // address = System.Web.HttpUtility.UrlEncode(address);
            // address = System.Uri.EscapeDataString(address);
            // address = System.Uri.EscapeUriString(address);
            address = CommaEncode(address);
            System.Console.WriteLine(address);


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
                if (adr.ErrorMessage != null)
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


        public static Wgs84Coordinates OsmGeoCode(string city, string state, string country)
        {
            city = System.Uri.EscapeDataString(city);
            state = System.Uri.EscapeDataString(state);
            country = System.Uri.EscapeDataString(country);
            // street=<housenumber> <streetname>
            // postalcode=<postalcode>
            
            // https://nominatim.org/release-docs/develop/api/Search/
            // https://nominatim.openstreetmap.org/search?format=json&city=Staufen&state=Aargau&country=Switzerland
            string url = $"https://nominatim.openstreetmap.org/search?format=json&city={city}&state={state}&country={country}";

            
            
            
            string resp = PostRequest(url);
            System.Collections.Generic.List<OSM.Geocoder.Nominatim> ls = OSM.Geocoder.Nominatim.FromJson(resp);
            
            if(ls== null || ls.Count < 1)
                throw new System.Exception("Could not geocode said address.");

            return new Wgs84Coordinates(ls[0].Lat, ls[0].Lon, ls[0].Bounds.MinimumLatitude.Value, ls[0].Bounds.MinimumLongitude.Value
                , ls[0].Bounds.MaximumLatitude.Value, ls[0].Bounds.MaximumLongitude.Value);
        } // End Function OsmGeoCode 


        static void Directions(string origin, string destination, string transitMode)
        {
            // http://maps.googleapis.com/maps/api/directions/outputFormat?parameters
            // &origin = 47.378141,8.540168
            // &destination = 47.551635,9.226241
            // &transit_mode = rail
            
#if HAVE_NO_API_KEY
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}"; ;
#else
            string api_key = TestPlotly.SecretManager.GetSecret<string>("GoogleGeoCodingApiKey");
            // string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={YOUR_API_KEY}";
            string url = $"http://maps.googleapis.com/maps/api/directions/json?key={api_key}&origin={origin}&destination={destination}&transit_mode={transitMode}";
#endif
            string response = PostRequest(url);
            System.Console.WriteLine(response);
        }


        static void Main(string[] args)
        {
            string origin = "47.378141,8.540168"; // Zürich HB
            string destination = "47.551635,9.226241"; // Erlen
            string transit_mode = "rail";
            
            Directions(origin, destination, transit_mode);
            
            
            string sourceXML = @"info/steuern/gemeinden.svg";
            string targetXML = @"info/steuern/gemeinden.min.svg";
            string newSourceXML = @"info/steuern/gemeinden.minimax.svg";

            XmlMinifierBeautifier.Minify(sourceXML, targetXML);
            XmlMinifierBeautifier.Prettify(targetXML, newSourceXML);
            
            
            using (System.Data.IDbCommand cmd = SQL.CreateCommand(@"
SELECT 
	 gemeindenummer AS gemeinde_nummer 
    ,gemeinde 
    ,kanton 
    ,gemeinde  + ', ' + kanton + ', Schweiz' AS gemeinde_adresse 
FROM __Steuern_2014 
WHERE (1=1) 
AND latitude IS NULL 
AND longitude IS NULL 

ORDER BY kanton, gemeinde 
"))
            {
                cmd.CommandText = @"
SELECT 
	 __Steuern_2016.gemeindenummer AS gemeinde_nummer 
    ,__Steuern_2016.gemeinde 
    ,__Steuern_2016.kanton 
    ,__Steuern_2016.gemeinde  
    + ', ' 
    + __Steuern_2016.kanton 
    + ', Schweiz' AS gemeinde_adresse 
FROM TestDb.dbo.__Steuern_2016 
LEFT JOIN TestDb.dbo.__Steuern_2014 ON __Steuern_2016.gemeindenummer = __Steuern_2014.gemeindenummer 
WHERE __Steuern_2014.gemeindenummer IS NULL 
";
                
                using (System.Data.DataTable dt = SQL.GetDataTable(cmd))
                {
                    cmd.CommandText = "UPDATE __Steuern_2016 SET latitude = @lat, longitude = @lng WHERE gemeindenummer = @gem_nr; ";
                    var pgem = SQL.AddParameter(cmd, "gem_nr", "12435");
                    var plat = SQL.AddParameter(cmd, "lat", "666");
                    var plng = SQL.AddParameter(cmd, "lng", "666");

                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        string id = System.Convert.ToString(dr["gemeinde_nummer"]);
                        string city = System.Convert.ToString(dr["gemeinde"]);
                        string state = System.Convert.ToString(dr["kanton"]);
                        string geocodeName = System.Convert.ToString(dr["gemeinde_adresse"]);
                        
                        System.Console.WriteLine(geocodeName);
                        Wgs84Coordinates gc = GeoCode(geocodeName);
                        // Wgs84Coordinates gc = OsmGeoCode(city, state, "Switzerland");
                        // System.Console.WriteLine(gc);

                        pgem.Value = id;
                        plat.Value = gc.Latitude;
                        plng.Value = gc.Longitude;
                        
                        SQL.ExecuteNonQuery(cmd);
                        System.Threading.Thread.Sleep(1000);
                    } // Next dr 
                    
                } // End Using dt 
                
            } // End using cmd 
            
            
            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        }
        
        
    }
    
    
}
