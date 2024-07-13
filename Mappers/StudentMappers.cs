using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using students_backend_asp_sqlite.Dtos.Student;
using students_backend_asp_sqlite.Models;

namespace students_backend_asp_sqlite.Mappers
{
    public static class StudentMappers
    {
        public static StudentDto ToStudentDto(this Student studenModel)
        {
            return new StudentDto
            {
                Id = studenModel.Id,
                Name = studenModel.Name,
                Address = studenModel.Address,
                Email = studenModel.Email
            };
        }

        public static Student ToStudentCreateDto(this CreateStudentDto studentDto)
        {
            return new Student
            {
                Id = studentDto.Id,
                Name = studentDto.Name,
                Address = studentDto.Address,
                Email = studentDto.Email
            };
        }
    }
}