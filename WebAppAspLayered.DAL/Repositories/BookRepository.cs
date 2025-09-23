using Microsoft.Data.SqlClient;
using WebAppAspLayered.DL.Entities;

namespace WebAppAspLayered.DAL.Repositories;

public class BookRepository
{
    private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=WebAppAspLayered_DB;Trusted_Connection=True;";

    public List<Book> GetAll()
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = """
            SELECT *
            FROM Book
            """;

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        List<Book> books = [];

        while (reader.Read())
        {
            books.Add(MapBook(reader));
        }

        return books;
    }

    private static Book MapBook(SqlDataReader reader)
    {
        return new()
        {
            Id = (int)reader["Id"],
            ISBN = (string)reader["ISBN"],
            Name = (string)reader["Name"],
            Author = (string)reader["Author"],
        };
    }
}
