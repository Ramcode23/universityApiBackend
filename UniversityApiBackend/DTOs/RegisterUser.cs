using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.DTOs
{
    public class RegisterUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }=String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; } = String.Empty;

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; } = String.Empty;



    }
}
