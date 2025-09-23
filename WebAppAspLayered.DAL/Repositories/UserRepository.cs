﻿using Microsoft.Data.SqlClient;
using WebAppAspLayered.DL.Entities;
using WebAppAspLayered.DL.Enums;

namespace WebAppAspLayered.DAL.Repositories;

public class UserRepository
{
    private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=DemoAspClean.Db;Trusted_Connection=True;";

    public void Add(User entity)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = connection.CreateCommand())
        {
            command.CommandText = @$"INSERT INTO User (EMAIL,PASSWORD,ROLE)
                                        VALUES(@email,@password,@role)";

            command.Parameters.AddWithValue("@email", entity.Email);
            command.Parameters.AddWithValue("@password", entity.Password);
            command.Parameters.AddWithValue("@role", entity.Role.ToString());

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public User? GetUserByEmail(string email)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = connection.CreateCommand())
        {
            command.CommandText = @$"SELECT * FROM User
                                         WHERE EMAIL like @email";

            command.Parameters.AddWithValue("@email", email);

            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (!reader.Read())
                {
                    return null;
                }
                return MapEntity(reader);
            }
        }
    }

    public bool ExistByEmail(string email)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = connection.CreateCommand())
        {
            command.CommandText = @$"SELECT 
                                         cast(CASE WHEN EXISTS (
                                            SELECT 1 FROM User WHERE EMAIL = @email) 
                                             THEN 1 
                                             ELSE 0 
                                         END as bit) AS isExisting;";

            command.Parameters.AddWithValue("@email", email);

            connection.Open();

            return (bool)command.ExecuteScalar();
        }
    }

    public User MapEntity(SqlDataReader reader)
    {
        return new User()
        {
            Id = (int)reader["ID"],
            Email = (string)reader["EMAIL"],
            Password = (string)reader["PASSWORD"],
            Role = Enum.Parse<UserRole>((string)reader["ROLE"]),
        };
    }
}
