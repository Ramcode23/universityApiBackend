using UniversityApiBackend.DTOs.Courses;

namespace UniversityApiBackend.DTOs.Students
{
    public class StudentDetailDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Dob { get; set; }

        public AddressDTO Adress { get; set; } = new AddressDTO();

        public List<CourseDTO> Courses { get; set; }
    }
}
