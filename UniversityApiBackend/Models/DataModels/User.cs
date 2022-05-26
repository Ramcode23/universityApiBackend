using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityApiBackend.Models.DataModels
{
   
    public class User:IdentityUser
    {
        [Required, StringLength(50)]
        public string  Name { get; set; }= string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; }= string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

    }
}
