using EmployeeManagementSystem.Application.DTOs.Department;
using EmployeeManagementSystem.Application.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<int> AddDepartment(DepartmentDTO departmentDto);
        Task<IEnumerable<DepartmentDTO>> GetDepartments();
    }
}
