#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTOs.Categories;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Services;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _service;
        private readonly IMapper _mapper;
        private readonly IUserHelper _userHelper;
        public CategoriesController(
            ICategoriesService service,
           IMapper mapper,
           IUserHelper userHelper)
        {
            _mapper = mapper;
            _service = service;
            _userHelper = userHelper;
        }

        // GET: api/Categories
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories([FromQuery] int pageNumber, int resultsPage)
        {


            var total = _service.GetAll().Count();
            var categories=  await Task.FromResult(_service.GetAllCategories(pageNumber, resultsPage).ToList());
      
            
            return Ok(new
            {
                TotalRecords = total,
                 Categories = categories,
            });
          
        }

        // GET: api/CategoriesList
        [HttpGet("List")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<CategoryListDTO>>> GetCategoriesList()
        {
            var categories = await Task.FromResult(_service.GetAll());
            if (categories.Any())
                return _mapper.Map< List< CategoryListDTO>>(categories);
            return new List<CategoryListDTO>();
        }

        // GET: api/Categories
        [HttpGet("Search")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> SearchCategory([FromQuery] string Name, [FromQuery] int[] rangeCourse)
        {
            var categories = await Task.FromResult(_service.SearchCategory(Name, rangeCourse).ToList());

            return Ok(new
            {
                TotalRecords = categories.Count(),
                Categories = categories,
            });
        }
       
        // GET: api/Categories/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _service.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutCategory(int id, CategoryCreateDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
            {
                return BadRequest();
            }


            try
            {
              var category=_mapper.Map<Category>(categoryDTO);
                await _service.Update(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles  = "Administrator")]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryCreateDTO categoryDTO)
        {
            
            if (categoryDTO == null)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(categoryDTO);
            category.CreatedBy = await _userHelper.GetUserByEmailAsync( User.Identity.Name);    
            await _service.Add(category);
            return Ok(
                new{
                    Message = "Category created successfully",
                   
                }
            );
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = _service.Exists(id);
            if (!category)
            {
                return NotFound();
            }

            ;
            await _service.Delete(id);

            return NoContent();
        }

      
    }
}
