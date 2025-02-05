using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUserService userService, IPasswordHasher<User> passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> Register(User user, string password)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            return await _userService.CreateUserAsync(user);
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success ? user : null;
        }
    }

}
