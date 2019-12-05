namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie 
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat 
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection 
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket 
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var allMovies = JsonConvert.DeserializeObject<MovieImportDto[]>(jsonString);

            var sb = new StringBuilder();

            var validMovies = new List<Movie>();


            foreach (var dtoMovie in allMovies)
            {
                if (IsValid(dtoMovie))
                {
                    var movie = new Movie
                    {
                        Title = dtoMovie.Title,
                        Genre = dtoMovie.Genre,
                        Duration = dtoMovie.Duration,
                        Rating = dtoMovie.Rating,
                        Director = dtoMovie.Director
                    };

                    validMovies.Add(movie);
                    sb.AppendLine(string.Format(SuccessfulImportMovie, dtoMovie.Title, dtoMovie.Genre, dtoMovie.Rating.ToString("F2")));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Movies.AddRange(validMovies);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var allHalls = JsonConvert.DeserializeObject<HallImportDto[]>(jsonString);

            var sb = new StringBuilder();

            //var validHalls = new List<Hall>();

            foreach (var dto in allHalls)
            {
                if (IsValid(dto))
                {
                    var hall = new Hall
                    {
                        Name = dto.Name,
                        Is4Dx = dto.Is4Dx,
                        Is3D = dto.Is3D,
                    };

                    context.Halls.Add(hall);
                    AddSeatsInDatabase(context, hall.Id, dto.Seats);
                    var projectionType = GetProjectionType(hall); // there are 4 possible scenarios because projection type is bool.
                    sb.AppendLine(string.Format(SuccessfulImportHallSeat, dto.Name, projectionType, dto.Seats));
                }

                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.SaveChanges();

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProjectionImportDto[]), 
                new XmlRootAttribute("Projections"));

            var allProjections = (ProjectionImportDto[])serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            //var validProjections = new List<Projection>();

            foreach (var dto in allProjections)
            {
                var isMovieValid = context.Movies.Any(m => m.Id == dto.MovieId);
                var isHallIdValid = context.Halls.Any(h => h.Id == dto.HallId);

                if (IsValid(dto) && isMovieValid && isHallIdValid)
                {
                    var projection = new Projection
                    {
                        MovieId = dto.MovieId,
                        HallId = dto.HallId,
                        DateTime = DateTime.ParseExact(
                            dto.DateTime, 
                            "yyyy-MM-dd HH:mm:ss", 
                            CultureInfo.InvariantCulture)
                    };

                    // in order to access Movie title prop, add the current projection into database
                    context.Projections.Add(projection); 
                    var dateTimeResult = projection.DateTime.ToString("MM/dd/yyyy");
                    sb.AppendLine(string.Format(SuccessfulImportProjection, projection.Movie.Title, dateTimeResult));
                }

                else
                {
                    sb.AppendLine(string.Format(ErrorMessage));
                }
            }

            //context.Projections.AddRange(validProjections);
            context.SaveChanges();
            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        private static string GetProjectionType(Hall hall)
        {
            var result = "Normal";

            if (hall.Is3D && hall.Is4Dx)
                result = "4Dx/3D";
            else if (hall.Is3D)
                result = "3D";
            else if (hall.Is4Dx)
                result = "4Dx";

            return result;
        }

        private static void AddSeatsInDatabase(CinemaContext context, int hallId, int seatCount)
        {
            var seats = new List<Seat>();
                
            for (int i = 0; i < seatCount; i++)
            {
                seats.Add(new Seat { HallId = hallId });
            }

            context.AddRange(seats);
            context.SaveChanges();
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);

            var validationResult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(entity, validationContext, validationResult, true);

            return isValid;
        }
    }
}