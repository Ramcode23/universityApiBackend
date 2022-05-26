using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IStudentService:IBaseService<Student>
    {
        IQueryable<Student> GetStudentsWithCoursesAsync(int pageNumber, int resultsPage);

        IQueryable<Student> GetStudentsWithNoCoursesAsync(int pageNumber, int resultsPage);

        
    }
}
