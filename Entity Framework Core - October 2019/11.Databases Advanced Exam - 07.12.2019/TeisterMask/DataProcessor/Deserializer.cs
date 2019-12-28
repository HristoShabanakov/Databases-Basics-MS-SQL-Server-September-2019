namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using TeisterMask.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using System.Text;
    using TeisterMask.Data.Models;
    using System.Linq;
    using System.Xml.Serialization;
    using System.IO;
    using System.Globalization;
    using TeisterMask.Data.Models.Enums;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employeeDto = JsonConvert.DeserializeObject<EmployeeImportDto[]>(jsonString);

            var sb = new StringBuilder();

            var validEmployees = new List<Employee>();

            foreach (var dto in employeeDto)
            {
                if (IsValid(dto))
                {
                    var employee = new Employee
                    {
                        Username = dto.Username,
                        Email = dto.Email,
                        Phone = dto.Phone,
                       

                    };

                    sb.AppendLine(string.Format(SuccessfullyImportedEmployee, dto.Username));
                    validEmployees.Add(employee);
                }

                else
                {
                    sb.AppendLine(string.Format(ErrorMessage));
                }
            }

            //context.Employees.AddRange(validEmployees);
            //context.SaveChanges();

            var result = sb.ToString().TrimEnd();

            return result;
        }
        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProjectsImportDto[])
                , new XmlRootAttribute("Projects"));

            var allProjects = (ProjectsImportDto[])serializer.Deserialize(new StringReader(xmlString));

            var validProjects = new List<Project>();

            var sb = new StringBuilder();

            foreach (var dto in allProjects)
            {
                var isValid = IsValid(dto) &&
                    dto.OpenDate != null &&
                    dto.DueDate != null;


                if (isValid)

                {
                    var project = new Project
                    {
                        Name = dto.Name,
                        OpenDate = DateTime.ParseExact(dto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        DueDate = DateTime.ParseExact(dto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Tasks = dto.Tasks.Select(t => new Task
                        {
                            Name = t.Name,
                            OpenDate = DateTime.ParseExact(t.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            DueDate = DateTime.ParseExact(t.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            ExecutionType = (ExecutionType)Enum.Parse(typeof(ExecutionType), t.ExecutionType),
                            LabelType = (LabelType)Enum.Parse(typeof(LabelType), t.LabelType)
                        })
                        .ToArray()
                    };

                    validProjects.Add(project);
                    sb.AppendLine(string.Format(SuccessfullyImportedProject, dto.Name, dto.Tasks.Count()));
                }

                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Projects.AddRange(validProjects);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();

            return result;
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}