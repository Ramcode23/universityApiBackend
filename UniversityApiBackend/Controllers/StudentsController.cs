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
using UniversityApiBackend.DTOs.Students;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Services;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentService _service;
          private readonly ICoursesService _courseService;
        private readonly IMapper _mapper;
        private readonly IUserHelper _userHelper;
        public StudentsController(
            IStudentService service,
            ICoursesService courseService,
            IMapper mapper,
            IUserHelper userHelper)
        {
            _mapper = mapper;
            _service = service;
            _courseService = courseService;
            _userHelper = userHelper;
        }

        // GET: api/Students
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<StundentListDTO>>> GetStudent([FromQuery] int pageNumber, int resultsPage)
        {


            return  await Task.FromResult(_service.GetStudentsWithCoursesAsync(pageNumber, resultsPage).ToList());
           

        }


        // GET: api/Students
        [HttpGet("Search")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<StundentListDTO>>> FindStudent([FromQuery] StudentFindDTO studentFindDTO)
        {
            var students = await Task.FromResult(_service.FindStudentsAsync(studentFindDTO).ToList());
            return students;
        }



        // GET: api/Students/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {

            var student = await _service.GetById(id);
            if (student == null)
                return NotFound();

            return student;
        }


        // Enroll students in courses
        [HttpPost("Enroll")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> EnrollStudent([FromBody] StudentEnrollDTO studentEnrollDTO)
        {
             
            var student = await _service.GetById(studentEnrollDTO.StudentId);
            if (student == null)
                return NotFound( new { message = "Student not found" });    

            var course= await _courseService.GetById(studentEnrollDTO.CourseId);
            if (course == null)
                return NotFound( new { message = "Course not found" });
          
            if (student.Courses.Any(c => c.Id == course.Id))
                return BadRequest(new { message = "Student already enrolled in this course" });

 
            student.Courses.Add(course);
            await _service.EnrollAsysnc(student);

            return Ok(new { message = "Student enrolled" });
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutStudent(int id, StudentDTO studentDTO)
        {
            if (id != studentDTO.Id)
            {
                return BadRequest();
            }


            try
            {
                var student = _mapper.Map<Student>(studentDTO);
                student.UpdatedBy = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                student.UpdatedAt = DateTime.Now;
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Student>> PostStudent(StudentDTO studentDTO)
        {
            if (studentDTO == null)
            {
                return Problem("Entity set 'UniversityDbContext.Student'  is null.");
            }
            var student = _mapper.Map<Student>(studentDTO);
            student.CreatedBy = await _userHelper.GetUserByEmailAsync(User.Identity.Name);


            await _service.Add(student);

            return Ok(new
            {
                Student = studentDTO,
            });
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteStudent(int id)
        {

            var student = await _service.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            await _service.Delete(id);

            return NoContent();
        }


    }
}
