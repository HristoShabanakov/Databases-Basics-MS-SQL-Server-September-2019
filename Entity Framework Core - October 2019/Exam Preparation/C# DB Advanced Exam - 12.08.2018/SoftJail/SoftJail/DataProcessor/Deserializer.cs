namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var alldepartments = JsonConvert.DeserializeObject<Department[]>(jsonString);

            var sb = new StringBuilder();
            var validDepartments = new List<Department>();


            foreach (var department in alldepartments)
            {
                var isValid = IsValid(department) && department.Cells.All(IsValid);

                if (isValid)
                {
                    validDepartments.Add(department);
                    sb.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");

                   
                }
                else
                {
                    sb.AppendLine("Invalid Data");
                }
            }

            context.Departments.AddRange(validDepartments);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();

            return  result;
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var allprisoners = JsonConvert.DeserializeObject<ImportPrisonersMailDto[]>(jsonString);

            var sb = new StringBuilder();

            var validPrisoners = new List<Prisoner>();

            foreach (var dto in allprisoners)
            {
                var isValid = IsValid(dto) 
                    && dto.FullName != null
                    && dto.Mails.All(IsValid);

                if(isValid)
                {
                    //In case property DateTime is null.
                    var releaseDate = dto.ReleaseDate == null
                        ? new DateTime?()
                        : DateTime.ParseExact(
                            dto.ReleaseDate,
                            "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);

                    var prisoner = new Prisoner
                    {
                        FullName = dto.FullName,
                        Nickname = dto.Nickname,
                        Age = dto.Age,
                        IncarcerationDate = DateTime.ParseExact(
                            dto.IncarcerationDate,
                            "dd/MM/yyyy",
                            CultureInfo.InvariantCulture),
                        ReleaseDate = releaseDate,
                        Bail = dto.Bail,
                        CellId = dto.CellId,
                        Mails = dto.Mails.Select(m => new Mail
                        {
                            Description = m.Description,
                            Sender = m.Sender,
                            Address = m.Address
                        })
                        .ToArray()

                    };

                    validPrisoners.Add(prisoner);

                    sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
                }

                else
                {
                    sb.AppendLine("Invalid Data");
                }
            }

            context.Prisoners.AddRange(validPrisoners);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(OfficerDto[]),
                new XmlRootAttribute("Officers"));

            var allOfficers = (OfficerDto[])serializer.Deserialize(new StringReader(xmlString));

            var validOfficers = new List<Officer>();

            var sb = new StringBuilder();

            foreach (var dto in allOfficers)
            {
                //Check for officer 
                var isWeaponValid = Enum.TryParse(dto.Weapon, out Weapon weapon);
                var isPositionValid = Enum.TryParse(dto.Weapon, out Position position);
                var isValid = IsValid(dto) && isWeaponValid && isPositionValid;

                //Create one
                if(isValid)
                {
                    var officer = new Officer
                    {
                        FullName = dto.Name,
                        Salary = dto.Money,
                        Position = position,
                        Weapon = weapon,
                        DepartmentId = dto.DepartmentId,
                        OfficerPrisoners = dto.Prisoners
                        //Array full with prisoners ids.
                        .Select(p => new OfficerPrisoner
                        {
                            PrisonerId = p.Id
                        })
                        .ToArray()

                    };
                    

                    //Add & Print.
                    validOfficers.Add(officer);
                    sb.AppendLine($"Imported {officer.FullName} {officer.OfficerPrisoners.Count} prisoners");
                }
            }


            context.Officers.AddRange(validOfficers);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();

            return result;
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResult = new List<ValidationResult>();

            var isValid =  Validator.TryValidateObject(entity, validationContext, validationResult, true);

             return isValid;
        }
    }
}