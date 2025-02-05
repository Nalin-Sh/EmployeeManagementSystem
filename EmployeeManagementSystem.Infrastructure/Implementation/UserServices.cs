using Dapper;
using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.Implementation
{
    public class UserServices : IUserService
    {
        private readonly string _db;

        public UserServices(IConfiguration configuration)
        {
            _db = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {

            using var connection = new SqlConnection(_db);
            string query = "SELECT * FROM Users WHERE Email = @Email";
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            using var connection = new SqlConnection(_db);
            string query = @"
            INSERT INTO Users (Id, UserName, Email, PasswordHash)
            VALUES (@Id, @UserName, @Email, @PasswordHash)";
            int rowsAffected = await connection.ExecuteAsync(query, user);
            return rowsAffected > 0;
        }
    }
}
