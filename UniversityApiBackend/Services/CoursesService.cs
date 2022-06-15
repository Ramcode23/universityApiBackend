using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqSnippets;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTOs;
using UniversityApiBackend.DTOs.Chapters;
using UniversityApiBackend.DTOs.Courses;
using UniversityApiBackend.Models;
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
            }
            else
            {
                entity.Categories = new List<Category>();
            }

            var lessons = entity.Chapter.Lessons;    
            entity.Chapter.Lessons = new List<Lesson>();

            entity.CreatedAt = DateTime.Now;
            _context.Courses.Add(entity);
            _context.SaveChanges();

            foreach (var lesson in lessons)
            {
                _context.Lessons.AddAsync(new Lesson()
                {
                    Chapter = entity.Chapter,
                    ChapterId = entity.Chapter.Id,
                    Tittle = lesson.Tittle
                });
            }


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

        public IQueryable<CourseDTO> SearchCourses(CourseFindDTO courseFindDTO)
        {
            var courses = _context.Courses
                 .Include(x => x.Categories)
                .Include(x => x.Students)
                .OrderBy(c => c.Name)
                .AsQueryable();
            if (!string.IsNullOrEmpty(courseFindDTO.Name))
            {
                courses = courses.Where(c => c.Name.Contains(courseFindDTO.Name));
            }
            if (!string.IsNullOrEmpty(courseFindDTO.CategoryName))
            {
                courses = courses.Where(c => c.Categories.Where(cat => cat.Name.Contains(courseFindDTO.CategoryName)).Any());
            }
            if(courseFindDTO.RangeStudents != null)
            if (courseFindDTO.RangeStudents.Length == 2 )
            {
                courses = courses.Where(c => c.Students.Count() >= courseFindDTO.RangeStudents[0] && c.Students.Count() <= courseFindDTO.RangeStudents[1]);
            }
            return courses.Select(c => new CourseDTO
            {
                Id = c.Id,
                Name = c.Name,
                CategoryName = c.Categories.FirstOrDefault().Name,
                Students = c.Students.Count(),
            });
        }
        public IQueryable<Course> GetAll()
        {

            return _context.Courses.Where(c=>c.IsDeleted==false).OrderBy(c => c.Name).AsQueryable();
        }

        public Task<Course?> GetById(int id)
        {
            return _context.Courses
              .Include(c => c.Categories)
                .Include(c => c.Chapter)
                .ThenInclude(c => c.Lessons)
           .Where(c => c.Id == id)
           .FirstOrDefaultAsync();
        }

     
        public Task Update(Course entity)
        {

            try
            {

           
            var categories = _context.Categories.Where(c => entity.Categories.Select(x => x.Id).Contains(c.Id)).ToList();
            var oldcategories = _context.Categories.Where(c => c.Courses.Where(x => x.Id == entity.Id).ToList().Count>0);
            var olLessons=_context.Lessons.Where(x => x.ChapterId == _context.Chapters.Where(c=>c.CourseId==entity.Id).FirstOrDefault().Id).ToList();
              var course= _context.Courses.
                    Include(x=>x.Chapter)
                   . Where(x=>x.Id==entity.Id).FirstOrDefault();

                entity.Categories = new List<Category>();
                var lessons = entity.Chapter.Lessons;
            entity.Chapter.Lessons = new List<Lesson>();
            _context.Lessons.RemoveRange(olLessons);
            _context.SaveChanges();

           

            if (categories.Any())
            {
                course.Categories = categories;
            }


            foreach (var lesson in lessons)
            {
                _context.Lessons.AddAsync(new Lesson()
                {
                    Chapter = course.Chapter,
                    ChapterId = course.Chapter.Id,
                    Tittle = lesson.Tittle
                });
            }
                _context.SaveChanges();

                entity.UpdatedAt = DateTime.Now;


                course.UpdatedAt=DateTime.Now;
                course.Name = entity.Name;
                course.Description = entity.Description;
                course.ShortDescription = entity.ShortDescription;
                course.Level = entity.Level;
                course.UpdatedBy = entity.UpdatedBy;









            _context.Courses.Update(course);

            return _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {

                throw;
            }
        }

     
        public IQueryable<CourseDTO> GetAllCourseList(int pageNumber, int resultsPage)
        {
           var courses = _context.Courses
                .Include(x=>x.Categories)
                .Include(x=>x.Students)
                .OrderBy(c => c.Name)
                .Select(c=> new CourseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    CategoryName=c.Categories.FirstOrDefault().Name,
                    Students=c.Students.Count(),
                })
                .AsQueryable();
            return Paginator.GetPage(courses, pageNumber, resultsPage);
        }
    }
}