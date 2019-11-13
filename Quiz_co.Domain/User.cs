using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quiz_co.Domain
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime Joined { get; set; }
        public string ImageUrl { get; set; }

        public virtual IEnumerable<Quiz> Quizzes { get; set; }
    }
}
