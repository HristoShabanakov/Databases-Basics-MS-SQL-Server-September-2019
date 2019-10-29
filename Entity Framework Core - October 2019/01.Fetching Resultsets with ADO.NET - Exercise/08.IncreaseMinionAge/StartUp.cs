namespace _08.IncreaseMinionAge
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;

    class StartUp
    {
        private static string connectionString =
        "Server=SHABBY\\SQLEXPRESS;" +
        "Database=MinionsDB;" +
        "Integrated Security=true;";

        private static SqlConnection connection = new SqlConnection(connectionString);
        static void Main()
        {
            var minions = Console.ReadLine().Split().Select(int.Parse).ToArray();

            using (connection)
            {
                connection.Open();

                foreach (var id in minions)
                {
                    UpdateMinion(connection, id);
                }

                GetAllMinions(connection);
            }
        }

        private static void GetAllMinions(SqlConnection connection)
        {
            string getallMinions = @"SELECT Name, Age FROM Minions";

            using (SqlCommand command = new SqlCommand(getallMinions, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = (string)reader[0];
                        int age = (int)reader[1];

                        Console.WriteLine($"{name} - {age}");
                    }
                }
            }
        }

        private static void UpdateMinion(SqlConnection connection, int id)
        {
            string updateQuery = @"UPDATE Minions
                                   SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), 
                                   Age += 1
                                   WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
