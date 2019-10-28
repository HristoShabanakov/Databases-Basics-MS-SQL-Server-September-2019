﻿namespace _02.VillainNames
{
    using System;
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
            connection.Open();

            using (connection)
            {
                string queryText = @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                                   FROM Villains AS v 
                                   JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                                   GROUP BY v.Id, v.Name 
                                   HAVING COUNT(mv.VillainId) > 3 
                                   ORDER BY COUNT(mv.VillainId)";
                SqlCommand cmd = new SqlCommand(queryText,connection);

                SqlDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["MinionsCount"]}");
                    }
                }
            }
        }
    }
}
