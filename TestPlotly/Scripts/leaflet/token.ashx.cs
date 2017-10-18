using System;
using System.Collections.Generic;
using System.Web;

namespace TestPlotly.Scripts.leaflet
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für token
    /// </summary>
    public class token : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";

            string token = SecretManager.GetSecret<string>("MapBoxAccessToken");
            
            context.Response.Write(@"

var myLeaflet = {
    token: """ + token + @"""
};

");
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