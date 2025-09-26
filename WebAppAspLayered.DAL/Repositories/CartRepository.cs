using Microsoft.Data.SqlClient;
using WebAppAspLayered.DL.Entities;

namespace WebAppAspLayered.DAL.Repositories;

public class CartRepository : BaseRepository
{
    public Cart GetCartByUserId(int userId)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = """
            SELECT c.Id as CartId, c.UserId, ci.Id as CartItemId, ci.BookId, ci.Quantity
            FROM CartItem ci
                RIGHT JOIN Cart c ON c.Id = ci.CartId
            WHERE c.UserId = @userId
            """;

        command.Parameters.AddWithValue("@userId", userId);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        Cart? cart = MapCart(reader);
        return cart ?? CreateCart(userId);
    }

    public bool HasUserCart(int userId)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = """
            SELECT COUNT(*)
            FROM Cart
            WHERE UserId = @userId
            """;

        command.Parameters.AddWithValue("@userId", userId);

        connection.Open();

        return (int)command.ExecuteScalar() > 0;
    }

    private static Cart? MapCart(SqlDataReader reader)
    {
        if (!reader.Read())
        {
            return null;
        }

        Cart cart = new()
        {
            Id = (int)reader["CartId"],
            UserId = (int)reader["UserId"],
        };

        CartItem? item = MapCartItem(reader);
        if (item != null) cart.Items.Add(item);

        while (reader.Read())
        {
            cart.Items.Add(MapCartItem(reader)!);
        }

        return cart;
    }

    private static CartItem? MapCartItem(SqlDataReader reader)
    {
        if (reader["CartId"] == DBNull.Value || reader["BookId"] == DBNull.Value || reader["Quantity"] == DBNull.Value)
        {
            return null;
        }

        return new()
        {
           Id = (int)reader["CartItemId"],
           CartId = (int)reader["CartId"],
           BookId = (int)reader["BookId"],
           Quantity = (int)reader["Quantity"],
        };
    }

    private Cart CreateCart(int userId)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = """
            INSERT INTO Cart (UserId) VALUES (@userId);
            SELECT @@IDENTITY
            """;

        command.Parameters.AddWithValue("@userId", userId);

        connection.Open();

        int cartId = decimal.ToInt32((decimal)command.ExecuteScalar());

        return new()
        {
            Id = cartId,
            UserId = userId,
        };
    }

    public void AddItem(CartItem cartItem)
    {
        using SqlConnection connection = new(_connectionString);
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = """
            IF NOT EXISTS (SELECT 1 FROM CartItem WHERE Id = @id)
            BEGIN
                INSERT INTO CartItem (CartId, BookId, Quantity)
                VALUES (@cartId, @bookId, @quantity)
            END
            ELSE
            BEGIN
                UPDATE CartItem
                SET CartId = @cartId, BookId = @bookId, Quantity = @quantity
                WHERE Id = @id
            END
            """;

        command.Parameters.AddWithValue("@id", cartItem.Id);
        command.Parameters.AddWithValue("@cartId", cartItem.CartId);
        command.Parameters.AddWithValue("@bookId", cartItem.BookId);
        command.Parameters.AddWithValue("@quantity", cartItem.Quantity);

        connection.Open();

        command.ExecuteNonQuery();
    }
}
