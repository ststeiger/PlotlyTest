
namespace System.Data.Common
{

    public static class DbProviderFactories
    {
        public static DbProviderFactory GetFactory(string providerInvariantName)
        {
            if (string.Equals(providerInvariantName, typeof(System.Data.SqlClient.SqlClientFactory).Namespace, StringComparison.InvariantCultureIgnoreCase))
                return System.Data.SqlClient.SqlClientFactory.Instance;

            if (string.Equals(providerInvariantName, typeof(Npgsql.NpgsqlFactory).Namespace, StringComparison.InvariantCultureIgnoreCase))
                return Npgsql.NpgsqlFactory.Instance;

            if (string.Equals(providerInvariantName, typeof( MySql.Data.MySqlClient.MySqlClientFactory ).Namespace, StringComparison.InvariantCultureIgnoreCase))
                return MySql.Data.MySqlClient.MySqlClientFactory.Instance;
            
            throw new NotImplementedException($"Provider \"{providerInvariantName}\" in System.Data.Common.DbProviderFactories.");
        }

        public static DbProviderFactory GetFactory(DataRow providerRow)
        {
            throw new NotImplementedException($"GetFactory(DataRow providerRow) in System.Data.Common.DbProviderFactories.");
        }

        public static DataTable GetFactoryClasses()
        {
            throw new NotImplementedException($"GetFactoryClasses() in System.Data.Common.DbProviderFactories.");
        }
    }


}


namespace System.Web
{



    public class HttpResponse
    {
        public System.IO.TextWriter Output;
        public System.IO.Stream OutputStream;

        public void Flush()
        {

        }
    }

    public class HttpContext 
    {
        public static HttpContext Current;

        public HttpResponse Response;

        
    }
}
