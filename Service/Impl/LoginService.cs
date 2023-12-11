using MepWeb.DTO.Request;
using MepWeb.Service.Interface;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.ComponentModel.Design;
using System.Configuration;

namespace MepWeb.Service.Impl
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration Configuration;
        public string ConnectionString { get; set; }
        public string ScwPlatformURL { get; set; }
        public LoginService(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("Main");
            ScwPlatformURL = Configuration["ScwPlatformURL"];
        }

        public async Task<HttpResponseMessage> LogInAsync(LoginRequest loginRequest)
        {
            var handlerHttp = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var client = new HttpClient(handlerHttp);

            var values = new Dictionary<string, string>
                          {
                              { "Email", loginRequest.Email },
                              { "Password", loginRequest.Password }
                          };

            string platformUrl = ScwPlatformURL;
            client.BaseAddress = new Uri(platformUrl);
            //var conv = JsonConvert.SerializeObject(loginRequest);
            //var strcontent = new StringContent(conv, System.Text.Encoding.UTF8, "application/json");
            var content = new FormUrlEncodedContent(values);
            HttpResponseMessage resp = await client.PostAsync("api/VerifyCredentials", content);
            return resp;
        }
        public async Task<string> GetMepUsrSigla(string email)
        {
            string command = @"SELECT usr1_username from dba.flusso_usr1 where usr1_e_mail = @email";

            using SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand(command, conn);
            cmd.Parameters.AddWithValue("@email", email);
            var reader = await cmd.ExecuteScalarAsync();

            return reader.ToString();
        }
    }
}
