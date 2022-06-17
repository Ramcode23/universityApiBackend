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
using UniversityApiBackend.DTOs.Account;
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
        public async Task<ActionResult<StudentDetailDTO>> GetStudent(int id)
        {

            var student = await _service.GetById(id);
            var studentDTO = _mapper.Map<StudentDetailDTO>(student);
            if (student == null)
                return NotFound();

            return studentDTO;
        }


  

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutStudent(int id, StudentCreateDTO studentDTO)
        {
            if (id != studentDTO.Id)
            {
                return BadRequest();
            }

            try
            {

             var updateBy = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                var student = _mapper.Map<Student>(studentDTO);
                student.UpdatedBy = updateBy;
                student.UpdatedAt = DateTime.Now;
                await  _service.Update(student);
           
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
        public async Task<ActionResult<Student>> PostStudent(StudentCreateDTO studentDTO)
        {
                 
            
            if (studentDTO == null)
                return Problem("Entity set 'UniversityDbContext.Student'  is null.");
            
            var isEmailExit = await _userHelper.GetUserByEmailAsync(studentDTO.User.UserName);
            if (isEmailExit!=null)
                return Problem($"This  {studentDTO.User.UserName} exits.");

            var student = _mapper.Map<Student>(studentDTO);
            student.User.UserName = student.User.Email;
            student.CreatedBy = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            student.CreatedAt=DateTime.Now;
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
