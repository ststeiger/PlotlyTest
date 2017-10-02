
using System;
using System.Collections.Generic;
using System.Web;


namespace TestPlotly.ajax
{


    /// <summary>
    /// Zusammenfassungsbeschreibung für mapMarker
    /// </summary>
    public class mapMarker : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            System.Data.DataTable dt = null;

            //using (System.Data.Common.DbCommand cmd = SQL.CreateCommand("SELECT * FROM T_Benutzer"))
            //{
            //    dt = SQL.GetDataTable(cmd);
            //}

            // context.Response.Write("Hello World - RowCount: " + dt.Rows.Count.ToString());

            using (System.Data.Common.DbCommand cmd = SQL.CreateCommand("SELECT * FROM T_Benutzer"))
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
