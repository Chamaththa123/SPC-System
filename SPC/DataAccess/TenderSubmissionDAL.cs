using MySql.Data.MySqlClient;
using SPC.Models;
using System.Data;

namespace SPC.DataAccess
{
    public class TenderSubmissionDAL
    {
        private readonly string _connectionString;

        public TenderSubmissionDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        //  Create Tender Submission
        public async Task<int> AddTenderSubmission(TenderSubmission submission)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"
                    INSERT INTO tendersubmission (TenderIdTender, SupplierIdSupplier, description, unit_price, status, date)
                    VALUES (@TenderIdTender, @SupplierIdSupplier, @Description, @UnitPrice, @Status, @Date)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenderIdTender", submission.TenderIdTender);
                    cmd.Parameters.AddWithValue("@SupplierIdSupplier", submission.SupplierIdSupplier);
                    cmd.Parameters.AddWithValue("@Description", submission.Description);
                    cmd.Parameters.AddWithValue("@UnitPrice", submission.UnitPrice);
                    cmd.Parameters.AddWithValue("@Status", submission.Status);
                    cmd.Parameters.AddWithValue("@Date", submission.Date);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        //  Get All Tender Submissions
        public async Task<List<TenderSubmission>> GetAllTenderSubmissions()
        {
            var submissions = new List<TenderSubmission>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM tendersubmission";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            submissions.Add(new TenderSubmission
                            {
                                IdTenderSubmission = reader.GetInt32("idTenderSubmission"),
                                TenderIdTender = reader.GetInt32("TenderIdTender"),
                                SupplierIdSupplier = reader.GetInt32("SupplierIdSupplier"),
                                Description = reader.GetString("description"),
                                UnitPrice = reader.GetString("unit_price"),
                                Status = reader.GetInt32("status"),
                                Date = reader.GetString("date")
                            });
                        }
                    }
                }
            }

            return submissions;
        }

        // Update Tender Submission
        public async Task<bool> UpdateTenderSubmission(TenderSubmission submission)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"
                    UPDATE tendersubmission 
                    SET TenderIdTender = @TenderIdTender, SupplierIdSupplier = @SupplierIdSupplier, 
                        description = @Description, unit_price = @UnitPrice, status = @Status, date = @Date
                    WHERE idTenderSubmission = @IdTenderSubmission";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdTenderSubmission", submission.IdTenderSubmission);
                    cmd.Parameters.AddWithValue("@TenderIdTender", submission.TenderIdTender);
                    cmd.Parameters.AddWithValue("@SupplierIdSupplier", submission.SupplierIdSupplier);
                    cmd.Parameters.AddWithValue("@Description", submission.Description);
                    cmd.Parameters.AddWithValue("@UnitPrice", submission.UnitPrice);
                    cmd.Parameters.AddWithValue("@Status", submission.Status);
                    cmd.Parameters.AddWithValue("@Date", submission.Date);

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        // Delete Tender Submission
        public async Task<bool> DeleteTenderSubmission(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "DELETE FROM tendersubmission WHERE idTenderSubmission = @Id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        // Accept/Reject Tender Submission (Single API)
        public async Task<bool> UpdateTenderSubmissionStatus(int id, int status)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "UPDATE tendersubmission SET status = @Status WHERE idTenderSubmission = @Id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Status", status);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        // Get Tender Submissions by SupplierId
        public async Task<List<TenderSubmission>> GetTenderSubmissionsBySupplierId(int supplierId)
        {
            var submissions = new List<TenderSubmission>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM tendersubmission WHERE SupplierIdSupplier = @SupplierId";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupplierId", supplierId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            submissions.Add(new TenderSubmission
                            {
                                IdTenderSubmission = reader.GetInt32("idTenderSubmission"),
                                TenderIdTender = reader.GetInt32("TenderIdTender"),
                                SupplierIdSupplier = reader.GetInt32("SupplierIdSupplier"),
                                Description = reader.GetString("description"),
                                UnitPrice = reader.GetString("unit_price"),
                                Status = reader.GetInt32("status"),
                                Date = reader.GetString("date")
                            });
                        }
                    }
                }
            }

            return submissions;
        }

        // Get Tender Submissions by TenderId
        public async Task<List<TenderSubmission>> GetTenderSubmissionsByTenderId(int tenderId)
        {
            var submissions = new List<TenderSubmission>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM tendersubmission WHERE TenderIdTender = @TenderId";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenderId", tenderId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            submissions.Add(new TenderSubmission
                            {
                                IdTenderSubmission = reader.GetInt32("idTenderSubmission"),
                                TenderIdTender = reader.GetInt32("TenderIdTender"),
                                SupplierIdSupplier = reader.GetInt32("SupplierIdSupplier"),
                                Description = reader.GetString("description"),
                                UnitPrice = reader.GetString("unit_price"),
                                Status = reader.GetInt32("status"),
                                Date = reader.GetString("date")
                            });
                        }
                    }
                }
            }

            return submissions;
        }

        public async Task<List<int>> GetActiveTenderSubmissionsByDrugId(int drugId)
        {
            var supplierIds = new List<int>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = @"
            SELECT ts.SupplierIdSupplier 
            FROM tendersubmission ts
            JOIN tender t ON ts.TenderIdTender = t.idTender
            WHERE t.drugId = @DrugId AND ts.status = 1";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DrugId", drugId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            supplierIds.Add(reader.GetInt32("SupplierIdSupplier"));
                        }
                    }
                }
            }

            return supplierIds;
        }


    }
}
