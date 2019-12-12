using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicHub.Data.Models
{
    public class Producer
    {
        public Producer()
        {
            this.Albums = new HashSet<Album>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(3), MaxLength(30)]
        public string Name { get; set; }

        //Text consisting of two words separated with space 
        //Each word must start with one upper letter and continue with many lower-case letters 
        //(Example: "Bon Jovi")

        [RegularExpression("[A-Z][a-z]+ [A-Z][a-z]+")]
        public string Pseudonym { get; set; }

        //text, consisting only of three groups (separated by space) 
        //of three digits and starting always with "+359" 
        //(Example: "+359 887 234 267")

        [RegularExpression(@"\+359 [0-9]{3} [0-9]{3} [0-9]{3}")]
        public string PhoneNumber { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}
