using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityApiBackend.Models.DataModels
{
    public class Category:BaseEntity
    {
      
        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Course> Courses{ get; set; }= new List<Course>();
    }
}
