using System.Linq;
using LinqSnippets;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
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
            _context.Students.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var student = _context.Students.Find(id);
            student.IsDeleted = true;
            _context.Students.Update(student);
            return _context.SaveChangesAsync();
        }

        public bool Exists(int Id)
        {
            return _context.Students.Any(e => e.Id == Id);
        }

        public IQueryable<Student> GetAll(int pageNumber, int resultsPage)
        {
            var students = _context.Students
           .Include(s => s.Courses)
           .OrderBy(s => s.FirstName)
           .ThenBy(s => s.LastName)
           .AsQueryable();
            return Paginator.GetPage(students, pageNumber, resultsPage);
        }

        public Task<Student?> GetById(int id)
        {
            return _context.Students
            .Where(s => s.Id == id)
            .OrderBy(s => s.FirstName)
            .ThenBy(s => s.LastName)
            .FirstOrDefaultAsync();
        }

        public IQueryable<Student?> GetStudentsWithCoursesAsync(int pageNumber, int resultsPage)
        {
            var students = _context.Students
            .Include(s => s.Courses)
            .Where(s => s.Courses.Count > 0)
            .OrderBy(s => s.FirstName)
            .ThenBy(s => s.LastName)
            .AsQueryable();
            return Paginator.GetPage(students, pageNumber, resultsPage);


        }

        public IQueryable<Student> GetStudentsWithNoCoursesAsync(int pageNumber, int resultsPage)
        {
            var students = _context.Students
            .Where(s => s.Courses.Count == 0)
            .OrderBy(s => s.FirstName)
            .ThenBy(s => s.LastName)
            .AsQueryable();

            return Paginator.GetPage(students, pageNumber, resultsPage);
        }

        public Task Update(Student entity)
        {
            _context.Students.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
