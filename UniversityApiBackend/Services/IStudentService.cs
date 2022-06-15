using UniversityApiBackend.DTOs.Account;
using UniversityApiBackend.DTOs.Students;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IStudentService : IBaseService<Student>
    {
        IQueryable<StundentListDTO> GetStudentsWithCoursesAsync(int pageNumber, int resultsPage);

        IQueryable<StundentListDTO> FindStudentsAsync(StudentFindDTO studentFindDTO);


  
    }
}
