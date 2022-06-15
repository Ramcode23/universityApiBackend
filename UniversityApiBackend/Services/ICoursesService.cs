using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApiBackend.DTOs;
using UniversityApiBackend.DTOs.Chapters;
using UniversityApiBackend.DTOs.Courses;
using UniversityApiBackend.Models;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICoursesService:IBaseService<Course>
    {
       // search by name categopry, range of students
        IQueryable<CourseDTO> SearchCourses(CourseFindDTO courseFindDTO);
        IQueryable<CourseDTO> GetAllCourseList(int pageNumber, int resultsPage);
       
   

    }
}