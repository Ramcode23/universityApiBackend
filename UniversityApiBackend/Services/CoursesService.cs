using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqSnippets;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly UniversityDbContext _context;
        public CoursesService(UniversityDbContext context)
        {
            _context = context;
        }

        public Task Add(Course entity)
        {
            _context.Courses.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var course = _context.Courses.Find(id);
            course.IsDeleted = true;
            _context.Courses.Update(course);
            return _context.SaveChangesAsync();
        }

        public bool Exists(int Id)
        {
            return _context.Courses.Any(c => c.Id == Id);
        }

        public IQueryable<Course> GetAll(int pageNumber, int resultsPage)
        {
            var courses = _context.Courses.OrderBy(c => c.Name).AsQueryable();
            return Paginator.GetPage(courses, pageNumber, resultsPage);
        }

        public Task<Course?> GetById(int id)
        {
            return _context.Courses
           .Where(c => c.Id == id)
           .FirstOrDefaultAsync();
        }

        public Task Update(Course entity)
        {
            _context.Courses.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}