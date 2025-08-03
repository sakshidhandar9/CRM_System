using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Data;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace CustomerRelationManagement.Services
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;

        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool ValidateUser(string username, string password, out string roleName)
        {
            roleName = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ValidateUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    con.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        roleName = result.ToString();
                        return true;
                    }
                }
            }

            return false;
        }

       
    }
}