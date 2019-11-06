namespace SoftUni
{
    using SoftUni.Data;
    using SoftUni.Models;
    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var context = new SoftUniContext())
            {
                string result = GetAddressesByTown(context);

                Console.WriteLine(result);
            }
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var addresses = context
                .Addresses
                .Select(a => new
                {
                    AddressText = a.AddressText,
                    TownName = a.Town.Name,
                    EmployeesCount = a.Employees.Count
                })
                .OrderByDescending(e => e.EmployeesCount)
                .ThenBy(e => e.TownName)
                .ThenBy(e => e.AddressText)
                .Take(10)
                .ToList();

            foreach (var a in addresses)
            {
                stringBuilder
                    .AppendLine($"{a.AddressText}, {a.TownName} - {a.EmployeesCount} employees");
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
