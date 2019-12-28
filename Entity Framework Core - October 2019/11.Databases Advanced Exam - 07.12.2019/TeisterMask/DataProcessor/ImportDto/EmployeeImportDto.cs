using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.DataProcessor.ImportDto
{
    public class EmployeeImportDto
    {
        [Required]
        [MinLength(3), MaxLength(40)]
        [RegularExpression("^[A-Za-z0-9.\\s_-]+$")]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^[0-9]{3}-[0-9]{3}-[0-9]{4}$")]
        public string Phone { get; set; }

        public EmployeeTaskDto[] Tasks { get; set; } 
    }

    public class EmployeeTaskDto
    {
        public string TaskId { get; set; }
    }
}
