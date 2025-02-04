using EmployeeManagementSystem.Application.DTOs.Employee;
using EmployeeManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> AddEmployee(EmployeeRequestDTO employeeDto);
        Task<IEnumerable<EmployeeResponseDTO>> GetEmployees();
        Task<EmployeeResponseDTO> GetEmployeeById(int id);
        Task<bool> UpdateEmployee(int id, EmployeeRequestDTO employeeDto);
        Task<bool> DeleteEmployee(int id);

    }
}
