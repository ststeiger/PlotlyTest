using System;
using System.Collections.Generic;
using System.Web;

namespace TestPlotly
{


    public class SQL
    {

        private static System.Data.Common.DbProviderFactory fact = System.Data.Common.DbProviderFactories.GetFactory(typeof(System.Data.SqlClient.SqlClientFactory).Namespace);


        public static string GetConnectionString()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();

            csb.DataSource = System.Environment.MachineName + @"\SqlExpress";
            csb.InitialCatalog = "COR_Basic_Demo_V4";

            csb.IntegratedSecurity = true;
            if (!csb.IntegratedSecurity)
            {
                csb.UserID = "ApertureWebServicesDE";
                csb.Password = "TOP_Secret";
            }


            return csb.ToString();
        }


        public static System.Data.Common.DbConnection GetConnection()
        {
            System.Data.Common.DbConnection con = fact.CreateConnection();
            con.ConnectionString = GetConnectionString();
            return con;
        }


        public static System.Data.Common.DbCommand CreateCommand(string sql)
        {
            System.Data.Common.DbCommand cmd = fact.CreateCommand();
            cmd.CommandText = sql;

            return cmd;
        }


        public static System.Data.Common.DbCommand fromFile(string resourceName)
        {
            System.Data.Common.DbCommand cmd = fact.CreateCommand();
            cmd.CommandText = ResourceLoader.ReadEmbeddedResource(typeof(SQL), resourceName); ;

            return cmd;
        }
        

        public static System.Data.Common.DbDataReader GetDataReader(System.Data.Common.DbCommand cmd)
        {
            System.Data.Common.DbDataReader dr = null;

            System.Data.Common.DbConnection con = GetConnection();
            cmd.Connection = con;

            if (con.State != System.Data.ConnectionState.Open)
                con.Open();

            dr = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess | System.Data.CommandBehavior.CloseConnection);

