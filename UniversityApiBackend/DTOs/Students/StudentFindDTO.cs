namespace UniversityApiBackend.DTOs.Students
{


    /* 
 class  with properties FirstName, LastName, Dob, Courses, Adress
   range of age  course adress  and category   
     */
    public class StudentFindDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string? course { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? Comunity { get; set; }

        public int[]? RangeAge { get; set; }=new int[2];
        public string?  CourseCategory { get; set; }


    }
}
