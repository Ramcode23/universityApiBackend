using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.DTOs.Courses
{
    public class CourseListDTO
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}

