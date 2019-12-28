namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using TeisterMask.DataProcessor.ImportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context
                .Projects
                .Where(p => p.Tasks.Count > 0)
                .Select(p => new ProjectExportDto
                {
                    TasksCount = p.Tasks.Count,
                    ProjectName = p.Name,
                    HasEndDate = p.DueDate == null ? "No" : "Yes",
                    Tasks = p.Tasks.Select(t => new TaskExportDto
                    {
                        Name = t.Name,
                        Label = t.LabelType.ToString()
                    })
                    .OrderBy(t => t.Name)
                    .ToArray()
                })
                .OrderByDescending(p => p.TasksCount)
                .ThenBy(p => p.ProjectName)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ProjectExportDto[]), 
                new XmlRootAttribute("Projects"));

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), projects, namespaces);

            return sb.ToString().TrimEnd();
        }


        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            // Select employee who have at least 1 task that its OpenDate its after or 
            //equal to the given date(parameter comming from the function).
            var employees = context
                .Employees
                .Where(e => e.EmployeesTasks.Any(t => t.Task.OpenDate >= date))
                .OrderByDescending(e => e.EmployeesTasks.Count(t => t.Task.OpenDate >= date))
                .ThenBy(e => e.Username)
                .Select(e => new EmployeeExportDto
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                    .Where(t => t.Task.OpenDate >= date)
                    .Select(t => new ExportTaskDto
                    {
                        TaskName = t.Task.Name,
                        OpenDate = t.Task.OpenDate.ToString(@"d", CultureInfo.InvariantCulture),
                        DueDate = t.Task.DueDate.ToString(@"d", CultureInfo.InvariantCulture),
                        LabelType = t.Task.LabelType.ToString(),
                        ExecutionType = t.Task.ExecutionType.ToString()
                    })
                    .OrderByDescending(t => DateTime.ParseExact(t.DueDate, @"d", CultureInfo.InvariantCulture))
                    .ThenBy(t => t.TaskName)
                    .ToArray()
                })
                .Take(10)
                .ToArray();
                

            var json = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return json;
        }

    }
}
