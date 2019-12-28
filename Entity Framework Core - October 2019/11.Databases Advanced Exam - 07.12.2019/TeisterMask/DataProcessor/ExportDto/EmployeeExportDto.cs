using System.ComponentModel.DataAnnotations;

namespace TeisterMask.DataProcessor.ExportDto
{
    public class EmployeeExportDto
    {
        [Required]
        [MinLength(3), MaxLength(40)]
        [RegularExpression("^[A-Za-z0-9.\\s_-]+$")]
        public string Username { get; set; }

        public ExportTaskDto[] Tasks { get; set; }
    }

   
}
