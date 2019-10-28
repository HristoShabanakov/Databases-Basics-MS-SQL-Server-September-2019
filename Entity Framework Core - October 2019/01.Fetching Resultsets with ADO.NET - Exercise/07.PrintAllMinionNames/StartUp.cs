namespace _07.PrintAllMinionNames
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    class StartUp
    {
        static void Main()
        {
            string connectionString = "Server=SHABBY\\SQLEXPRESS;Database=MinionsDB;Integrated Security=True;";
            var connection = new SqlConnection(connectionString);

            connection.Open();

            using (connection)
            {
                try
                {
                    var command = new SqlCommand("SELECT [Name] FROM Minions", connection);
                    var reader = command.ExecuteReader();
                    var names = new List<string>();

                    using (reader)
                    {
                        try
                        {
                            while (reader.Read())
                            {
                                names.Add(Convert.ToString(reader[0]));
                            }
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e.Message);
                        }
                    }

                    for (int i = 0; i <= names.Count / 2; i++)
                    {
                        Console.WriteLine(names[i]);
                        if(i != names.Count - 1  - i)
                        {
                            Console.WriteLine(names[names.Count - 1 - i]);
                        }
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
