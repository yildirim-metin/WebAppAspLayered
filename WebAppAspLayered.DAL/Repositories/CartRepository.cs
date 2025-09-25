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
                INNER JOIN Cart c ON c.Id = ci.CartId
            WHERE c.UserId = @userId
            """;

        command.Parameters.AddWithValue("@userId", userId);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        return MapCart(reader);
    }

    private static Cart MapCart(SqlDataReader reader)
    {
        reader.Read();
        Cart cart = new()
        {
            Id = (int)reader["CartId"],
            UserId = (int)reader["UserId"],
            Items = 
            [
                MapCartItem(reader),
            ],
        };

        while (reader.Read())
        {
            cart.Items.Add(MapCartItem(reader));
        }

        return cart;
    }

    private static CartItem MapCartItem(SqlDataReader reader)
    {
        return new()
        {
           Id = (int)reader["CartItemId"],
           CartId = (int)reader["CartId"],
           ProductId = (int)reader["ProductId"],
           Quantity = (int)reader["Quantity"],
        };
    }
}
