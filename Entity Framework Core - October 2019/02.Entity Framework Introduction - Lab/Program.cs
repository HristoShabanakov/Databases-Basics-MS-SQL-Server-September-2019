using System;
using System.Linq;
using DatabaseDemoEFCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseDemoEFCore
{
    class Program
    {
        static void Main()
        {
            using (var db = new SoftUniDbContext())
            {
                //01 Order by FirstName, LastName adn JobTitle
                var employees = db.Employees.Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle
                })
                   .OrderBy(e => e.JobTitle)
                    .ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} is working as {employee.JobTitle}");
                }

                // 02 Select current employee from Database
                var employeeToFire = db.Employees
                     .Include(e => e.Department)
                     .FirstOrDefault(e => e.FirstName == "Guy" &&
                     e.LastName == "Gilbert");


                //Cascade insert 
                var town = new Town()
                {
                    Name = "Tarnovo"
                };

                var address = new Address()
                {
                    AddressText = "CodingStreet"
                };

                town.Addresses.Add(address);

                db.Towns.Add(town);

                db.SaveChanges();

            }
        }
    }
}
