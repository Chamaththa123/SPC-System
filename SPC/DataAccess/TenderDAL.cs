using MySql.Data.MySqlClient;
using SPC.Models;
using System.Data;

namespace SPC.DataAccess
{
    public class TenderDAL
    {
        private readonly string _connectionString;

        public TenderDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        // Create Tender
        public async Task<int> AddTender(Tender tender)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "INSERT INTO tender (drugId, description, status, date) VALUES (@drugId, @description, @status, @date)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@drugId", tender.DrugId);
                    cmd.Parameters.AddWithValue("@description", tender.Description);
                    cmd.Parameters.AddWithValue("@status", tender.Status);
                    cmd.Parameters.AddWithValue("@date", tender.Date);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Read All Tenders

        public async Task<List<dynamic>> GetAllTenders()
        {
            var tenders = new List<dynamic>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"
                    SELECT t.idTender, t.drugId, d.name AS drugName, t.description, t.status, t.date
                    FROM Tender t
                    JOIN Drug d ON t.drugId = d.idDrug";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tenders.Add(new
                            {
                                IdTender = reader.GetInt32("idTender"),
                                DrugId = reader.GetInt32("drugId"),
                                DrugName = reader.GetString("drugName"),
                                Description = reader.GetString("description"),
                                Status = reader.GetInt32("status"),
                                Date = reader.GetString("date")
                            });
                        }
                    }
                }
            }

            return tenders;
        }

        // Get Tender by ID
        public async Task<dynamic> GetTenderById(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"
            SELECT t.idTender, t.drugId, d.name AS drugName, t.description, t.status, t.date
            FROM Tender t
            JOIN Drug d ON t.drugId = d.idDrug
            WHERE t.idTender = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new
                            {
                                IdTender = reader.GetInt32("idTender"),
                                DrugId = reader.GetInt32("drugId"),
                                DrugName = reader.GetString("drugName"),
                                Description = reader.GetString("description"),
                                Status = reader.GetInt32("status"),
                                Date = reader.GetString("date")
                            };
                        }
                    }
                }
            }
            return null;
        }



        // Update Tender
        public async Task<int> UpdateTender(Tender tender)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "UPDATE tender SET drugId = @drugId, description = @description, status = @status, date = @date WHERE idTender = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", tender.IdTender);
                    cmd.Parameters.AddWithValue("@drugId", tender.DrugId);
                    cmd.Parameters.AddWithValue("@description", tender.Description);
                    cmd.Parameters.AddWithValue("@status", tender.Status);
                    cmd.Parameters.AddWithValue("@date", tender.Date);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Delete Tender
        public async Task<int> DeleteTender(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "DELETE FROM tender WHERE idTender = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Toggle Active/Inactive Status
        public async Task<int> ToggleTenderStatus(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "UPDATE tender SET status = CASE WHEN status = 1 THEN 0 ELSE 1 END WHERE idTender = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
