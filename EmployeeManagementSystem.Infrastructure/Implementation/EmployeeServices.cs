using Dapper;
using EmployeeManagementSystem.Application.DTOs.Employee;
using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Entities;
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
    public class EmployeeServices : IEmployeeService
    {
        private readonly IDbConnection _db;
        private readonly IDepartmentService _departmentService;

        public EmployeeServices(IConfiguration configuration, IDepartmentService departmentService) 
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _departmentService = departmentService;
        }

        public async Task<int> AddEmployee(EmployeeRequestDTO employeeDto)
        {
            var parameters = new
            {
                employeeDto.Name,
                employeeDto.DepartmentID,
                employeeDto.Salary,
                employeeDto.DateOfJoining
            };

            return await _db.ExecuteAsync("sp_AddEmployee", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            return await _db.ExecuteAsync("sp_SoftDeleteEmployee", new { EmployeeID = id }, commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<EmployeeResponseDTO> GetEmployeeById(int id)
        {
            var query = "EXEC sp_GetEmployeeById @EmployeeID";
            var parameters = new { EmployeeID = id };

            return await _db.QueryFirstOrDefaultAsync<EmployeeResponseDTO>(query, parameters);
        }

        public async Task<IEnumerable<EmployeeResponseDTO>> GetEmployees()
        {
            return await _db.QueryAsync<EmployeeResponseDTO>("sp_GetEmployees", commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateEmployee(int id, EmployeeRequestDTO employeeDto)
        {
            var parameters = new
            {
                EmployeeID = id,
                employeeDto.Name,
                employeeDto.DepartmentID,
                employeeDto.Salary,
                employeeDto.DateOfJoining
            };

            return await _db.ExecuteAsync("sp_UpdateEmployee", parameters, commandType: CommandType.StoredProcedure) > 0;
        }
    }
}
