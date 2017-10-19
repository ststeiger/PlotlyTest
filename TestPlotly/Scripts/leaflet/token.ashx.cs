
namespace TestPlotly.Scripts.leaflet
{


    /// <summary>
    /// Zusammenfassungsbeschreibung für token
    /// </summary>
    public class token : System.Web.IHttpHandler
    {

        public void ProcessRequest(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/javascript";

            string token = SecretManager.GetSecret<string>("MapBoxAccessToken");
            
            context.Response.Write(@"

var myLeaflet = {
    token: """ + token + @"""
};

");
        } // End Sub ProcessRequest 


        public bool IsReusable
        {
            get
            {
                return false;
            }
        } // End Property IsReusable 


    } // End Class token : System.Web.IHttpHandler 


} // End Namespace TestPlotly.Scripts.leaflet 
