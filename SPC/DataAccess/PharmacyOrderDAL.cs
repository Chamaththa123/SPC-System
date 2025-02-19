using MySql.Data.MySqlClient;
using SPC.Models;
using System.Data;

namespace SPC.DataAccess
{
    public class PharmacyOrderDAL
    {
        private readonly string _connectionString;

        public PharmacyOrderDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        // Add Pharmacy Order
        public async Task<int> AddPharmacyOrder(PharmacyOrder order)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"
                    INSERT INTO pharmacy_order (StockId, drugId, drugCode, drugName, branchId, qty, status)
                    VALUES (@StockId, @DrugId, @DrugCode, @DrugName, @BranchId, @Qty, @Status)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StockId", order.StockId);
                    cmd.Parameters.AddWithValue("@DrugId", order.DrugId);
                    cmd.Parameters.AddWithValue("@DrugCode", order.DrugCode);
                    cmd.Parameters.AddWithValue("@DrugName", order.DrugName);
                    cmd.Parameters.AddWithValue("@BranchId", order.BranchId);
                    cmd.Parameters.AddWithValue("@Qty", order.Qty);
                    cmd.Parameters.AddWithValue("@Status", order.Status);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Get All Pharmacy Orders
        public async Task<List<PharmacyOrder>> GetAllPharmacyOrders()
        {
            var orders = new List<PharmacyOrder>();
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM pharmacy_order";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        orders.Add(new PharmacyOrder
                        {
                            IdPharmacyOrder = reader.GetInt32("idPharmacyOrder"),
                            StockId = reader.GetInt32("StockId"),
                            DrugId = reader.GetInt32("drugId"),
                            DrugCode = reader.GetString("drugCode"),
                            DrugName = reader.GetString("drugName"),
                            BranchId = reader.GetInt32("branchId"),
                            Qty = reader.GetInt32("qty"),
                            Status = reader.GetInt32("status")
                        });
                    }
                }
            }
            return orders;
        }

        // Get Pharmacy Order by ID
        public async Task<PharmacyOrder> GetPharmacyOrderById(int id)
        {
            PharmacyOrder order = null;
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM pharmacy_order WHERE idPharmacyOrder = @Id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            order = new PharmacyOrder
                            {
                                IdPharmacyOrder = reader.GetInt32("idPharmacyOrder"),
                                StockId = reader.GetInt32("StockId"),
                                DrugId = reader.GetInt32("drugId"),
                                DrugCode = reader.GetString("drugCode"),
                                DrugName = reader.GetString("drugName"),
                                BranchId = reader.GetInt32("branchId"),
                                Qty = reader.GetInt32("qty"),
                                Status = reader.GetInt32("status")
                            };
                        }
                    }
                }
            }
            return order;
        }

        // Update Status of Pharmacy Order
        public async Task<bool> UpdatePharmacyOrderStatus(int id, int status)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "UPDATE pharmacy_order SET status = @Status WHERE idPharmacyOrder = @Id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Status", status);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        // Get Pharmacy Orders by Branch ID
        public async Task<List<PharmacyOrder>> GetPharmacyOrdersByBranchID(int branchId)
        {
            var orders = new List<PharmacyOrder>();
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM pharmacy_order WHERE branchId = @BranchId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BranchId", branchId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            orders.Add(new PharmacyOrder
                            {
                                IdPharmacyOrder = reader.GetInt32("idPharmacyOrder"),
                                StockId = reader.GetInt32("StockId"),
                                DrugId = reader.GetInt32("drugId"),
                                DrugCode = reader.GetString("drugCode"),
                                DrugName = reader.GetString("drugName"),
                                BranchId = reader.GetInt32("branchId"),
                                Qty = reader.GetInt32("qty"),
                                Status = reader.GetInt32("status")
                            });
                        }
                    }
                }
            }
            return orders;
        }
    }
}
