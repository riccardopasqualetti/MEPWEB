using MepWeb.Service.Interface;
using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;
using System.Configuration;

namespace MepWeb.Service.Impl
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration Configuration;
        public string ConnectionString { get; set; }
        public LoginService(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("Main");
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
