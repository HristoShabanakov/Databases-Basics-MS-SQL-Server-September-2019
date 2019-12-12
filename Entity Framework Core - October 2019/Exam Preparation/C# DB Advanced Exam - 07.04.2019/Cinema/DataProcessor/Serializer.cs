namespace Cinema.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Cinema.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            // Export all movies which have rating more or equal to the given and have at least one projection with sold tickets.

            var movies = context
                .Movies
                .Where(m => m.Rating >= rating && m.Projections.Any(p => p.Tickets.Count >= 1))
                .OrderByDescending(m => m.Rating)
                .ThenByDescending(m => m.Projections.Sum(p => p.Tickets.Sum(t => t.Price)))
                .Select(x => new
                {
                    MovieName = x.Title,
                    Rating = x.Rating.ToString("F2"),
                    TotalIncomes = x.Projections.Sum(p => p.Tickets.Sum(t => t.Price)).ToString("f2"),
                    //For each customer, export its first name, last name and balance formatted to the second digit. 
                    Customers = x.Projections.SelectMany(t => t.Tickets).Select(c => new
                    {
                        FirstName = c.Customer.FirstName,
                        LastName = c.Customer.LastName,
                        Balance = c.Customer.Balance.ToString("F2"),
                    })
                .OrderByDescending(c => c.Balance)
                .ThenBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .ToArray()
                })
                .Take(10)
                .ToArray();
            var jsonString = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);

            return jsonString;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var customers = context
                .Customers
                .Where(c => c.Age >= age)
                .OrderByDescending(x => x.Tickets.Sum(p => p.Price))
                .Take(10)
                .Select(c => new CustomerExportDto
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    //To find the spent money we need to find the total sum of all tickets.
                    SpentMoney = c.Tickets.Sum(p => p.Price).ToString("f2"),
                    //spent time (in format: "hh\:mm\:ss").
                    SpentTime = TimeSpan.FromSeconds(c.Tickets.Sum(p => p.Projection.Movie.Duration.TotalSeconds)).ToString(@"hh\:mm\:ss")
                })
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(CustomerExportDto[]), new XmlRootAttribute("Customers"));

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), customers, namespaces);

            return sb.ToString().TrimEnd();
        }
    }
}