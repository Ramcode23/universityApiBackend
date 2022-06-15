using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.DTOs.Students
{
    public class StudentCreateDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime Dob { get; set; }

        public UserDTO User { get; set; }= new UserDTO();
        public List<StudentCourseDTO> Courses { get; set; }

        public AddressDTO Address { get; set; }= new AddressDTO();
    }
}
