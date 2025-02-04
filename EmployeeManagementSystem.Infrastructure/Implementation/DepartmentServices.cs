using Dapper;
using EmployeeManagementSystem.Application.DTOs.Department;
using EmployeeManagementSystem.Application.DTOs.Employee;
using EmployeeManagementSystem.Application.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.Implementation
{
    public class DepartmentServices : IDepartmentService
    {
        private readonly IDbConnection _db;
        public DepartmentServices(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<int> AddDepartment(DepartmentDTO departmentDto)
        {
            var parameters = new
            {
                departmentDto.DepartmentName,
             
            };

            return await _db.ExecuteAsync("sp_AddDepartment", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<DepartmentDTO>> GetDepartments()
        {
            return await _db.QueryAsync<DepartmentDTO>("sp_GetDepartments", commandType: CommandType.StoredProcedure);
        }
    }
}
