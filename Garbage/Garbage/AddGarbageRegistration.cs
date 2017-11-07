using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using Garbage.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace Garbage
{
    public static class AddGarbageRegistration
    {
        private static string ConnectionString = Environment.GetEnvironmentVariable("ConnectionString");

        [FunctionName("AddgarbageResgistration")]
        public async static System.Threading.Tasks.Task<HttpResponseMessage> addGarbageRegistration([HttpTrigger(AuthorizationLevel.Function, "put","post", Route = "HttpTriggerCSharp/AddGarbageRegistartion")]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                var content = await req.Content.ReadAsStringAsync();
                var registration = JsonConvert.DeserializeObject<GarbageRegistration>(content);

                using (SqlConnection connection= new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        string id = Guid.NewGuid().ToString();
                        DateTime time = DateTime.Now; 
                        string sql = "INSERT INTO GarbageRegistration Values(@id,@user,@email, @description,@GarbageTypeId,@CityId,@street,@weight,@lat,@long,CONVERT(datetime,CURRENT_TIMESTAMP))";
                        command.CommandText = sql;
                        command.Parameters.AddWithValue("@id", id);
                    
                        command.Parameters.AddWithValue("@user", registration.Name);
                        command.Parameters.AddWithValue("@email", registration.Email);
                        command.Parameters.AddWithValue("@description", registration.Description);                     
                        command.Parameters.AddWithValue("@GarbageTypeId", registration.GarbagetypeId);
                        command.Parameters.AddWithValue("@CityId", registration.CityId);
                        command.Parameters.AddWithValue("@street", registration.Street);
                        command.Parameters.AddWithValue("@weight", registration.Weight);
                        command.Parameters.AddWithValue("@lat", registration.Lat);
                        command.Parameters.AddWithValue("@long", registration.Long);
                        //command.Parameters.AddWithValue("@timestamp", Convert.ToDateTime( time));
                        command.ExecuteNonQuery();
                        registration.GarbageRegistrationid = new Guid(id);


                    }

                }

                var json = JsonConvert.SerializeObject(registration);
                return req.CreateResponse(HttpStatusCode.OK, json);
            }

            catch(Exception ex)
            {
                return req.CreateResponse(HttpStatusCode.InternalServerError);
           }
        }
    }
}
