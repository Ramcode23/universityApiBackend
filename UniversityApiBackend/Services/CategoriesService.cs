using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqSnippets;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTOs.Categories;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly UniversityDbContext _context;
        public CategoriesService(UniversityDbContext context)
        {
            _context = context;
        }

        public Task Add(Category entity)
        {
            entity.CreatedAt = DateTime.Now;
            _context.Categories.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var category = _context.Categories.Find(id);
            category.IsDeleted = true;
            category.DeleteteAt = DateTime.Now;
            _context.Categories.Update(category);
            return _context.SaveChangesAsync();
        }

        public bool Exists(int Id)
        {
            return _context.Categories.Any(c => c.Id == Id);
        }


        public IQueryable<Category> GetAll()
        {
            return _context.Categories
             .OrderBy(c => c.Name)
             .AsQueryable();
           
        }

        
        public IQueryable<CategoryDTO> GetAllCategories(int pageNumber, int resultsPage)
        {
            var categories = _context.Categories
             .OrderBy(c => c.Name)
             .Select(c => new CategoryDTO
             {
                 Id = c.Id,
                 Name = c.Name,
                 Courses=c.Courses.Count()
             })

             .AsQueryable();
            return Paginator.GetPage(categories, pageNumber, resultsPage);
        }

        public IQueryable<CategoryDTO> SearchCategory(string Name, int[] rangeCourse)
        {
            var categories = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                categories = categories.Where(c => c.Name.ToLower().Contains(Name.ToLower()));
            }
            if (rangeCourse.Length > 0)
            {
                categories = categories.Where(c => c.Courses.Count() > rangeCourse[0] && c.Courses.Count() < rangeCourse[1]);
            }
            return categories.Select(c => new CategoryDTO
            {
               Id= c.Id,
               Name= c.Name,
                Courses = c.Courses.Count()
            }


            ).AsQueryable();
        }

        public Task<CategoryDTO?> GetById(int id)
        {
            return _context.Categories.Select(c => new CategoryDTO
            {
               Id= c.Id,
               Name= c.Name,
                Courses = c.Courses.Count()
            } )
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task Update(Category entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _context.Categories.Update(entity);

            return _context.SaveChangesAsync();
        }

        Task<Category?> IBaseService<Category>.GetById(int id)
        {
            throw new NotImplementedException();
        }

      
    }

}
