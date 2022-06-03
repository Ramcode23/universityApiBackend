using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityApiBackend.DTOs.Courses
{
    public class CourseFindDTO
    {
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public int[]? RangeStudents  { get; set; }
    }
}