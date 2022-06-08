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

        public async Task AddChapter(ChapterDTO chapterDTO)
        {
            var course = await _context.Courses
                 .Include(c => c.Chapter)
                 .FirstOrDefaultAsync(c => c.Id == chapterDTO.CourseId);

            var lessons = new List<Lesson>();
            foreach (var lesson in chapterDTO.Lessons)
            {
                lessons.Add(new Lesson
                {
                    //Tittle = lesson.Tittle,
                    //Chapter = course.Chapter
                    Tittle = lesson.Tittle,
                    ChapterId = course.Chapter.CourseId
                }); ;
            }


            _context.Lessons.AddRange(lessons);
            await _context.SaveChangesAsync();

            course.Chapter.Lessons.ToList().AddRange(lessons.ToArray());
            await _context.SaveChangesAsync();
            return;
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
            if (courseFindDTO.RangeStudents.Length == 2)
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

            return _context.Courses.OrderBy(c => c.Name).AsQueryable();
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

        public Task AddLesson(int ChaperId, List<Lesson> lessons)
        {
            var chapter = _context.Chapters.Find(ChaperId);
            if (chapter == null)
            {
                throw new Exception("Chapter not found");
            }

            return _context.SaveChangesAsync();
        }

        public Task RemoveLesson(int lessonId)
        {
            _context.Lessons.Remove(_context.Lessons.Find(lessonId));
            return _context.SaveChangesAsync();
        }

        public Task EditLesson(int ChaperId, List<Lesson> lessons)
        {
            var chapter = _context.Chapters.Find(ChaperId);
            if (chapter == null)
            {
                throw new Exception("Chapter not found");
            }
            foreach (var lesson in lessons)
            {
                //lesson.Chapter = chapter;
                _context.Lessons.Update(lesson);
            }
            return _context.SaveChangesAsync();
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