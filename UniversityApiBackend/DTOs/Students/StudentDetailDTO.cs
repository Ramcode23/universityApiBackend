using UniversityApiBackend.DTOs.Courses;

namespace UniversityApiBackend.DTOs.Students
{
    public class StudentDetailDTO
    {
        public int Id { get; set; }
      
        public DateTime Dob { get; set; }
        public UserDTO User { get; set; } = new UserDTO();
        public AddressDTO Address { get; set; } = new AddressDTO();

        public List<CourseListDTO> Courses { get; set; }
    }
}
