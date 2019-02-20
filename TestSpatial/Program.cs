
using TestPlotly;


namespace TestSpatial
{


    public static class Program
    {
        private static readonly System.Globalization.NumberFormatInfo s_webNumberFormat = CreateWebNumberFormat();


        [System.STAThread]
        static void Main(string[] args)
        {
#if false
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
#endif
            TestSpatial.TestPolygonArea.Test2();
            return;

            // Test();
            // TestPolygonArea.Test();
            // Wgs84Coordinates coords = Helper.GeoCode("Route de Colovrex 16, 1218 Le Grand-Sacconex");


#if UPDATE_COUNTRY_COORDINATES

            System.Collections.Generic.List<CountryImport.CountryData> ls = CountryImport.CountryData
                .FromJsonFile(@"d:\username\documents\visual studio 2017\Projects\TestPlotly\TestSpatial\countryBoundsData.json");

            System.Console.WriteLine(ls);


            using (System.Data.Common.DbCommand cmd = SQL.fromFile("InsertCountryBounds.sql"))
            {
                SQL.AddParameter(cmd, "long", ls[0].Long);
                SQL.AddParameter(cmd, "short", ls[0].Short);

                SQL.AddParameter(cmd, "CTRP_Min_Lat", ls[0].LatMin);
                SQL.AddParameter(cmd, "CTRP_Min_Lng", ls[0].LongMin);

                SQL.AddParameter(cmd, "CTRP_MAX_Lat", ls[0].LatMax);
                SQL.AddParameter(cmd, "CTRP_MAX_Lng", ls[0].LongMax);


                SQL.InsertList<CountryImport.CountryData>(cmd, ls,
                    delegate (System.Data.IDbCommand cmd2, CountryImport.CountryData cd)
                    {
                        SQL.ResetParameter(cmd, "long", cd.Long);
                        SQL.ResetParameter(cmd, "short", cd.Short);

                        SQL.ResetParameter(cmd, "CTRP_Min_Lat", cd.LatMin);
                        SQL.ResetParameter(cmd, "CTRP_Min_Lng", cd.LongMin);

                        SQL.ResetParameter(cmd, "CTRP_MAX_Lat", cd.LatMax);
                        SQL.ResetParameter(cmd, "CTRP_MAX_Lng", cd.LongMax);
                    }
                );

            } // End Using cmd 
#endif



#if false // UPDATE_ORT_Coordinates

            using (System.Data.IDbCommand cmd = SQL.fromFile("GetLocationGeocode.sql"))
            {
                using (System.Data.DataTable dt = SQL.GetDataTable(cmd))
                {
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        string uid = System.Convert.ToString(dr["OBJ_UID"]);
                        string geocodeName = System.Convert.ToString(dr["Location"]);

                        Wgs84Coordinates coords = Helper.GeoCode(geocodeName);
                        
                        using (System.Data.IDbCommand cmd2 = SQL.fromFile("UpdateLocationGeocode.sql"))
                        {
                            SQL.AddParameter(cmd2, "obj_uid", uid);
                            SQL.AddParameter(cmd2, "lat", coords.Latitude);
                            SQL.AddParameter(cmd2, "lng", coords.Longitude);


                            SQL.AddParameter(cmd2, "latMin", coords.MinLatitude);
                            SQL.AddParameter(cmd2, "lngMin", coords.MinLongitude);

                            SQL.AddParameter(cmd2, "latMax", coords.MaxLatitude);
                            SQL.AddParameter(cmd2, "lngMax", coords.MaxLongitude);

                            SQL.ExecuteNonQuery(cmd2);
                        }

                        System.Threading.Thread.Sleep(1000);
                    } // Next dr 

                } // End Using dt 

            } // End using cmd 
#endif


#if true // Update_Building_Coordinates
            using (System.Data.IDbCommand cmd = SQL.fromFile("GetBuildingsToGeocode.sql"))
            {
                using (System.Data.DataTable dt = SQL.GetDataTable(cmd))
                {
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        string uid = System.Convert.ToString(dr["OBJ_UID"]);
                        string geocodeName = System.Convert.ToString(dr["OBJ_StringToGeoCode"]);
                        // geocodeName = "Zürichstrasse 130, 8600 Dübendorf";

                        decimal lat = System.Convert.ToDecimal(dr["OBJ_Lat"]);
                        decimal lng = System.Convert.ToDecimal(dr["OBJ_Lng"]);

                        // Helper.InsertBuildingData(uid, geocodeName);
                        Helper.InsertBuildingData(uid, geocodeName, new Wgs84Coordinates(lat, lng));
                        System.Threading.Thread.Sleep(1000);
                    } // Next dr 

                } // End Using dt 

            } // End using cmd 

#endif

#if false

            47.392058, 8.485391

            Helper.InsertBuildingData("A1C10E45-CA2B-4796-BCB7-931546D44667", "Bahnhofstrasse 4, 3073 Gümligen"
                ,new Wgs84Coordinates(46.93459319999999000000M, 7.50623670000000100000M)
                );
#endif




#if false 
            
            Helper.InsertBuildingData("9C643E00-2EA9-4D15-A354-9FDFE9D0E810", "Bahnhofstrasse 4, 3073 Gümligen"
                , new Wgs84Coordinates(47.412959M, 9.263165M)
                );

            
            using (System.Data.IDbCommand cmd = SQL.fromFile("GetBuildingsToPolygonCode.sql"))
            {
                using (System.Data.DataTable dt = SQL.GetDataTable(cmd))
                {
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        string uid = System.Convert.ToString(dr["GB_UID"]);
                        string geocodeName = ""; // System.Convert.ToString(dr["OBJ_StringToGeoCode"]);
                        decimal lat = System.Convert.ToDecimal(dr["GB_GM_Lat"]);
                        decimal lng = System.Convert.ToDecimal(dr["GB_GM_Lng"]);

                        // Helper.InsertBuildingData(uid, geocodeName);
                        Helper.InsertBuildingData(uid, geocodeName, new Wgs84Coordinates(lat, lng));
                        System.Threading.Thread.Sleep(1000);
                    } // Next dr 

                } // End Using dt 

            } // End using cmd 
#endif


            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 
        

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


    } // End Class Program 


} // End Namespace TestSpatial 
