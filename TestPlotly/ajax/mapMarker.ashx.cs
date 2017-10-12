
using System.Collections.Generic;
using System.Web;


namespace TestPlotly.ajax
{


    public class OBJ
    {
        public System.Guid OBJ_UID;
        public string OBJT_Code;
        public string OBJ_Label;
        public decimal OBJ_Lat;
        public decimal OBJ_Long;
    }


    public class Column
    {
        public int index;
        public string columnName;
        public string fieldType;
    }



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


    /// <summary>
    /// Zusammenfassungsbeschreibung für mapMarker
    /// </summary>
    public class mapMarker : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            // System.Data.DataTable dt = null;
            //using (System.Data.Common.DbCommand cmd = SQL.CreateCommand("SELECT * FROM T_Benutzer"))
            //{
            //    dt = SQL.GetDataTable(cmd);
            //}

            // context.Response.Write("Hello World - RowCount: " + dt.Rows.Count.ToString());


            System.Collections.Generic.Dictionary<string, Column> dict = new Dictionary<string, Column>();


            //{
            //    "foo":{ "index":0,"columnName":"col1","fieldType":"int"}
            //    ,"bar":{ "index":1,"columnName":"col2","fieldType":"int"}
            //}


            // dict.Add("foo", new Column() { index = 0, columnName = "col1", fieldType = "int" });
            // dict.Add("bar", new Column() { index = 1, columnName = "col2", fieldType = "int" });

            // SQL.Serialize(dict, context);

            using (System.Data.Common.DbCommand cmd = SQL.fromFile("Marker_SO.sql"))
            {
                SQL.SerializeLargeDataset(cmd, context);
            } // End Using cmd 



            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(LongRunningMethod));

            object workLoad = null;

            //workLoad = new Data
            //{
            //    ID = int.Parse(context.Request.Params["ID"]),
            //    OtherData = context.Request.Params["OtherData"]
            //};

            System.Collections.Generic.List<Wgs84Point> ls = null;

            using (System.Data.Common.DbCommand cmd = SQL.fromFile("Insert_WGS84.sql"))
            {
                // SQL.AddParameter(cmd, "ZO_OBJ_WGS84_UID", System.Guid.NewGuid());
                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_GB_UID", System.Guid.NewGuid());
                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_SO_UID", System.Guid.NewGuid());

                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_Sort", 123);
                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_GM_Lat", ls[0].Lat);
                SQL.AddParameter(cmd, "ZO_OBJ_WGS84_GM_Lng", ls[0].Long);

                SQL.InsertList<Wgs84Point>(cmd, ls, delegate (System.Data.IDbCommand cmd2, Wgs84Point p)
                    {
                        SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_GB_UID", System.Guid.NewGuid());
                        SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_SO_UID", System.Guid.NewGuid());

                        SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_Sort", ls[0].Sort);
                        SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_GM_Lat", ls[0].Lat);
                        SQL.ResetParameter(cmd, "ZO_OBJ_WGS84_GM_Lng", ls[0].Long);
                    }
                );

            } // End Using cmd 



            // https://stackoverflow.com/questions/6298191/keep-processing-after-returning-a-response-to-the-client
            context.Response.Flush();
            // Prevents any other content from being sent to the browser
            context.Response.SuppressContent = true;
            context.Response.Close();


            // Directs the thread to finish, bypassing additional processing
            // System.Threading.ThreadPool.QueueUserWorkItem(new WaitCallback(LongRunningMethod), workLoad);


            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(
                delegate(object state) 
                {
                    var data = (Data)state;
                    // CalculateTheOriginOfTheLife(data.ID, data.OtherData);

                    System.Console.WriteLine("Finsihed");
                    System.Threading.Thread.Sleep(15000);


                    System.Console.WriteLine("hellow ");
                }
            ), workLoad);

            context.ApplicationInstance.CompleteRequest();
        } // End Sub ProcessRequest 


        public void LongRunningMethod(object state)
        {
            Data data = (Data)state;

            // CalculateTheOriginOfTheLife(data.ID, data.OtherData);

            System.Console.WriteLine("Finsihed");
            System.Threading.Thread.Sleep(15000);

            System.Console.WriteLine("hellow ");
        } // End Sub LongRunningMethod 


        public class Data
        {
            public int ID { get; set; }
            public string OtherData { get; set; }
        } // End Class Data 


        public bool IsReusable
        {
            get
            {
                return false;
            }
        } // End Property IsReusable 


    } // End Class mapMarker : IHttpHandler 


} // End Namespace TestPlotly.ajax 
