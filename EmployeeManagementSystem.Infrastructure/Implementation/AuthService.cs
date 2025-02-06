using Dapper;
using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IDbConnection _db;

        public AuthService(IUserService userService, IPasswordHasher<User> passwordHasher, IDbConnection db)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _db = db;
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

        public async Task<bool> AssignRole(Guid userId, string roleName)
        {
            string roleQuery = "SELECT Id FROM Roles WHERE Name = @RoleName";
            var roleId = await _db.QueryFirstOrDefaultAsync<Guid?>(roleQuery, new { RoleName = roleName });

            if (roleId == null)
                return false;

            string userRoleQuery = "INSERT INTO UserRoles (UserId, RoleId) VALUES (@UserId, @RoleId)";
            var result = await _db.ExecuteAsync(userRoleQuery, new { UserId = userId, RoleId = roleId });

            return result > 0;
        }
        public async Task<List<string>> GetUserRoles(Guid userId)
        {
            string query = @"
                SELECT r.Name 
                FROM Roles r
                INNER JOIN UserRoles ur ON r.Id = ur.RoleId
                WHERE ur.UserId = @UserId";

            var roles = await _db.QueryAsync<string>(query, new { UserId = userId });
            return roles.ToList();
        }

    }

}
