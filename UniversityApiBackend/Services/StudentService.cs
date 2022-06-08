using System.Linq;
using LinqSnippets;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTOs.Account;
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
            var students = _context.Students.Where(x=>x.IsDeleted==false|| x.IsDeleted==null)
            .AsQueryable();
            if (studentFindDTO.FirstName != null)
               students = students.Where(e => e.User.Name.Contains(studentFindDTO.FirstName.ToLower()));
            if (studentFindDTO.LastName != null)
                students = students.Where(e => e.User.LastName.Contains( studentFindDTO.LastName.ToLower()));
            if (studentFindDTO.courseName != null)
                students = students.Where(e => e.Courses.Any(c => c.Name.Contains( studentFindDTO.courseName.ToLower())));
            if (studentFindDTO.CourseCategory != null)
                students = students.Where(e => e.Courses.Any(c => c.Categories.Where(ct=>ct.Name.ToLower().Contains( studentFindDTO.CourseCategory.ToLower())).Any()));
            if (studentFindDTO.RangeAge != null && studentFindDTO.RangeAge.Any(x=>x>0))

                students = students.Where(e => (e.Dob.Year-DateTime.Now.Year) >= studentFindDTO.RangeAge[0] && (e.Dob.Year-DateTime.Now.Year) <= studentFindDTO.RangeAge[1]);
            if (studentFindDTO.Street != null)
                students = students.Where(e => e.Address.Street.ToLower().Contains(studentFindDTO.Street.ToLower()));
            if (studentFindDTO.City != null)
                students = students.Where(e => e.Address.City.ToLower().Contains(studentFindDTO.City.ToLower()));
            if (studentFindDTO.State != null)
                students = students.Where(e => e.Address.State.ToLower().Contains(studentFindDTO.State.ToLower()));
            if (studentFindDTO.ZipCode != null)
                students = students.Where(e => e.Address.ZipCode.ToLower().Contains(studentFindDTO.ZipCode.ToLower()));
            if (studentFindDTO.Country != null)
                students = students.Where(e => e.Address.Country.ToLower().Contains(studentFindDTO.Country.ToLower()));
            if (studentFindDTO.Comunity != null)
                students = students.Where(e => e.Address.Comunity.ToLower().Contains(studentFindDTO.Comunity.ToLower()));
          var use=  students.Count();
            return students.Select(e => new StundentListDTO
            {
                Id = e.Id,
                FirstName = e.User.Name,
                LastName = e.User.LastName,
                Email = e.User.UserName,
                Age = DateTime.Now.Year - e.Dob.Year,
                Dob = e.Dob,
                CourseName = e.Courses.FirstOrDefault().Name,
                Street = e.Address.Street,
                City = e.Address.City,
                State = e.Address.State,
                ZipCode = e.Address.ZipCode,
                Country = e.Address.Country,
                Comunity = e.Address.Comunity,
                Category = e.Courses.FirstOrDefault().Categories.FirstOrDefault().Name
            });
        }

        public IQueryable<Student> GetAll()
        {
            var students = _context.Students
           .Include(s => s.Courses)
          .AsQueryable();
            return students;
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

        public IQueryable<StundentListDTO> GetStudentsWithCoursesAsync(int pageNumber=1, int resultsPage=10)
        {
            var students = _context.Students.Where(x => x.IsDeleted == false || x.IsDeleted == null)
            .Include(s => s.Courses)
            .Include(s => s.User)
            .OrderBy(s => s.User.Name)
            .ThenBy(s => s.User.LastName)
            .Select(e => new StundentListDTO
            {
                Id = e.Id,
                FirstName = e.User.Name,
                LastName = e.User.LastName,
                Email = e.User.UserName,
                Age = DateTime.Now.Year - e.Dob.Year,
                Dob = e.Dob,
                CourseName = e.Courses.FirstOrDefault().Name,
                Street = e.Address.Street,
                City = e.Address.City,
                State = e.Address.State,
                ZipCode = e.Address.ZipCode,
                Country = e.Address.Country,
                Comunity = e.Address.Comunity,
                Category = e.Courses.FirstOrDefault().Categories.FirstOrDefault().Name

            })  
            .AsQueryable();
            var tes = students.Count();
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

        public Task Update(RegisterStudent entity,User user)
        {


            try
            {

                var student = _context.Students.Include(x=>x.UpdatedBy)
                    .Include(x=>x.Address)
                    .Include(x=>x.User)
                    .FirstOrDefault(es => es.Id == entity.Id);
                student.User.Name = entity.FirstName;
                student.User.LastName = entity.LastName;
                student.Address.City = entity.City;
                student.Address.State = entity.State;
                student.Address.ZipCode = entity.ZipCode;
                student.Address.Country = entity.Country;
                student.Address.Comunity = entity.Comunity;
                student.UpdatedAt = DateTime.Now;
                student.UpdatedBy = user;
                _context.Students.Update(student);
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

          
        }
        public Task Update(Student entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _context.Students.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
