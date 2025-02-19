using MySql.Data.MySqlClient;
using SPC.Models;
using System.Data;

namespace SPC.DataAccess
{
    public class StockDAL
    {
        private readonly string _connectionString;

        public StockDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        // Create Stock Entry
        public async Task<int> CreateStock(Stock stock)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "INSERT INTO stock (DrugIdDrug, DrugCode, DrugName, branchId, InStock, expire_date) VALUES (@DrugId, @DrugCode, @DrugName, @BranchId, @InStock, @ExpireDate)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DrugId", stock.DrugIdDrug);
                    cmd.Parameters.AddWithValue("@DrugCode", stock.DrugCode);
                    cmd.Parameters.AddWithValue("@DrugName", stock.DrugName);
                    cmd.Parameters.AddWithValue("@BranchId", stock.BranchId);
                    cmd.Parameters.AddWithValue("@InStock", stock.InStock);
                    cmd.Parameters.AddWithValue("@ExpireDate", stock.ExpireDate);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Stock> GetStockById(int stockId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM stock WHERE idStock = @StockId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StockId", stockId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Stock
                            {
                                IdStock = reader.GetInt32("idStock"),
                                DrugIdDrug = reader.GetInt32("DrugIdDrug"),
                                DrugCode = reader.GetString("DrugCode"),
                                DrugName = reader.GetString("DrugName"),
                                BranchId = reader.GetInt32("branchId"),
                                InStock = reader.GetInt32("InStock"),
                                ExpireDate = reader.GetString("expire_date")
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Get Stock by Branch ID
        public async Task<List<Stock>> GetStockByBranch(int branchId)
        {
            var stockList = new List<Stock>();
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM stock WHERE branchId = @BranchId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BranchId", branchId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            stockList.Add(new Stock
                            {
                                IdStock = reader.GetInt32("idStock"),
                                DrugIdDrug = reader.GetInt32("DrugIdDrug"),
                                DrugCode = reader.GetString("DrugCode"),
                                DrugName = reader.GetString("DrugName"),
                                BranchId = reader.GetInt32("branchId"),
                                InStock = reader.GetInt32("InStock"),
                                ExpireDate = reader.GetString("expire_date")
                            });
                        }
                    }
                }
            }
            return stockList;
        }

        // Update Stock InStock by Stock ID
        public async Task<int> UpdateStockInStock(int stockId, int inStock)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "UPDATE stock SET InStock = @InStock WHERE idStock = @StockId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StockId", stockId);
                    cmd.Parameters.AddWithValue("@InStock", inStock);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
