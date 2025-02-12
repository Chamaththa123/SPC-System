using MySql.Data.MySqlClient;
using SPC.Models;
using System.Data;

namespace SPC.DataAccess
{
    public class UserDAL
    {

        private readonly string _connectionString;

        public UserDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public async Task<int> RegisterUser(User user)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "INSERT INTO user (name, email, password, contact, role, status) VALUES (@name, @email, @password, @contact, @role, @status)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);  // Hashed password
                    cmd.Parameters.AddWithValue("@contact", user.Contact);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                    cmd.Parameters.AddWithValue("@status", user.Status);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM user WHERE email = @Email";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                IdUser = reader.GetInt32("idUser"),
                                Name = reader.GetString("name"),
                                Email = reader.GetString("email"),
                                Password = reader.GetString("password"),
                                Contact = reader.GetString("contact"),
                                Role = reader.GetInt32("role"),
                                Status = reader.GetInt32("status")
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<User> GetUserById(int userId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM user WHERE idUser = @userId";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                IdUser = reader.GetInt32("idUser"),
                                Name = reader.GetString("name"),
                                Email = reader.GetString("email"),
                                Password = reader.GetString("password"),
                                Contact = reader.GetString("contact"),
                                Role = reader.GetInt32("role"),
                                Status = reader.GetInt32("status")
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<bool> UpdateUserStatus(int userId, int status)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE user SET status = @Status WHERE idUser = @UserId";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Status", status);

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }


    }
}
