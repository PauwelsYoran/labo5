using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace Garbage.Models
{
    public static class GetGarbageType
    {
        private static string ConnectionString = Environment.GetEnvironmentVariable("ConnectionString");

        [FunctionName("GetGarbageType")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "garbage/garbageType")]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
            List<GarbageType> garbageTypes = new List<GarbageType>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    string sql = "SELECT * From GarbageType";
                    command.CommandText = sql;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        GarbageType garbageType = new GarbageType();
                        garbageType.GarbageTypeId = Guid.Parse(reader["GarbageTypeId"].ToString());
                        garbageType.Name = reader["Name"].ToString();
                        garbageTypes.Add(garbageType);
                    }
                }
            }
            return req.CreateResponse(HttpStatusCode.OK, garbageTypes);

            }
            catch (Exception ex)
            {
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
 
