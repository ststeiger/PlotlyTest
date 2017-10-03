
using System;
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


            dict.Add("foo", new Column() { index = 0, columnName = "col1", fieldType = "int" });
            dict.Add("bar", new Column() { index = 1, columnName = "col2", fieldType = "int" });

            // SQL.Serialize(dict, context);

            using (System.Data.Common.DbCommand cmd = SQL.fromFile("Marker_SO.sql"))
            {
                SQL.SerializeLargeDataset(cmd, context);
            }

        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }


}
