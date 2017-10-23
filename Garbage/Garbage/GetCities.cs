using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using Garbage.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace Garbage
{
    

    public static class GetCities
    {
        private static string ConnectionString = Environment.GetEnvironmentVariable("ConnectionString");
            
        [FunctionName("GetCities")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "garbage/cities")]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                List<City> cities = new List<City>();
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        string sql = "SELECT * From City";
                        command.CommandText = sql;
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            City city = new City();
                            city.CityId = Guid.Parse(reader["CityId"].ToString());
                            city.Name = reader["Name"].ToString();
                            cities.Add(city);
                        }
                    }
                }
                return req.CreateResponse(HttpStatusCode.OK, cities);

            }
            catch (Exception ex)
            {
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
