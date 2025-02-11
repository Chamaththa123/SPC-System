using MySql.Data.MySqlClient;
using SPC.Models;
using System.Data;

namespace SPC.DataAccess
{
    public class FacilityDAL
    {
        private readonly string _connectionString;

        public FacilityDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }


        //get all facilties
        public async Task<List<Facility>> GetAllFacility()
        {
            var facilities = new List<Facility>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync().ConfigureAwait(false);
                string query = "SELECT * FROM facility";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        facilities.Add(new Facility
                        {
                            idFacility = reader.GetInt32("idFacility"),
                            name = reader.GetString("name"),
                            location = reader.GetString("location"),
                            type = reader.GetInt32("type")
                        });
                    }
                }
            }
            return facilities;
        }

        //get facility by id
        public async Task<Facility> GetFacilityById(int id)
        {
            Facility facility = null;
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync().ConfigureAwait(false);
                string query = "SELECT * FROM facility WHERE idFacility = @idFacility";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idFacility", id);
                    using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        if (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            facility = new Facility
                            {
                                idFacility = reader.GetInt32("idFacility"),
                                name = reader.GetString("name"),
                                location = reader.GetString("location"),
                                type = reader.GetInt32("type")
                            };
                        }
                    }
                }
            }
            return facility;
        }


        //add facility
        public async Task AddFacility(Facility facility)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync().ConfigureAwait(false);
                string query = "INSERT INTO facility (name, location, type) VALUES (@name, @location, @type)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", facility.name);
                    cmd.Parameters.AddWithValue("@location", facility.location);
                    cmd.Parameters.AddWithValue("@type", facility.type);
                    await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
        }


        //update facility
        public async Task UpdateFacility(Facility facility)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync().ConfigureAwait(false);
                string query = "UPDATE facility SET name=@name, location=@location, type=@type WHERE idFacility=@idFacility";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idFacility", facility.idFacility);
                    cmd.Parameters.AddWithValue("@name", facility.name);
                    cmd.Parameters.AddWithValue("@location", facility.location);
                    cmd.Parameters.AddWithValue("@type", facility.type);
                    await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
        }
    }
}
