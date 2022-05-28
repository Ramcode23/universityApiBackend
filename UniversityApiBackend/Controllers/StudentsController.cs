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
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Services;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentService _service;
        private readonly IMapper _mapper;
        public StudentsController(
            IStudentService service,
            Mapper mapper)
        {
           _mapper=mapper;
            _service = service;
        }

        // GET: api/Students
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudent([FromQuery] int pageNumber, int resultsPage)
        {
              var students=  await _service.GetAll(pageNumber,resultsPage).ToListAsync();
            if(students.Any())
                return _mapper.Map<List<StudentDTO>>(students);

            return new List<StudentDTO>();
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

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }


            try
            {
                await _service.Update(student);
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
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
          if (student == null)
          {
              return Problem("Entity set 'UniversityDbContext.Student'  is null.");
          }  
            await _service.Add(student);

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
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
