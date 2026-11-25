using Microsoft.Data.SqlClient;
using MySite.Models;

namespace MySite.Repositories
{
    public class ItemRepository
    {
        private readonly string _connectionString;

        public ItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<List<Item>> GetAllAsync()
        {
            var items = new List<Item>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var cmd = new SqlCommand("SELECT Id, Title, Description, UserId FROM Items", connection);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(new Item
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            UserId = reader.GetInt32(3)
                        });
                    }
                }
            }
            return items;
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var cmd = new SqlCommand("SELECT Id, Title, Description, UserId FROM Items WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Item
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            UserId = reader.GetInt32(3)
                        };
                    }
                }
            }
            return null;
        }

        public async Task<List<Item>> GetByUserIdAsync(int userId)
        {
            var items = new List<Item>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var cmd = new SqlCommand("SELECT Id, Title, Description, UserId FROM Items WHERE UserId = @UserId", connection);
                cmd.Parameters.AddWithValue("@UserId", userId);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(new Item
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            UserId = reader.GetInt32(3)
                        });
                    }
                }
            }
            return items;
        }

        // Создать запись
        public async Task CreateAsync(Item item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var cmd = new SqlCommand("INSERT INTO Items (Title, Description, UserId) VALUES (@Title, @Desc, @UserId)", connection);
                cmd.Parameters.AddWithValue("@Title", item.Title);
                cmd.Parameters.AddWithValue("@Desc", item.Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAsync(Item item, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var cmd = new SqlCommand("UPDATE Items SET Title = @Title, Description = @Desc WHERE Id = @Id AND UserId = @UserId", connection);
                cmd.Parameters.AddWithValue("@Title", item.Title);
                cmd.Parameters.AddWithValue("@Desc", item.Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@UserId", userId);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int id, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var cmd = new SqlCommand("DELETE FROM Items WHERE Id = @Id AND UserId = @UserId", connection);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@UserId", userId);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}