            return dr;
        }


        public static System.Data.DataTable GetDataTable(System.Data.Common.DbCommand cmd)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            using (System.Data.Common.DbConnection con = GetConnection())
            {
                cmd.Connection = con;

                using (System.Data.Common.DbDataAdapter da = fact.CreateDataAdapter())
                {
                    da.SelectCommand = cmd;

                    da.Fill(dt);
                }
            }

            return dt;
        }


        public static void Serialize(object obj, System.Web.HttpContext context)
        {
#if DEBUG 
            Serialize(obj, context, true);
#else
            Serialize(obj, context, false);
#endif
        }


        public static void Serialize(object obj, System.Web.HttpContext context, bool pretty)
        {
            Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();

            using (Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(context.Response.Output))
            {
                ser.Serialize(jsonWriter, obj);
            }
        }
        

        public static void WriteAssociativeArray(Newtonsoft.Json.JsonTextWriter jsonWriter, System.Data.Common.DbDataReader dr)
        {
            WriteAssociativeArray(jsonWriter, dr, false);
        }


        public static void WriteAssociativeArray(Newtonsoft.Json.JsonTextWriter jsonWriter, System.Data.Common.DbDataReader dr, bool dataType)
        {
            // JSON: 
            //{
            //     "column_1":{ "index":0,"fieldType":"int"}
            //    ,"column_2":{ "index":1,"fieldType":"int"}
            //}

            jsonWriter.WriteStartObject();

            for (int i = 0; i < dr.FieldCount; ++i)
            {
                jsonWriter.WritePropertyName(dr.GetName(i));
                jsonWriter.WriteStartObject();

                jsonWriter.WritePropertyName("index");
                jsonWriter.WriteValue(i);

#if false
                jsonWriter.WritePropertyName("columnName");
                jsonWriter.WriteValue(dr.GetName(i));
#endif

                if (dataType)
                {
                    jsonWriter.WritePropertyName("fieldType");
                    jsonWriter.WriteValue(GetAssemblyQualifiedNoVersionName(dr.GetFieldType(i)));
                }
                
                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndObject();
        }


        public static void WriteArray(Newtonsoft.Json.JsonTextWriter jsonWriter, System.Data.Common.DbDataReader dr)
        {
            jsonWriter.WriteStartArray();

            for (int i = 0; i < dr.FieldCount; ++i)
            {
                jsonWriter.WriteStartObject();

                jsonWriter.WritePropertyName("index");
                jsonWriter.WriteValue(i);

                jsonWriter.WritePropertyName("columnName");
                jsonWriter.WriteValue(dr.GetName(i));

                jsonWriter.WritePropertyName("fieldType");
                jsonWriter.WriteValue(GetAssemblyQualifiedNoVersionName(dr.GetFieldType(i)));

                jsonWriter.WriteEndObject();
            } // Next i 

            jsonWriter.WriteEndArray();
        }


        public static void SerializeLargeDataset(System.Data.Common.DbCommand cmd, System.Web.HttpContext context, bool pretty)
        {
            Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();

            using (Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(context.Response.Output))
            {
                if (pretty)
                    jsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented;

                jsonWriter.WriteStartObject();

                jsonWriter.WritePropertyName("tables");
                jsonWriter.WriteStartArray();


                using (System.Data.Common.DbConnection con = GetConnection())
                {
                    cmd.Connection = con;

                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();

                    using (System.Data.Common.DbDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess
                         | System.Data.CommandBehavior.CloseConnection
                        ))
                    {

                        do
                        {
                            jsonWriter.WriteStartObject(); // tbl = new Table();

                            jsonWriter.WritePropertyName("columns");

                            // WriteArray(jsonWriter, dr);
                            WriteAssociativeArray(jsonWriter, dr);
                            


                            jsonWriter.WritePropertyName("rows");
                            jsonWriter.WriteStartArray();

                            if (dr.HasRows)
                            {

                                while (dr.Read())
                                {
                                    object[] thisRow = new object[dr.FieldCount];

                                    jsonWriter.WriteStartArray(); // object[] thisRow = new object[dr.FieldCount];
                                    for (int i = 0; i < dr.FieldCount; ++i)
                                    {
                                        jsonWriter.WriteValue(dr.GetValue(i));
                                    } // Next i
                                    jsonWriter.WriteEndArray(); // tbl.Rows.Add(thisRow);
                                } // Whend 

                            } // End if (dr.HasRows) 

                            jsonWriter.WriteEndArray();

                            jsonWriter.WriteEndObject(); // ser.Tables.Add(tbl);
                        } while (dr.NextResult());

                    } // End using dr 

                    if (con.State != System.Data.ConnectionState.Closed)
                        con.Close();
                } // End using con 

                jsonWriter.WriteEndArray();

                jsonWriter.WriteEndObject();
                jsonWriter.Flush();
            } // End Using jsonWriter 

            context.Response.Output.Flush();
            context.Response.OutputStream.Flush();
            context.Response.Flush();
        } // End Sub SerializeLargeDataset 


        public static void SerializeLargeDataset(System.Data.Common.DbCommand cmd, System.Web.HttpContext context)
        {
#if DEBUG 
            SerializeLargeDataset(cmd, context, true);
#else
            SerializeLargeDataset(cmd, context, false);
#endif
        } // End Sub SerializeLargeDataset 


        public static string GetAssemblyQualifiedNoVersionName(System.Type type)
        {
            if (type == null)
                return null;

            return GetAssemblyQualifiedNoVersionName(type.AssemblyQualifiedName);
        } // End Function GetAssemblyQualifiedNoVersionName


        public static string GetAssemblyQualifiedNoVersionName(string input)
        {
            int i = 0;
            bool isNotFirst = false;
            for (; i < input.Length; ++i)
            {
                if (input[i] == ',')
                {
                    if (isNotFirst)
                        break;

                    isNotFirst = true;
                }
            }

            return input.Substring(0, i);
        } // End Function GetAssemblyQualifiedNoVersionName


    }


}
