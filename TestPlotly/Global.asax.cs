
namespace TestPlotly
{


    public class Global : System.Web.HttpApplication
    {


        protected void Application_Start(object sender, System.EventArgs e)
        {

        }


        // Code that runs when a new session is started
        protected void Session_Start(object sender, System.EventArgs e)
        {
            // More MicroSuck: Fix
            // “Session state has created a session id, but cannot save it because the response was already flushed by the application.”
            // https://stackoverflow.com/questions/904952/whats-causing-session-state-has-created-a-session-id-but-cannot-save-it-becau
            string sessionId = Session.SessionID;
        }


        protected void Application_BeginRequest(object sender, System.EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, System.EventArgs e)
        {

        }

        protected void Application_Error(object sender, System.EventArgs e)
        {

        }

        protected void Session_End(object sender, System.EventArgs e)
        {

        }

        protected void Application_End(object sender, System.EventArgs e)
        {

        }


    }


}
