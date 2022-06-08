#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTOs;
using UniversityApiBackend.DTOs.Chapters;
using UniversityApiBackend.DTOs.Courses;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models;
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Services;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService _service;
        private readonly IMapper _mapper;
        private readonly IUserHelper _userHelper;
        public CoursesController(
            ICoursesService service,
            IMapper mapper,
            IUserHelper userHelper)
        {
            _service = service;
            _mapper = mapper;
            _userHelper = userHelper;
        }

        // GET: api/Courses
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses([FromQuery] int pageNumber, int resultsPage)
        {
           
            return await Task.FromResult(_service.GetAllCourseList(pageNumber, resultsPage).ToList());

        }
        // GET: api/Courses
        [HttpGet("Seracrh")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> FindCourses([FromQuery] CourseFindDTO courseFindDTO)
        {
            var courses = await Task.FromResult(_service.SearchCourses(courseFindDTO).ToList());
            return _mapper.Map<List<CourseDTO>>(courses);

        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CourseDetailDTO>> GetCourse(int id)
        {
            var course = await _service.GetById(id);

            if (course == null)
            {
                return NotFound();
            }

            return _mapper.Map<CourseDetailDTO>(course);
           // return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutCourse(int id, CourseCreateDTO courseCreate)
        {
            if (id != courseCreate.Id)
            {
                return BadRequest();
            }



            try
            {
                var course = _mapper.Map<Course>(courseCreate);
                course.UpdatedBy = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                course.UpdatedAt = DateTime.Now;
                await _service.Update(course);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Course>> PostCourse(CourseCreateDTO courseCreate)
        {
            if (courseCreate == null)
                return BadRequest();

            var course = _mapper.Map<Course>(courseCreate);
            course.CreatedBy = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            await _service.Add(course);
            return Ok(
                 new
                 {
                     message = "Course created successfully",
                 }
            );
        }


        [HttpPost("AddCategoryToCourse")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Course>> AddCatetory(int courseId, int[] categoriesId)
        {
            if (categoriesId == null)
                return BadRequest();

            await _service.AddCatetory(courseId, categoriesId);
            return Ok();

        }

        [HttpPost("AddChapterToCourse")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Course>> AddChapter(ChapterDTO chapterDTO)
        {
            if (!chapterDTO.Lessons.Any())
                return BadRequest(new { message = "No lessons added" });
            if( ! _service.Exists(chapterDTO.CourseId))
                return BadRequest(new { message = "Course not exist" });

            await _service.AddChapter(chapterDTO);
            return Ok( new { message = "Lessons added successfully" });

        }


        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _service.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            await _service.Delete(id);
            return NoContent();
        }

        // DELETE: api/Courses/5
        [HttpDelete("DeleteCategoryFromCourse")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> RemoveCatetories(int[] categoriesId)
        {
            if (categoriesId == null)
                return BadRequest();

            await _service.RemoveCatetory(categoriesId);
            return Ok();
        }


        // DELETE: api/Courses/5
        [HttpDelete(" chapter/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> RemoveChapter(int id)
        {
            if (id == null)
                return BadRequest();

            await _service.RemoveChapter(id);
            return Ok();
        }


        private bool CourseExists(int id)
        {
            return _service.Exists(id);
        }
    }
}
