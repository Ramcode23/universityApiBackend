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


        [InverseProperty("CreatedBy")]
        public ICollection<Student> StudentsCreatedBy { get; set; } = new List<Student>();

        [InverseProperty("UpdatedBy")]
        public ICollection<Student> StudentsUpdatedBy { get; set; } = new List<Student>();

        [InverseProperty("DeletedBy")]
        public ICollection<Student> StudentsDeletedBy { get; set; } = new List<Student>();


        [InverseProperty("CreatedBy")]
        public ICollection<Category> CategoriesCreatedBy { get; set; } = new List<Category>();

        [InverseProperty("UpdatedBy")]
        public ICollection<Category> CategoriesUpdatedBy { get; set; } = new List<Category>();

        [InverseProperty("DeletedBy")]
        public ICollection<Category> CategoriesDeletedBy { get; set; } = new List<Category>();


        [InverseProperty("CreatedBy")]
        public ICollection<Course> CoursesCreatedBy { get; set; } = new List<Course>();

        [InverseProperty("UpdatedBy")]
        public ICollection<Course> CoursesUpdatedBy { get; set; } = new List<Course>();

        [InverseProperty("DeletedBy")]
        public ICollection<Course> CoursesDeletedBy { get; set; } = new List<Course>();


        [InverseProperty("CreatedBy")]
        public ICollection<Chapter> ChaptersCreatedBy { get; set; } = new List<Chapter>();

        [InverseProperty("UpdatedBy")]
        public ICollection<Chapter> ChaptersUpdatedBy { get; set; } = new List<Chapter>();

        [InverseProperty("DeletedBy")]
        public ICollection<Chapter> ChaptersDeletedBy { get; set; } = new List<Chapter>();





        
    }
}
