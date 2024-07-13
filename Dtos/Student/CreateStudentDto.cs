using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace students_backend_asp_sqlite.Dtos.Student
{
    public class CreateStudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}