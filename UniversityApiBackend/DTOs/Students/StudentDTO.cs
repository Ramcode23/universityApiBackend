namespace UniversityApiBackend.DTOs.Students
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;



        public string LastName { get; set; } = string.Empty;



        public DateTime Dob { get; set; }
    }
}
