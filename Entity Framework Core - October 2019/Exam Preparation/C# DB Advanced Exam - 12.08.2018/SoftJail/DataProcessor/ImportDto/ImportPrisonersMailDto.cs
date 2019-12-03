using SoftJail.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportPrisonersMailDto
    {
        [Required]
        [MinLength(3), MaxLength(20)]
        public string FullName { get; set; }

        [Required]
        [RegularExpression("^The [A-Z][a-z]+$")] //
        public string Nickname { get; set; }

        //[Required]
        [Range(18, 65)]
        public int Age { get; set; }

        //Required is needed for this property because Release Date can be null.
        [Required]
        public string IncarcerationDate { get; set; }
        //When we have a DateTime in the original object in Dto has to be changed to string.
        public string ReleaseDate { get; set; }

        [Range(typeof(decimal), "0.00", "79228162514264337593543950335")]
        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        public MailDto[] Mails { get; set; }
    }

    public class MailDto 
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s]+ str.$")] //(Example: “62 Muir Hill str.“)
        public string Address { get; set; }
    }

}
