using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace students_backend_asp_sqlite.Dtos.Account
{
    public class NewUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}