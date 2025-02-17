using MySql.Data.MySqlClient;
using SPC.Models;
using System.Data;

namespace SPC.DataAccess
{
    public class DrugDAL
    {
        private readonly string _connectionString;

        public DrugDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        // Add Drug
        public async Task<int> AddDrug(Drug drug)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "INSERT INTO drug (code, name, description,stockIn, expiry_date, status) VALUES (@code, @name, @description,@stockIn, @expiry_date, @status)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@code", drug.Code);
                    cmd.Parameters.AddWithValue("@name", drug.Name);
                    cmd.Parameters.AddWithValue("@description", drug.Description);
                    cmd.Parameters.AddWithValue("@expiry_date", drug.ExpiryDate);
                    cmd.Parameters.AddWithValue("@status", drug.Status);
                    cmd.Parameters.AddWithValue("@stockIn", drug.StockIn);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Get All Drugs
        public async Task<List<Drug>> GetAllDrugs()
        {
            var drugs = new List<Drug>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM drug";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        drugs.Add(new Drug
                        {
                            IdDrug = reader.GetInt32("idDrug"),
                            Code = reader.GetString("code"),
                            Name = reader.GetString("name"),
                            Description = reader.IsDBNull("description") ? "" : reader.GetString("description"),
                            ExpiryDate = reader.GetString("expiry_date"),
                            StockIn = reader.GetInt32("stockIn"),
                            Status = reader.GetInt32("status")
                        });
                    }
                }
            }
            return drugs;
        }

        // Get Drug By ID
        public async Task<Drug> GetDrugById(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM drug WHERE idDrug = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Drug
                            {
                                IdDrug = reader.GetInt32("idDrug"),
                                Code = reader.GetString("code"),
                                Name = reader.GetString("name"),
                                Description = reader.IsDBNull("description") ? "" : reader.GetString("description"),
                                ExpiryDate = reader.GetString("expiry_date"),
                                Status = reader.GetInt32("status"),
                                StockIn = reader.GetInt32("stockIn")
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Update Drug
        public async Task<bool> UpdateDrug(Drug drug)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "UPDATE drug SET code=@code, name=@name, description=@description, expiry_date=@expiry_date, status=@status WHERE idDrug=@id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", drug.IdDrug);
                    cmd.Parameters.AddWithValue("@code", drug.Code);
                    cmd.Parameters.AddWithValue("@name", drug.Name);
                    cmd.Parameters.AddWithValue("@description", drug.Description);
                    cmd.Parameters.AddWithValue("@expiry_date", drug.ExpiryDate);
                    cmd.Parameters.AddWithValue("@status", drug.Status);

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        // Update StockIn for a Drug
        public async Task<bool> UpdateStockIn(int id, int newStockIn)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "UPDATE drug SET stockIn = @stockIn WHERE idDrug = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@stockIn", newStockIn);

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

    }
}
