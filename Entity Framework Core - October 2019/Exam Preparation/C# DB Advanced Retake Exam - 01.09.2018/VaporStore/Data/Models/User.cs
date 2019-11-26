using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Data.Models
{
    public class User
    {
        public User()
        {
            this.Cards = new HashSet<Card>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)] // text with length[3,20].
        public string Username { get; set; } 

        //text which has two words(latin) Both start with upper letter, separated by space (John Smith)
        //^ - asserts position at start of a line, $ - asserts position at the end of a line.
        [Required]
        [RegularExpression("^[A-Z][a-z]+ [A-Z][a-z]+$")] 
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(3,103)]
        public int Age { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
