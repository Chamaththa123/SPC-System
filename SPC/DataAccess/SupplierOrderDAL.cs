using MySql.Data.MySqlClient;
using SPC.Models;
using System.Data;

namespace SPC.DataAccess
{
    public class SupplierOrderDAL
    {
        private readonly string _connectionString;

        public SupplierOrderDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        // Add Supplier Order
        public async Task<int> AddSupplierOrder(SupplierOrder order)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"
                    INSERT INTO supplier_order (drugId, drugCode, drugName, SupplierId, qty, status)
                    VALUES (@DrugId, @DrugCode, @DrugName, @SupplierId, @Qty, @Status)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DrugId", order.DrugId);
                    cmd.Parameters.AddWithValue("@DrugCode", order.DrugCode);
                    cmd.Parameters.AddWithValue("@DrugName", order.DrugName);
                    cmd.Parameters.AddWithValue("@SupplierId", order.SupplierId);
                    cmd.Parameters.AddWithValue("@Qty", order.Qty);
                    cmd.Parameters.AddWithValue("@Status", order.Status);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Get All Supplier Orders
        public async Task<List<SupplierOrder>> GetAllSupplierOrders()
        {
            var orders = new List<SupplierOrder>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM supplier_order";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            orders.Add(new SupplierOrder
                            {
                                IdSupplierOrder = reader.GetInt32("IdSupplierOrder"),
                                DrugId = reader.GetInt32("drugId"),
                                DrugCode = reader.GetString("drugCode"),
                                DrugName = reader.GetString("drugName"),
                                SupplierId = reader.GetInt32("SupplierId"),
                                Qty = reader.GetInt32("qty"),
                                Status = reader.GetInt32("status")
                            });
                        }
                    }
                }
            }

            return orders;
        }

        // Get Supplier Order by Id
        public async Task<SupplierOrder> GetSupplierOrderById(int id)
        {
            SupplierOrder order = null;

            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM supplier_order WHERE idSupplierOrder = @Id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            order = new SupplierOrder
                            {
                                IdSupplierOrder = reader.GetInt32("idSupplierOrder"),
                                DrugId = reader.GetInt32("drugId"),
                                DrugCode = reader.GetString("drugCode"),
                                DrugName = reader.GetString("drugName"),
                                SupplierId = reader.GetInt32("SupplierId"),
                                Qty = reader.GetInt32("qty"),
                                Status = reader.GetInt32("status")
                            };
                        }
                    }
                }
            }

            return order;
        }

        // Update Status of Supplier Order
        public async Task<bool> UpdateSupplierOrderStatus(int id, int status)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "UPDATE supplier_order SET status = @Status WHERE idSupplierOrder = @Id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Status", status);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        // Get Supplier Orders by Supplier ID
        public List<SupplierOrder> GetSupplierOrdersBySupplierID(int supplierId)
        {
            var orders = new List<SupplierOrder>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                var query = "SELECT * FROM supplier_order WHERE SupplierId = @SupplierId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@SupplierId", supplierId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new SupplierOrder
                        {
                            IdSupplierOrder = reader.GetInt32("idSupplierOrder"),
                            DrugId = reader.GetInt32("drugId"),
                            DrugCode = reader.GetString("drugCode"),
                            DrugName = reader.GetString("drugName"),
                            SupplierId = reader.GetInt32("SupplierId"),
                            Qty = reader.GetInt32("qty"),
                            Status = reader.GetInt32("status")
                        });
                    }
                }
            }

            return orders;
        }

    }
}
