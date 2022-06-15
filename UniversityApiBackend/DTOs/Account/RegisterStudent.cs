using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UniversityApiBackend.DTOs.Students;

namespace UniversityApiBackend.DTOs.Account
{
    public class RegisterStudent
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        public DateTime Dob { get; set; }
        
        public string Street { get; set; }= string.Empty;
        public string City { get; set; }= string.Empty;
        public string State { get; set; }= string.Empty;
        public string ZipCode { get; set; }= string.Empty;
        public string Country { get; set; }= string.Empty;
        public string Comunity { get; set; }= string.Empty;

        public List<StudentCourseDTO> Courses { get; set; } = new List<StudentCourseDTO>();
    }
}