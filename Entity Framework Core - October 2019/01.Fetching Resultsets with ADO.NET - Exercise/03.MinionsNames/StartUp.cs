
namespace _03.MinionsNames
{
    using System;
    using System.Data.SqlClient;
    class StartUp
    {
        static void Main()
        {
            string connectionString = "Server=SHABBY\\SQLEXPRESS;Database=MinionsDB;Integrated Security=True;";
            var connection = new SqlConnection(connectionString);

            int villainId = int.Parse(Console.ReadLine());

            connection.Open();

            using (connection)
            {
                string villainQuery = "SELECT [Name] FROM Villains WHERE Id = @villainId";
                var villainCommand = new SqlCommand(villainQuery, connection);
                villainCommand.Parameters.AddWithValue("@villainId", villainId);

                var reader = villainCommand.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Villain:{reader["Name"]}");
                }
                reader.Close();

                string minionsQuery = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                      FROM MinionsVillains AS mv
                                      JOIN Minions As m ON mv.MinionId = m.Id
                                      WHERE mv.VillainId = @villainId
                                      ORDER BY m.Name";
                var minionsCommand = new SqlCommand(minionsQuery, connection);
                minionsCommand.Parameters.AddWithValue("@villainId", villainId);
                reader = minionsCommand.ExecuteReader();

                int counter = 1;

                while (reader.Read())
                {
                    Console.WriteLine($"{counter} {reader["Name"]} {reader["Age"]}");
                    counter++;
                }
            }
        }
    }
}
