using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using students_backend_asp_sqlite.Models;

namespace students_backend_asp_sqlite.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}