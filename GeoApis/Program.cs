
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

            string[] proxyList = ProxyHelper.GetProxyArray();

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



        private static string GetRequest(string url)
        {
            string resp = null;
            string data = null;

            string[] proxyList = ProxyHelper.GetProxyArray();

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
                    resp = wc.DownloadString(url);
                    sw.Stop();

                    System.Console.WriteLine(System.Environment.NewLine);
                    System.Console.WriteLine(new string('/', System.Console.WindowWidth));

                    System.Console.WriteLine("Elapsed: " + sw.Elapsed.TotalSeconds.ToString());
                    System.Console.WriteLine(proxy);
                    System.Console.WriteLine(new string('/', System.Console.WindowWidth));
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


        // clientId=&signature=
        public static string Sign(string url, string keyString)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

            // converting key to bytes will throw an exception, need to replace '-' and '_' characters first.
            string usablePrivateKey = keyString.Replace("-", "+").Replace("_", "/");
            byte[] privateKeyBytes = System.Convert.FromBase64String(usablePrivateKey);

            System.Uri uri = new System.Uri(url);
            byte[] encodedPathAndQueryBytes = encoding.GetBytes(uri.LocalPath + uri.Query);

            // compute the hash
            System.Security.Cryptography.HMACSHA1 algorithm = new System.Security.Cryptography.HMACSHA1(privateKeyBytes);
            byte[] hash = algorithm.ComputeHash(encodedPathAndQueryBytes);

            // convert the bytes to string and make url-safe by replacing '+' and '/' characters
            string signature = System.Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_");

            // Add the signature to the existing URI.
            return uri.Scheme + "://" + uri.Host + uri.LocalPath + uri.Query + "&signature=" + signature;
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

            if (ls == null || ls.Count < 1)
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



        // GeoCode("Châtel-Saint-Denis, FR, Schweiz");
        public static Wgs84Coordinates YandexGeoCode(string address)
        {
            // https://geocode-maps.yandex.ru/1.x/?geocode=Jetzgrad,%20A.Marsch&lang=en_US&format=json
            // https://geocode-maps.yandex.ru/1.x/?geocode=Moscow+Lva+Tolstogo+Street+16&lang=en_US&format=json

            // address = System.Web.HttpUtility.UrlEncode(address);
            // address = System.Uri.EscapeDataString(address);
            // address = System.Uri.EscapeUriString(address);
            address = CommaEncode(address);

#if HAVE_NO_API_KEY
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}"; ;
#else
            string YOUR_API_KEY = TestPlotly.SecretManager.GetSecret<string>("GoogleGeoCodingApiKey");
            string url = $"https://geocode-maps.yandex.ru/1.x/?geocode={address}&lang=en_US&format=json"; // &apikey={YOUR_API_KEY}";
#endif

            string resp = GetRequest(url);
            // System.IO.File.WriteAllText(@"D:\username\Documents\visual studio 2017\Projects\TestPlotly\TestSpatial\geoCodeResponse.json", resp, System.Text.Encoding.UTF8);
            // string resp = System.IO.File.ReadAllText(@"D:\username\Documents\visual studio 2017\Projects\TestPlotly\TestSpatial\geoCodeResponse.json", System.Text.Encoding.UTF8);

            System.IO.File.WriteAllText(@"D:\yand.json", resp, System.Text.Encoding.UTF8);
            Yandex.RootNode rn = GeoApis.Yandex.RootNode.FromJson(resp);



            System.Console.WriteLine(rn);

            if (rn.Response.GeoObjectCollection.MetaDataProperty.GeocoderResponseMetaData.Found > 0)
            {
                System.Console.WriteLine(rn.Response.GeoObjectCollection.FeatureMember[0].GeoObject.Point.GoogleCoords);
                Yandex.Point pnt = rn.Response.GeoObjectCollection.FeatureMember[0].GeoObject.Point;

                return new Wgs84Coordinates(pnt.Latitude, pnt.Longitude);
            }

            return null;
        }


        public static void UpdateBuildingsWithYandex()
        {
            using (System.Data.IDbCommand cmd = SQL.fromFile("GetGBGeocode.sql"))
            {
                using (System.Data.DataTable dt = SQL.GetDataTable(cmd))
                {
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        System.Threading.Thread.Sleep(14000);

                        try
                        {
                            string uid = System.Convert.ToString(dr["GB_UID"]);
                            string geocodeName = System.Convert.ToString(dr["GB_Adresse"]);
                            // geocodeName = "Zürichstrasse 130, 8600 Dübendorf";
                            // geocodeName = "Bern Helvetiastrasse 37 Switzerland";

                            System.Console.WriteLine("Geocoding " + geocodeName + "(" + uid + ")");
                            Wgs84Coordinates coords = YandexGeoCode(geocodeName);

                            if (coords == null)
                                continue;

                            string str = coords.ToString();
                            System.Console.WriteLine(str);

                            using (System.Data.IDbCommand cmdUpdate = SQL.CreateCommand(@"
UPDATE T_AP_Gebaeude 
	SET  GB_GM_Lat = @lat 
		,GB_GM_Lng = @lng 
WHERE GB_UID = @gb_uid 
;
"))
                            {
                                SQL.AddParameter(cmdUpdate, "gb_uid", uid);
                                SQL.AddParameter(cmdUpdate, "lat", coords.Latitude);
                                SQL.AddParameter(cmdUpdate, "lng", coords.Longitude);

                                SQL.ExecuteNonQuery(cmdUpdate);
                            } // End Using cmdUpdate 

                        } // End Try 
                        catch (System.Exception ex)
                        {
                            System.Console.WriteLine(ex.Message);
                        }

                    } // Next dr 

                } // End Using dt 

            } // End Using cmd 

        } // End Sub UpdateBuildingsWithYandex 





        static bool isClockwise(LatLng[] poly)
        {
            decimal sum = 0;

            for (int i = 0; i < poly.Length - 1; i++)
            {
                LatLng cur = poly[i], next = poly[i + 1];
                sum += (next.lat - cur.lat) * (next.lng + cur.lng);
            } // Next i 

            return sum > 0;
        } // End Function isClockwise 


        // MSSQL is CLOCKWISE (MS-SQL wants the polygon points in clockwise sequence) 
        static LatLng[] toClockWise(LatLng[] poly)
        {
            if (!isClockwise(poly))
                poly.Reverse();

            return poly;
        } // End Function toClockWise 


        // OSM is COUNTER-clockwise  (OSM wants the polygon points in counterclockwise sequence) 
        static LatLng[] toCounterClockWise(LatLng[] poly)
        {
            if (isClockwise(poly))
                poly.Reverse();

            return poly;
        } // End Function toCounterClockWise 


        public static string CreatePolygon(LatLng[] latLongs)
        {
            //POLYGON ((73.232821 34.191819,73.233755 34.191942,73.233653 34.192358,73.232843 34.192246,73.23269 34.191969,73.232821 34.191819))
            string polyString = "";

            // MS-SQL polygon absolutely wants to be clockwise...
            // Don't copy array, just switch direction if necessary 
            if (isClockwise(latLongs))
            {
                for (int i = 0; i < latLongs.Length; ++i)
                {
                    if (i != 0)
                        polyString += ",";

                    polyString += latLongs[i].lng + " " + latLongs[i].lat; // + ",";
                } // Next i 
            }
            else
            {
                for (int i = latLongs.Length - 1; i > -1; --i)
                {
                    if (i != latLongs.Length - 1)
                        polyString += ",";

                    polyString += latLongs[i].lng + " " + latLongs[i].lat; // + ",";
                } // Next i 
            }

            polyString = "POLYGON((" + polyString + "))";
            return polyString;
        } // End Function CreatePolygon 


        public static string CreateSqlPolygon(LatLng[] latLongs)
        {
            string s = "geography::STPolyFromText('" + CreatePolygon(latLongs) + "', 4326)";
            return s;
        } // End Function CreateSqlPolygon 


        private static System.Random rand = new System.Random();


        public static Polygon GetNearestBuildingPolygon(decimal latitide, decimal longitude)
        {
            OpenToolkit.Mathematics.DecimalVector2 geoPoint = new OpenToolkit.Mathematics.DecimalVector2(latitide, longitude);

            LatLngBounds bounds = LatLngBounds.FromPoint(new LatLng(latitide, longitude), 1000); // d.h. Radius = 500m

            decimal area = bounds.BoundsArea;
            if (area > 0.25m)
            {
                System.Console.WriteLine("The maximum bbox size is 0.25, and your request was too large.\nEither request a smaller area, or use planet.osm.");
                return null;
            } // End if (area > 0.25m) 


            string xml = null;
#if fromFile
            xml = System.IO.File.ReadAllText(@"D:\Stefan.Steiger\Desktop\map.osm.xml", System.Text.Encoding.UTF8);
#else
            const string OSM_API_VERSION = "0.6";
            // string url = "https://www.openstreetmap.org/api/0.6/map?bbox=8.626273870468141,47.69679769756054,8.636573553085329,47.700530864557194&no_cache=1562588642802";
            string url = "https://www.openstreetmap.org/api/" + OSM_API_VERSION + "/map?bbox=" + bounds.ToBBoxString();

            string[] proxyList = ProxyHelper.GetProxyArray();
            string proxy = proxyList[s_rnd.Next(0, proxyList.Length)];


            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                // wc.Proxy = new System.Net.WebProxy(proxy);

                xml = wc.DownloadString(url);
            } // End Using wc 
#endif

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(xml);

            System.Xml.XmlNodeList nodes = doc.SelectNodes("//node");


            System.Collections.Generic.Dictionary<string, LatLng> nodeDictionary =
                new System.Collections.Generic.Dictionary<string, LatLng>(System.StringComparer.InvariantCultureIgnoreCase);

            System.Collections.Generic.Dictionary<string, Polygon> buildingPolygonDictionary =
                new System.Collections.Generic.Dictionary<string, Polygon>(System.StringComparer.InvariantCultureIgnoreCase);


            foreach (System.Xml.XmlElement node in nodes)
            {
                string id = node.GetAttribute("id");
                string nodeLat = node.GetAttribute("lat");
                string nodeLong = node.GetAttribute("lon");

                decimal dlat = 0;
                decimal dlong = 0;
                decimal.TryParse(nodeLat, out dlat);
                decimal.TryParse(nodeLong, out dlong);

                nodeDictionary[id] = new LatLng(dlat, dlong);
            } // Next node 


            // https://stackoverflow.com/questions/1457638/xpath-get-nodes-where-child-node-contains-an-attribute
            // querySelectorAll('way tag[k="building"]')
            System.Xml.XmlNodeList buildings = doc.SelectNodes("//way[tag/@k=\"building\"]");
            foreach (System.Xml.XmlElement building in buildings)
            {
                System.Collections.Generic.List<LatLng> lsPolygonPoints = new System.Collections.Generic.List<LatLng>();

                System.Xml.XmlNodeList buildingNodes = building.SelectNodes("./nd");
                foreach (System.Xml.XmlElement buildingNode in buildingNodes)
                {
                    string reff = buildingNode.GetAttribute("ref");
                    lsPolygonPoints.Add(nodeDictionary[reff]);
                } // Next buildingNode 

                LatLng[] polygonPoints = toCounterClockWise(lsPolygonPoints.ToArray());
                string id = building.GetAttribute("id");

                Polygon poly = new Polygon(polygonPoints);
                poly.OsmId = id;

                buildingPolygonDictionary[id] = poly;
            } // Next building 

            // System.Console.WriteLine(buildingPolygonDictionary);



            decimal? min = null;
            string uid = null;

            foreach (System.Collections.Generic.KeyValuePair<string, Polygon> kvp in buildingPolygonDictionary)
            {
                decimal minDist = kvp.Value.GetMinimumDistance(geoPoint);

                if (min.HasValue)
                {
                    if (minDist < min.Value)
                    {
                        min = minDist;
                        uid = kvp.Key;
                    } // End if (minDist < min.Value)
                }
                else
                {
                    uid = kvp.Key;
                    min = minDist;
                }

            } // Next kvp 

            return buildingPolygonDictionary[uid];
        } // End Sub 




        static void GetAndInsertBuildingPolygon()
        {
            string sql = @"
INSERT INTO T_ZO_Objekt_Wgs84Polygon
(
	 ZO_OBJ_WGS84_UID
	,ZO_OBJ_WGS84_GB_UID
	,ZO_OBJ_WGS84_SO_UID
	,ZO_OBJ_WGS84_Sort
	,ZO_OBJ_WGS84_GM_Lat
	,ZO_OBJ_WGS84_GM_Lng
)
SELECT 
	 NEWID() ZO_OBJ_WGS84_UID -- uniqueidentifier
	,@gb_uid AS ZO_OBJ_WGS84_GB_UID -- uniqueidentifier
	,NULL AS ZO_OBJ_WGS84_SO_UID -- uniqueidentifier
	,@i ZO_OBJ_WGS84_Sort -- int
	,@lat ZO_OBJ_WGS84_GM_Lat -- decimal(23,20)
	,@lng ZO_OBJ_WGS84_GM_Lng -- decimal(23,20)
; 
";

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();


            using (System.Data.Common.DbCommand cmdList = SQL.fromFile("GetGbOsmPolygon.sql"))
            {

                using (System.Data.DataTable dt = SQL.GetDataTable(cmdList))
                {
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        string gb_uid = System.Convert.ToString(dr["GB_UID"]);
                        decimal latitude = System.Convert.ToDecimal(dr["GB_GM_Lat"]);
                        decimal longitude = System.Convert.ToDecimal(dr["GB_GM_Lng"]);

                        System.Threading.Thread.Sleep(4000);

                        Polygon nearestBuilding = GetNearestBuildingPolygon(latitude, longitude);
                        if (nearestBuilding == null)
                            continue;
                        System.Console.WriteLine(nearestBuilding);
                        System.Console.WriteLine(nearestBuilding.OsmId); // 218003784

                        LatLng[] msPoints = nearestBuilding.ToClockWiseLatLngPoints();
                        string createPolygon = CreateSqlPolygon(msPoints);
                        System.Console.WriteLine(sql);
                        //                      sql = @"
                        //SELECT 
                        //	 geography::STPolyFromText('POLYGON((7.7867531 46.9361500,7.7869622 46.9361188,7.7869515 46.9360856,7.7869952 46.9360793,7.7870059 46.9361123,7.7870300 46.9361087,7.7870312 46.9361124,7.7870944 46.9361028,7.7870933 46.9360991,7.7872340 46.9360778,7.7873147 46.9363299,7.7871740 46.9363510,7.7871728 46.9363473,7.7871099 46.9363568,7.7871110 46.9363605,7.7868341 46.9364021,7.7867531 46.9361500))', 4326)
                        //	,geometry::STPolyFromText('POLYGON((7.7867531 46.9361500,7.7869622 46.9361188,7.7869515 46.9360856,7.7869952 46.9360793,7.7870059 46.9361123,7.7870300 46.9361087,7.7870312 46.9361124,7.7870944 46.9361028,7.7870933 46.9360991,7.7872340 46.9360778,7.7873147 46.9363299,7.7871740 46.9363510,7.7871728 46.9363473,7.7871099 46.9363568,7.7871110 46.9363605,7.7868341 46.9364021,7.7867531 46.9361500))', 4326)

                        //	-- Geometry is BAD for area
                        //	,geography::STPolyFromText('POLYGON((7.7867531 46.9361500,7.7869622 46.9361188,7.7869515 46.9360856,7.7869952 46.9360793,7.7870059 46.9361123,7.7870300 46.9361087,7.7870312 46.9361124,7.7870944 46.9361028,7.7870933 46.9360991,7.7872340 46.9360778,7.7873147 46.9363299,7.7871740 46.9363510,7.7871728 46.9363473,7.7871099 46.9363568,7.7871110 46.9363605,7.7868341 46.9364021,7.7867531 46.9361500))', 4326).STArea() AS geogArea 
                        //	,geometry::STPolyFromText('POLYGON((7.7867531 46.9361500,7.7869622 46.9361188,7.7869515 46.9360856,7.7869952 46.9360793,7.7870059 46.9361123,7.7870300 46.9361087,7.7870312 46.9361124,7.7870944 46.9361028,7.7870933 46.9360991,7.7872340 46.9360778,7.7873147 46.9363299,7.7871740 46.9363510,7.7871728 46.9363473,7.7871099 46.9363568,7.7871110 46.9363605,7.7868341 46.9364021,7.7867531 46.9361500))', 4326).STArea() AS geomArea 
                        //";



                        LatLng[] osmPoints = nearestBuilding.ToCounterClockWiseLatLngPoints();

                        string sql2 = "DELETE FROM T_ZO_Objekt_Wgs84Polygon WHERE ZO_OBJ_WGS84_GB_UID = @gb_uid; ";

                        using (System.Data.Common.DbCommand cmd = SQL.CreateCommand(sql2))
                        {
                            SQL.AddParameter(cmd, "gb_uid", gb_uid);
                            SQL.ExecuteNonQuery(cmd);
                        } // End Using cmd 

                        for (int i = 0; i < osmPoints.Length; ++i)
                        {
                            using (System.Data.Common.DbCommand cmd = SQL.CreateCommand(sql))
                            {
                                SQL.AddParameter(cmd, "gb_uid", gb_uid);
                                SQL.AddParameter(cmd, "i", i);
                                SQL.AddParameter(cmd, "lat", osmPoints[i].lat);
                                SQL.AddParameter(cmd, "lng", osmPoints[i].lng);

                                SQL.ExecuteNonQuery(cmd);
                            } // End Using cmd 

                        } // Next i 

                        System.Console.WriteLine(sql);
                    } // Next dr 

                } // End Using dt 

            } // End using cmd 

        } // End Sub 
        

        static void Main(string[] args)
        {
            // ProxyHelper.GetProxyList("proxylist2.htm", "proxyList2.json");


            GetAndInsertBuildingPolygon();

            


            // UpdateBuildingsWithYandex();

            GeoApis.Polygon poly = new Polygon();
            poly.PopulateV1();

            System.Console.WriteLine(poly.MathematicalArea);
            System.Console.WriteLine(poly.Centroid);
            System.Console.WriteLine(poly.Midpoint);



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
                    System.Data.IDbDataParameter pgem = SQL.AddParameter(cmd, "gem_nr", "12435");
                    System.Data.IDbDataParameter plat = SQL.AddParameter(cmd, "lat", "666");
                    System.Data.IDbDataParameter plng = SQL.AddParameter(cmd, "lng", "666");

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
        } // End Sub Main 


    } // End Class 


} // End Namespace 
