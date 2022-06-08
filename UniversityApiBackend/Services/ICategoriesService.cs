using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApiBackend.DTOs.Categories;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICategoriesService : IBaseService<Category>
    {
        IQueryable<CategoryDTO> SearchCategory(string Name, int[] rangeCourse);
        IQueryable<CategoryDTO> GetAllCategories(int pageNumber, int resultsPage);
  
        Task<CategoryDTO?> GetById(int id);
    }
}