using EmployeeManagementSystem.Application.DTOs.Employee;
using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace EmployeeManagementSystem.API.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetEmployees();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeRequestDTO employeeDto)
        {
            await _employeeService.AddEmployee(employeeDto);
            return RedirectToAction("Index");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            return employee != null ? 
                Ok(employee) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employeeDto = await _employeeService.GetEmployeeById(id);
            if (employeeDto == null) return NotFound();

            ViewBag.Departments = await _departmentService.GetDepartments();

            return View(employeeDto);
        }



        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id,EmployeeRequestDTO employeeDto)
        {

            var result = await _employeeService.UpdateEmployee(id, employeeDto);
            return result ? Ok(new { message = "Employee updated successfully" }) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            return result ? Ok(new { message = "Employee deleted successfully" }) : NotFound();
        }
    }
}
