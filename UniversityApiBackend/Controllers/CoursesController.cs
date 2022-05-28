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
using UniversityApiBackend.DTOs.Courses;
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
        public CoursesController(
            ICoursesService service,
            Mapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses([FromQuery] int pageNumber, int resultsPage)
        {
            var courses = await _service.GetAll(pageNumber, resultsPage).ToListAsync();
            if (courses.Any())
                return _mapper.Map<List<CourseDTO>>(courses);

            return new List<CourseDTO>();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _service.GetById(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }



            try
            {
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
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {

            await _service.Add(course);

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
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
