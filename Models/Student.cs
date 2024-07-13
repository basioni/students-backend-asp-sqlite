using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace students_backend_asp_sqlite.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; } 
        public string? PhoneNumber { get; set; } 
        public string? Email { get; set; } 


    
    }

}