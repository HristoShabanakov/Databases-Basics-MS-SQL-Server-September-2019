using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicHub.Data.Models
{
    public class Writer
    {
        public Writer()
        {
            this.Songs = new HashSet<Song>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(3),MaxLength(20)]
        public string Name { get; set; }

        //text, consisting of two words separated with space 
        //and each word must start with one upper letter and continue with many lower-case letters 
        //(Example: "Freddie Mercury")

        [RegularExpression("[A-Z][a-z]+ [A-Z][a-z]+")]
        public string Pseudonym { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}
