using System.Linq;
using LinqSnippets;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTOs.Students;
using UniversityApiBackend.Models.DataModels;
namespace UniversityApiBackend.Services
{
    public class StudentService : IStudentService
    {
        private readonly UniversityDbContext _context;
        public StudentService(UniversityDbContext context)
        {
            _context = context;
        }

        public Task Add(Student entity)
        {
            entity.CreatedAt = DateTime.Now;
            _context.Students.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var student = _context.Students.Find(id);
            student.DeleteteAt = DateTime.Now;
            student.IsDeleted = true;
            _context.Students.Update(student);
            return _context.SaveChangesAsync();
        }

        public Task EnrollAsysnc(Student student)
        {

            _context.Students.Update(student);
            return _context.SaveChangesAsync();
        }

        public bool Exists(int Id)
        {
            return _context.Students.Any(e => e.Id == Id);
        }
        public IQueryable<StundentListDTO> FindStudentsAsync(StudentFindDTO studentFindDTO)
        {
            // select categories courses and students
            var students = _context.Students
            .AsQueryable();
            if (studentFindDTO.FirstName != null)
               students = students.Where(e => e.User.Name == studentFindDTO.FirstName);
            if (studentFindDTO.LastName != null)
                students = students.Where(e => e.User.LastName == studentFindDTO.LastName);
            if (studentFindDTO.course != null)
                students = students.Where(e => e.Courses.Any(c => c.Name == studentFindDTO.course));
            if (studentFindDTO.CourseCategory != null)
                students = students.Where(e => e.Courses.Any(c => c.Categories.Where(ct=>ct.Name.Contains( studentFindDTO.CourseCategory)).Any()));
            if (studentFindDTO.RangeAge != null)

                students = students.Where(e => (e.Dob.Year-DateTime.Now.Year) >= studentFindDTO.RangeAge[0] && (e.Dob.Year-DateTime.Now.Year) <= studentFindDTO.RangeAge[1]);
            if (studentFindDTO.Street != null)
                students = students.Where(e => e.Address.Street == studentFindDTO.Street);
            if (studentFindDTO.City != null)
                students = students.Where(e => e.Address.City == studentFindDTO.City);
            if (studentFindDTO.State != null)
                students = students.Where(e => e.Address.State == studentFindDTO.State);
            if (studentFindDTO.ZipCode != null)
                students = students.Where(e => e.Address.ZipCode == studentFindDTO.ZipCode);
            if (studentFindDTO.Country != null)
                students = students.Where(e => e.Address.Country == studentFindDTO.Country);
            if (studentFindDTO.Comunity != null)
                students = students.Where(e => e.Address.Comunity == studentFindDTO.Comunity);
            
            return students.Select(e => new StundentListDTO
            {
                 Id = e.Id,
                FirstName = e.User.Name,
                LastName = e.User.LastName,
                Age = e.Dob.Year - DateTime.Now.Year,
                CourseName = e.Courses.FirstOrDefault().Name,
                Street = e.Address.Street,
                City = e.Address.City,
                State = e.Address.State,
                ZipCode = e.Address.ZipCode,
                Country = e.Address.Country,
                Comunity = e.Address.Comunity,
                Category=e.Courses.FirstOrDefault().Categories.FirstOrDefault().Name
            });
        }

        public IQueryable<Student> GetAll(int pageNumber, int resultsPage)
        {
            var students = _context.Students
           .Include(s => s.Courses)
           //./*OrderBy(s => s.FirstName)*/
           ////.ThenBy(s => s.LastName)
          .AsQueryable();
            return Paginator.GetPage(students, pageNumber, resultsPage);
        }

        public Task<Student?> GetById(int id)
        {
            return _context.Students
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();
        }

        public IQueryable<Student> GetStudentsByRangeAgeAsync(int[] rangeAge)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StundentListDTO> GetStudentsWithCoursesAsync(int pageNumber, int resultsPage)
        {
            var students = _context.Students
            .Include(s => s.Courses)
            .Include(s => s.User)
            .OrderBy(s => s.User.Name)
            .ThenBy(s => s.User.LastName)
            .Select(e => new StundentListDTO
            {
                Id = e.Id,
                FirstName = e.User.Name,
                LastName = e.User.LastName,
                Age = e.Dob.Year - DateTime.Now.Year,
                CourseName = e.Courses.FirstOrDefault().Name,
                Street = e.Address.Street,
                City = e.Address.City,
                State = e.Address.State,
                ZipCode = e.Address.ZipCode,
                Country = e.Address.Country,
                Comunity = e.Address.Comunity,
                Category=e.Courses.FirstOrDefault().Categories.FirstOrDefault().Name

            })  
            .AsQueryable();
            return Paginator.GetPage(students, pageNumber, resultsPage);


        }

        public IQueryable<Student> GetStudentsWithNoCoursesAsync(int pageNumber, int resultsPage)
        {
            var students = _context.Students
            .Where(s => s.Courses.Count == 0)
            //.OrderBy(s => s.FirstName)
            //.ThenBy(s => s.LastName)
            .AsQueryable();

            return Paginator.GetPage(students, pageNumber, resultsPage);
        }

        public Task Update(Student entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _context.Students.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
