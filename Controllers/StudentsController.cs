using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using students_backend_asp_sqlite.Data;
using students_backend_asp_sqlite.Dtos.Student;
using students_backend_asp_sqlite.Mappers;
using students_backend_asp_sqlite.Models;

namespace students_backend_asp_sqlite.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    // api/students
    public class StudentsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public StudentsController(AppDBContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        // Get All Students
        public async Task<IActionResult> GetStudents()
        {
            // var students = await _context.Students.AsNoTracking().ToListAsync();
            var students = await _context.Students.AsNoTracking().Select(s => s.ToStudentDto()).ToListAsync();
            return Ok(students);
        }

        // Get Student by Id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if(student is null)
            {
                return NotFound();
            }
            return Ok(student.ToStudentDto());
        }

        // Create New Student
        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var studentModel = studentDto.ToStudentCreateDto(); 
            await _context.AddAsync(studentModel);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Ok(studentModel.ToStudentDto());
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        // api/students/1
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDto updateStudentDto)
        {
            var studentFromDB = await _context.Students.FindAsync(id);

            if(studentFromDB is null)
            {
                return BadRequest("Student is not Found");
            }

            studentFromDB.Name = updateStudentDto.Name;
            studentFromDB.Address = updateStudentDto.Address;
            studentFromDB.Email = updateStudentDto.Email;
            studentFromDB.PhoneNumber = updateStudentDto.PhoneNumber;

            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok("Student Successfully Updated ");
            }

            return BadRequest("Unable to Update Student");
        }

        [HttpDelete]
        // delete student
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if(student is null){
                return NotFound();
            }
            _context.Remove(student);

            var result = await _context.SaveChangesAsync();
            if(result > 0)
                return Ok("Student Deleted");

            return BadRequest("Student is not added");
        }

    }
}