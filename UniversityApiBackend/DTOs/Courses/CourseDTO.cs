using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DTOs.Courses
{
    public class CourseDTO
    {
      
        public string Name { get; set; } = string.Empty;

       
        public string ShortDescription { get; set; } = string.Empty;
       
        public string Description { get; set; } = string.Empty;

     
        public string PublicGoal { get; set; } = string.Empty;


        public Level Levels { get; set; } = Level.Basic;

    }
}
