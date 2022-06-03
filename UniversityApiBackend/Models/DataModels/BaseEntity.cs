using System.ComponentModel.DataAnnotations;
namespace UniversityApiBackend.Models.DataModels
{
    public class BaseEntity
    {
        //TODO:Modify model
        [Required]
        [Key]
        public int Id { get; set; }
       
        public User CreatedBy { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public User? UpdatedBy { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public User? DeletedBy { get; set; } 
        public DateTime? DeleteteAt { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
