using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Models;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly SchoolContext _context;

        public StudentController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Student?firstname=yigit&lastname=ates
        // gets students having given firstname and lastname
        // firstname and lastname can be empty
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            // query students using given firstname and lastname values
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName)) {
                return await _context.Students
                .Where(i => i.FirstName.ToLower() == firstName.ToLower())
                .Where(i => i.LastName.ToLower() == lastName.ToLower())
                .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(firstName)) {
                return await _context.Students
                .Where(i => i.FirstName.ToLower() == firstName.ToLower())
                .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(lastName)) {
                return await _context.Students
                .Where(i => i.LastName.ToLower() == lastName.ToLower())
                .ToListAsync();
            }
            else {
                return await _context.Students
                .ToListAsync();
            }
        }

        // GET: api/Student/bygrade?mingrade=0&maxgrade=3
        // gets students having higher or equal grade than mingrade value
        // and lower or equal grade than maxgrade value
        // mingrade and maxgrade can be empty
        [HttpGet("byGrade")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByGrade([FromQuery] int? minGrade, [FromQuery] int? maxGrade)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            // set mingrade and maxgrade values to default
            if (!minGrade.HasValue) {
                minGrade = 0;
            }
            if (!maxGrade.HasValue) {
                maxGrade = 4;
            }

            return await _context.Students
                .Where(i => (i.Grade >= minGrade && i.Grade <= maxGrade))
                .ToListAsync();
        }

        // GET: api/Student/5
        // gets student with given id if student exists
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            //var student = await _context.Students.FindAsync(id);
            var student = await _context.Students
                .FirstOrDefaultAsync(i => i.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Student/5
        // updates student having the given id with given values
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Student
        // adds a student with given values
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'SchoolContext.Students'  is null.");
            }
            
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // DELETE: api/Student/5
        // deletes the student with the given id if student exists
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
