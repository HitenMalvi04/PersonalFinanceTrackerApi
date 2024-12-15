using Microsoft.Data.SqlClient;
using System.Data;
using PersonalFinanceTrackerAPI.Models;

namespace PersonalFinanceTrackerAPI.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User GetUserById(int userid)
        {
            User user = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("PR_LOC_Users_SelectByPK", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userid);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            UserName = reader["UserName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString()
                        };
                    }
                }
            }

            return user;
        }

        public void InsertUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("PR_LOC_Users_Insert", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters for the insert procedure
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

                // Execute the insert command
                command.ExecuteNonQuery();
            }
        }

        public void UpdateUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("PR_LOC_Users_Update", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters for the update procedure
                command.Parameters.AddWithValue("@UserId", user.UserId);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

                // Execute the update command
                command.ExecuteNonQuery();
            }
        }


        public void RegisterUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("PR_Users_Register", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters for the insert procedure
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);  // No hashing here

                // Execute the insert command
                command.ExecuteNonQuery();
            }
        }



        public User LoginUser(string userName, string passwordHash)
        {
            User user = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("PR_Users_Login", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters for the login procedure
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);  // Use plain password

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            UserName = reader["UserName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString()
                        };
                    }
                }
            }

            return user;
        }





    }
}
