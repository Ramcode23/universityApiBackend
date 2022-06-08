using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Chapter:BaseEntity
    {
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } 

        [Required]
        public ICollection<Lesson> Lessons { get; set; }=new List<Lesson>();
    }
}
