using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.DTOs.Employee
{
    public class EmployeeResponseDTO
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public int? DepartmentId { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfJoining { get; set; }
    }
}
