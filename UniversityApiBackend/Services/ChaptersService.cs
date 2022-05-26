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
    public class ChaptersService : IChaptersService
    {
        private readonly UniversityDbContext _context;
        public ChaptersService(UniversityDbContext context)
        {
            _context = context;
        }

        public Task Add(Chapter entity)
        {
            _context.Chapters.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var chapter = _context.Chapters.Find(id);
            chapter.IsDeleted = true;
            _context.Chapters.Update(chapter);
            return _context.SaveChangesAsync();
        }

        public bool Exists(int Id)
        {
            return _context.Chapters.Any(c => c.Id == Id);
        }

        public IQueryable<Chapter> GetAll(int pageNumber, int resultsPage)
        {
            var chapters= _context.Chapters.AsQueryable();
            return Paginator.GetPage(chapters, pageNumber, resultsPage);
        }

        public Task<Chapter?> GetById(int id)
        {
            return _context.Chapters
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        }

        public Task Update(Chapter entity)
        {
            _context.Chapters.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}