using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Domain.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfJoining { get; set; }
        public bool IsDeleted { get; set; }
    }
}
