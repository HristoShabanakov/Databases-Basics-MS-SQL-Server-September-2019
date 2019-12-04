namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
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

            var validHalls = new List<HallImportDto>();

            foreach (var dto in allHalls)
            {
                if (IsValid(dto))
                {
                    var hall = new Hall
                    {
                        Name = dto.Name,
                        Is4Dx = dto.Is4Dx,
                        Is3D = dto.Is3D,
                        Seats = dto.Seats
                    };

                    validHalls.Add(hall);
                    sb.AppendLine($"Successfully imported {dto.Name}() with {dto.Name.Count()} seats!");
                }

                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Halls.AddRange(validHalls);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            throw new NotImplementedException();
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