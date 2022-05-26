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
    public class CategoriesService:ICategoriesService
    {
        private readonly UniversityDbContext _context;
        public CategoriesService(UniversityDbContext context)
        {
            _context = context;
        }

        public Task Add(Category entity)
        {
            _context.Categories.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var category = _context.Categories.Find(id);
            category.IsDeleted = true;
            _context.Categories.Update(category);
            return _context.SaveChangesAsync();
        }

        public bool Exists(int Id)
        {
            return _context.Categories.Any(c => c.Id == Id);
        }

        public IQueryable<Category> GetAll(int pageNumber, int resultsPage)
        {
            var categories = _context.Categories
             .OrderBy(c => c.Name)
             .AsQueryable();
            return Paginator.GetPage(categories, pageNumber, resultsPage);
        }

        public Task<Category?> GetById(int id)
        {
           return _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task Update(Category entity)
        {
            _context.Categories.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
        
    }
