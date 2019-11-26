namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.ImportDtos;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
            var gamesDto = JsonConvert.DeserializeObject<ImportGameDto[]>(jsonString);

            var sb = new StringBuilder();

            foreach (var gameDto in gamesDto)
            {
                if (!IsValid(gameDto) || gameDto.Tags.Count == 0)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var developer = GetDeveloper(context, gameDto.Developer);
                var genre = GetGenre(context, gameDto.Genre);

                foreach (var currentTag in gameDto.Tags)
                {
                    var tag = GetTag(context, currentTag);
                }
            }

            return "";
		}

        private static object GetTag(VaporStoreDbContext context, string currentTag)
        {
            throw new NotImplementedException();
        }

        private static object GetGenre(VaporStoreDbContext context, string gameDtogenre)
        {
            var genre = context.Genres.FirstOrDefault(x => x.Name == gameDtogenre);

            if (genre == null)
            {
                genre = new Genre
                {
                    Name = gameDtogenre
                };

                context.Genres.Add(genre);
                context.SaveChanges();
            }

            return genre;
        }

        private static object GetDeveloper(VaporStoreDbContext context, string gameDtodeveloper)
        {
            var developer = context.Developers.FirstOrDefault(x => x.Name == gameDtodeveloper);

            if(developer == null)
            {
                developer = new Developer
                {
                    Name = gameDtodeveloper
                };

                context.Developers.Add(developer);
                context.SaveChanges();
            }

            return developer;
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			throw new NotImplementedException();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
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