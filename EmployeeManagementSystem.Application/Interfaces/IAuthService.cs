using EmployeeManagementSystem.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Register(User user, string password);
        Task<User> Login(string email, string password);
    }

}
