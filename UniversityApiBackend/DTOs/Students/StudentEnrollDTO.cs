using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApiBackend.DTOs.Courses;

namespace UniversityApiBackend.DTOs.Students
{
    /* add list course  to student*/
    public class StudentEnrollDTO
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        
    }
}