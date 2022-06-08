using System.ComponentModel.DataAnnotations;
using UniversityApiBackend.DTOs.Chapters;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DTOs.Courses
{
    public class CourseDTO
    {

        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(280)]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        public int Students { get; set; } 


       

    }
}
