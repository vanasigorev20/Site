using Microsoft.Data.SqlClient;
using MySite.Models;

namespace MySite.Repositories
{
    public class BookRepository
    {
        private readonly string _connectionString;

        public BookRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            var books = new List<Book>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT Id, Title, Author, Year, Price FROM Books", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        books.Add(new Book
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Year = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                            Price = reader.IsDBNull(4) ? null : reader.GetDecimal(4)
                        });
                    }
                }
            }
            return books;
        }
    }
}