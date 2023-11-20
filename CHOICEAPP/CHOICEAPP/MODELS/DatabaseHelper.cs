using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CHOICEAPP.DONEE;
using CHOICEAPP.MODELS;
using CHOICEAPP.NOTIFICATION;
using CHOICEAPP.SOURCE;
using Xamarin.Essentials;
using Microsoft.EntityFrameworkCore;

namespace CHOICEAPP.MODELS
{
    public class DatabaseHelper : IDbConn
    {
        public string server_name { get; set; }
        public string server_user { get; set; }
        public string server_pass { get; set; }
        public string server_dbname { get; set; }

        private static readonly Lazy<DatabaseHelper> lazy = new Lazy<DatabaseHelper>(() => new DatabaseHelper());

        public static DatabaseHelper Instance => lazy.Value;

        private const string ConnectionString = "Data Source=MSI\\CHOICESERVER;AttachDbFilename=\"C:\\Program Files\\Microsoft SQL Server\\MSSQL16.CHOICESERVER\\MSSQL\\DATA\\Caps.mdf\";Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        private SqlConnection sqlConnection;
        public DatabaseHelper()
        {
           
        }
        public string GetConnectionString()
        {
            if (string.IsNullOrEmpty(server_dbname) || string.IsNullOrEmpty(server_name) || string.IsNullOrEmpty(server_user) || string.IsNullOrEmpty(server_pass))
            {
                return "Error DB empty";
            }
            string s = "Data Source=MSI\\CHOICESERVER;AttachDbFilename=\"C:\\Program Files\\Microsoft SQL Server\\MSSQL16.CHOICESERVER\\MSSQL\\DATA\\Caps.mdf\";Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            return s;

        }
        public SqlConnection Connection
        {
            get { return new SqlConnection(GetConnectionString()); }
        }
        public async Task<UserModel> GetUserAsync(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand($"SELECT * FROM UserModel WHERE Email = '{username}' AND Password = '{password}'", connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return new UserModel
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                            UserType = Convert.ToInt32(reader["UserType"]),
                            // Map other properties as needed
                        };
                    }
                }
            }

            return null; // User not found
        }
        public async Task<List<ClinicModel>> GetClinicUsersAsync()
        {
            List<ClinicModel> clinicUsers = new List<ClinicModel>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM FERTILITYCLINICTBL", connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        clinicUsers.Add(new ClinicModel
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            clinicName = reader["clinicName"].ToString(),
                            clinicLogo = reader["ClinicLogo"].ToString(),
                            clinicIntroduction = reader["ClinicDetails"].ToString(),
                            Location = reader["ClinicLocation"].ToString(),
                            clinicStatus = reader["Status"].ToString()
                            // Add other properties as needed
                        });
                    }
                }
            }

            return clinicUsers;
        }

    }
}
