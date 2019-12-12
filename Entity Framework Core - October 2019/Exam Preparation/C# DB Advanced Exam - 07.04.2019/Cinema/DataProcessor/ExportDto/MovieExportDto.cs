using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.DataProcessor.ExportDto
{
    public class MovieExportDto
    {
        [Required]
        [MinLength(3), MaxLength(20)]
        public string MovieName { get; set; } 

        [Required]
        [Range(1, 10)]
        public double Rating { get; set; }

        public string TotalIncomes { get; set; }

        public CustomersDto[] Customers { get; set; }

    }

    public class CustomersDto
    {
        [Required]
        [MinLength(3), MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string LastName { get; set; }

        public string Balance { get; set; }
    }
}
