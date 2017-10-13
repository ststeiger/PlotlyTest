
using TestPlotly;


namespace TestSpatial
{


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


        [System.STAThread]
        static void Main(string[] args)
        {
#if false
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
#endif

            // Test();
            // TestPolygonArea.Test();
            // Wgs84Coordinates coords = Helper.GeoCode("Route de Colovrex 16, 1218 Le Grand-Sacconex");

#if false
            using (System.Data.IDbCommand cmd = SQL.fromFile("GetBuildingsToGeocode.sql"))
            {
                using (System.Data.DataTable dt = SQL.GetDataTable(cmd))
                {
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        string uid = System.Convert.ToString(dr["OBJ_UID"]);
                        string geocodeName = System.Convert.ToString(dr["OBJ_StringToGeoCode"]);
                        decimal lat = System.Convert.ToDecimal(dr["OBJ_Lat"]);
                        decimal lng = System.Convert.ToDecimal(dr["OBJ_Lng"]);

                        // Helper.InsertBuildingData(uid, geocodeName);
                        Helper.InsertBuildingData(uid, geocodeName, new Wgs84Coordinates(lat, lng));
                        System.Threading.Thread.Sleep(1000);
                    } // Next dr 

                } // End Using dt 

            } // End using cmd 

#endif
             
            Helper.InsertBuildingData("A1C10E45-CA2B-4796-BCB7-931546D44667", "Bahnhofstrasse 4, 3073 Gümligen"
                ,new Wgs84Coordinates(46.93459319999999000000M, 7.50623670000000100000M)
                );


            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        }
    }
}
