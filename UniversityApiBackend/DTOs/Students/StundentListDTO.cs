using UniversityApiBackend.DTOs.Courses;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DTOs.Students
{
    public class StundentListDTO
    {
     public int Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
       public int Age { get; set; }

        public string CourseName { get; set; }
     
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? Comunity { get; set; }
         public string? Category { get; set; }

    }
}
