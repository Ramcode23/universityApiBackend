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

            var total = _service.GetAll().Count();
          var courses= await Task.FromResult(_service.GetAllCourseList(pageNumber, resultsPage).ToList());
            

            return Ok(new
            {
                TotalRecords = total,
                courses = courses,
            });

        }

        // GET: api/Courses
        [HttpGet("CoursesList")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<CourseListDTO>>> GetCoursesList()
        {
            var courses = await Task.FromResult(_service.GetAll());
            return _mapper.Map<List<CourseListDTO>>(courses);
           

        }

        // GET: api/Courses
        [HttpGet("Search")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> FindCourses([FromQuery] CourseFindDTO courseFindDTO)
        {
            var courses = await Task.FromResult(_service.SearchCourses(courseFindDTO).ToList());

            return Ok(new
            {
                TotalRecords = courses.Count,
                courses = courses,
            });

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

     


        private bool CourseExists(int id)
        {
            return _service.Exists(id);
        }
    }
}
