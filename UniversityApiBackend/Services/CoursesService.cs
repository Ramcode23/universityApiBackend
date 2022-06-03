using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqSnippets;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTOs.Courses;
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
            var categories = _context.Categories.Where(c => entity.Categories.Select(x => x.Id).Contains(c.Id)).ToList();
            if (entity.Categories.Any())
            {
                entity.Categories = categories;
            }else{
                entity.Categories = new List<Category>();
            }       
            
            entity.CreatedAt = DateTime.Now;
            _context.Courses.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task AddCatetory(int courseId, int[] categoriesId)
        {
            var course = _context.Courses.Find(courseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            var categories = _context.Categories.Where(c => categoriesId.Contains(c.Id)).ToList();
            if (categories.Count != categoriesId.Length)
            {
                throw new Exception("Category not found");
            }
            course.Categories = categories;
            return _context.SaveChangesAsync();
        }

        public Task AddChapter(int courseId, string List)
        {
            var course = _context.Courses.Find(courseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            var chapter = new Chapter()
            {
                Course = course,
                List = List

            };
            return _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var course = _context.Courses.Find(id);
            course.DeleteteAt = DateTime.Now;
            course.IsDeleted = true;

            _context.Courses.Update(course);
            return _context.SaveChangesAsync();
        }

        public bool Exists(int Id)
        {
            return _context.Courses.Any(c => c.Id == Id);
        }

        public IQueryable<Course> SearchCourses(CourseFindDTO courseFindDTO)
        {
            var courses = _context.Courses.AsQueryable();
            if (!string.IsNullOrEmpty(courseFindDTO.Name))
            {
                courses = courses.Where(c => c.Name.Contains(courseFindDTO.Name));
            }
            if (!string.IsNullOrEmpty(courseFindDTO.CategoryName))
            {
                courses = courses.Where(c => c.Categories.Where(cat => cat.Name.Contains(courseFindDTO.CategoryName)).Any());
            }
            if (courseFindDTO.RangeStudents.Length == 2)
            {
                courses = courses.Where(c => c.Students.Count() >= courseFindDTO.RangeStudents[0] && c.Students.Count() <= courseFindDTO.RangeStudents[1]);
            }
            return courses.AsQueryable();
        }
        public IQueryable<Course> GetAll(int pageNumber, int resultsPage)
        {
            var courses = _context.Courses.OrderBy(c => c.Name).AsQueryable();
            return Paginator.GetPage(courses, pageNumber, resultsPage);
        }

        public Task<CourseDTO?> GetCourseById(int id)
        {
            return _context.Courses
           .Where(c => c.Id == id)
           .Select(c => new CourseDTO()
           {
               Id = c.Id,
               Name = c.Name,
               ShortDescription = c.ShortDescription,
               Description = c.Description,
               Level = c.Level,

           }


           )
           .FirstOrDefaultAsync();
        }
        public Task<Course?> GetById(int id)
        {
            return _context.Courses
           .Where(c => c.Id == id)
           .FirstOrDefaultAsync();
        }

        public Task RemoveCatetory(int[] categoriesId)
        {
            var categories = _context.Categories.Where(c => categoriesId.Contains(c.Id)).ToList();
            if (categories.Count != categoriesId.Length)
            {
                throw new Exception("Category not found");
            }
            foreach (var category in categories)
            {
                category.Courses.Clear();
            }
            return _context.SaveChangesAsync();
        }

        public Task RemoveChapter(int chaptersId)
        {
            _context.Chapters.Remove(_context.Chapters.Find(chaptersId));
            return _context.SaveChangesAsync();
        }

        public Task Update(Course entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _context.Courses.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}