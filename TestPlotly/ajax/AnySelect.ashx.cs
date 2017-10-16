
using System.Web;


namespace TestPlotly.ajax
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für AnySelect
    /// </summary>
    public class AnySelect : System.Web.IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {


        public string GetUserId()
        {
            string tBenutzer = "12435";
            
            return tBenutzer;
        } // End Functon GetUserId
        

        public string ScriptName
        {
            get
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;

                string strSQL = null;

                strSQL = context.Request.QueryString["SQL"];

                if (strSQL == null)
                {
                    strSQL = context.Request.Form["SQL"];
                }

                if (strSQL == null)
                {
                    strSQL = context.Request.Headers["SQL"];
                }

                if (System.StringComparer.OrdinalIgnoreCase.Equals(strSQL, "FMS_Maps_Marker_GB.sql"))
                    strSQL = "Marker_GB.sql";

                return strSQL;
            }

        } // End Property ScriptName

        public void AddPassedParameters(System.Data.IDbCommand cmd, HttpContext context)
        {
            string userId = GetUserId();

            foreach (string queryKey in context.Request.QueryString)
            {
                string queryValue = context.Request.QueryString[queryKey];

                SQL.AddParameter(cmd, queryKey, queryValue);
            }

            foreach (string formKey in context.Request.Form.AllKeys)
            {
                string formValue = context.Request.Form[formKey];

                SQL.AddParameter(cmd, formKey, formValue);
            }


            try
            {
                SQL.AddParameter(cmd, "__user_id", userId);

            }
            catch (System.Exception ex)
            {
            }

            try
            {
                SQL.AddParameter(cmd, "__stichtag", System.DateTime.Today.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));

            }
            catch (System.Exception ex)
            {
            }

        } // End Sub AddPassedParameters 


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            using (System.Data.Common.DbCommand cmd = SQL.fromFile(this.ScriptName))
            {
                AddPassedParameters(cmd, context);

                SQL.SerializeLargeDataset(cmd, context);
            } // cmd

        } // End Sub ProcessRequest


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }


}
