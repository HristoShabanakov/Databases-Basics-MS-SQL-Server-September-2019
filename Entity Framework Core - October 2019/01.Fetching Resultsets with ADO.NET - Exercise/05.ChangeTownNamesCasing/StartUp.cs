
namespace _05.ChangeTownNamesCasing
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    class StartUp
    {

        private static string connectionString =
        "Server=SHABBY\\SQLEXPRESS;" +
        "Database=MinionsDB;" +
        "Integrated Security=true;";

        private static SqlConnection connection = new SqlConnection(connectionString);
        static void Main()
        {
            string countryName = Console.ReadLine();

            connection.Open();

            using (connection)
            {
                string updateTowns = @"UPDATE Towns
                                     SET Name = UPPER(Name)
                                     WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)

                                    SELECT t.Name 
                                    FROM Towns as t
                                    JOIN Countries AS c ON c.Id = t.CountryCode
                                    WHERE c.Name = @countryName";

                using (SqlCommand command = new SqlCommand(updateTowns, connection))
                {
                    command.Parameters.AddWithValue("@countryName", countryName);
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows == 0)
                    {
                        Console.WriteLine("No town names were affected.");
                    }

                    else
                    {
                        Console.WriteLine($"{affectedRows} town names were affected.");
                    }
                }
            }
        }

        private static void PrintTownNames(SqlConnection connection, string countryName)
        {
            string getTowns = @"SELECT t.Name 
                              FROM Towns as t
                              JOIN Countries AS c 
                              ON c.Id = t.CountryCode
                              WHERE c.Name = @countryName";

            using (SqlCommand command = new SqlCommand(getTowns, connection))
            {
                command.Parameters.AddWithValue("@countryName", countryName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var towns = new List<string>();

                    while (reader.Read())
                    {
                        towns.Add((string)reader[0]);
                    }

                    Console.WriteLine($"[{string.Join(", ", towns)}]");
                }
            }
        }
    }
}

