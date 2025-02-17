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
                string query = "INSERT INTO stock (DrugIdDrug, branchId, InStock, expire_date) VALUES (@DrugId, @BranchId, @InStock, @ExpireDate)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DrugId", stock.DrugIdDrug);
                    cmd.Parameters.AddWithValue("@BranchId", stock.BranchId);
                    cmd.Parameters.AddWithValue("@InStock", stock.InStock);
                    cmd.Parameters.AddWithValue("@ExpireDate", stock.ExpireDate);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
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
    }
}
