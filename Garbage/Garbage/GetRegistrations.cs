using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Garbage.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace Garbage
{
    public static class GetRegistrations
    {

        private static string ConnectionString = Environment.GetEnvironmentVariable("ConnectionString");

        [FunctionName("GetRegistrations")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "garbage/getRegistrations/{user}")]HttpRequestMessage req, string user, TraceWriter log)
        {
            //try
            //{
                List<GarbageRegistration> registrations = new List<GarbageRegistration>();
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        string sql = "SELECT * From GarbageRegistration WHERE Name = @user";
                        command.CommandText = sql;
                        command.Parameters.AddWithValue("@user", user);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            GarbageRegistration regis = new GarbageRegistration();
                            regis.CityId = Guid.Parse(reader["CityId"].ToString());
                            regis.Description = reader["Description"].ToString();
                            regis.Street = reader["Street"].ToString();
                            regis.TimeStamp = DateTime.Parse(reader["Timestamp"].ToString());
                        regis.Name = reader["Name"].ToString();
                            Debug.WriteLine(regis.Lat);

                            //regis.Lat = float.Parse(reader["Lat"].ToString());
                            //regis.Long = float.Parse(reader["Long"].ToString());
                            regis.Weight = int.Parse(reader["Weight"].ToString());
                            regis.Email = reader["Email"].ToString();
                            regis.GarbageRegistrationid = Guid.Parse(reader["GarbageRegistrationid"].ToString());
                            regis.GarbagetypeId = Guid.Parse(reader["GarbagetypeId"].ToString());
                        registrations.Add(regis);
                        }
                    }
                }
                return req.CreateResponse(HttpStatusCode.OK, registrations);

            //}
//catch (Exception ex)
           // {
           //     return req.CreateResponse(HttpStatusCode.InternalServerError);
            //}
        }
    }
}

