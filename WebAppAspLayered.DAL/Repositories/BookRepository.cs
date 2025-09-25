using Microsoft.Data.SqlClient;
using WebAppAspLayered.DAL.Models;
using WebAppAspLayered.DL.Entities;

namespace WebAppAspLayered.DAL.Repositories;

public class BookRepository : BaseRepository
{
    public List<Book> GetAll(int page, BookFilterDal? filter)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = """
            SELECT *
            FROM Book
            WHERE (ISBN LIKE @isbn OR @isbn IS NULL)
            AND (Name LIKE @name OR @name IS NULL)
            ORDER BY Id
            OFFSET @offset ROWS
            FETCH NEXT 5 ROWS ONLY
            """;

        command.Parameters.AddWithValue("@offset", page * 5);
        command.Parameters.AddWithValue("@isbn",
            filter?.ISBN != null ? $"%{filter.ISBN}%" : DBNull.Value);
        command.Parameters.AddWithValue("@name",
            filter?.Name != null ? $"%{filter.Name}%" : DBNull.Value);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        List<Book> books = [];

        while (reader.Read())
        {
            books.Add(MapBook(reader));
        }

        return books;
    }

    public int CountAny(BookFilterDal? filter)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = """
            SELECT COUNT(*)
            FROM Book
            WHERE (ISBN LIKE @isbn OR @isbn IS NULL)
            AND (Name LIKE @name OR @name IS NULL)
            """;

        command.Parameters.AddWithValue("@isbn",
            filter?.ISBN != null ? $"%{filter.ISBN}%" : DBNull.Value);
        command.Parameters.AddWithValue("@name",
            filter?.Name != null ? $"%{filter.Name}%" : DBNull.Value);

        connection.Open();
        return (int) command.ExecuteScalar();
    }

    private static Book MapBook(SqlDataReader reader)
    {
        return new()
        {
            Id = (int)reader["Id"],
            ISBN = (string)reader["ISBN"],
            Name = (string)reader["Name"],
            Author = (string)reader["Author"],
            IsFav = (bool)reader["IsFav"],
        };
    }
}
