using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApiBackend.DTOs.Courses;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICoursesService:IBaseService<Course>
    {
       // search by name categopry, range of students
        IQueryable<Course> SearchCourses(CourseFindDTO courseFindDTO);
         Task AddCatetory(int courseId, int[] categoriesId);
        Task RemoveCatetory( int[] categoriesId);
        Task AddChapter(int courseId,string List);
        Task RemoveChapter(int chaptersId);

    }
}