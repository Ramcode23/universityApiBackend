using System.ComponentModel.DataAnnotations;
using UniversityApiBackend.DTOs.Categories;
using UniversityApiBackend.DTOs.Chapters;

using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DTOs.Courses
{
    public class CourseCreateDTO
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string PublicGoal { get; set; } = string.Empty;

        public Level Levels { get; set; } = Level.Basic;

public List<CategoryCourseDTO> Categories { get; set; }             


    }
}
