namespace _06.RemoveVillain
{
    using System;
    using System.Data.SqlClient;

    class StartUp
    {
        private static string connectionString =
            "Server=Server=SHABBY\\SQLEXPRESS;" +
            "Database=MinionsDB;" +
            "Integrated Security=true;";

        private static SqlConnection connection = new SqlConnection(connectionString);

        private static SqlTransaction transaction ;

        static void Main()
        {
            int id = int.Parse(Console.ReadLine());

            connection.Open();

            using (connection)
            {
                transaction = connection.BeginTransaction();

                try
                {

                SqlCommand command = new SqlCommand();
                //set the connection and transaction and defining the command.
                command.Connection = connection;
                command.Transaction = transaction;
                command.CommandText = @"SELECT Name FROM Villains WHERE Id = @villainId";
                //Escape Sql Injection by adding a parameter.
                command.Parameters.AddWithValue("@villainId", id);

                object value = command.ExecuteScalar();

                if (value == null)
                {
                        throw new ArgumentException("No such villain was found.");
                }
                    string villainName = (string)value;

                    command.CommandText = @"DELETE FROM MinionsVillains 
                                          WHERE VillainId = @villainId";

                    int minionsDeleted = command.ExecuteNonQuery();

                    command.CommandText = @"DELETE FROM Villains
                                          WHERE Id = @villainId";

                    command.ExecuteNonQuery();

                    transaction.Commit();

                    Console.WriteLine($"{villainName} was deleted.");
                    Console.WriteLine($"{minionsDeleted} minions were released.");
                }
                catch (ArgumentException ane)
                {
                    try
                    {
                        Console.WriteLine(ane.Message);
                        transaction.Rollback();
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e.Message);
                    }
                }

                catch (Exception e)
                {
                    try
                    {
                        Console.WriteLine(e.Message);
                        transaction.Rollback();
                    }
                    catch (Exception re)
                    {
                        Console.WriteLine(re.Message);
                    }
                }
            }
        }
    }
}
