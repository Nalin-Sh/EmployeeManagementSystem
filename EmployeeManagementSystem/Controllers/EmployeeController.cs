using EmployeeManagementSystem.Application.DTOs.Employee;
using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using PagedList.Core;

namespace EmployeeManagementSystem.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index(int? department)
        {
            ViewBag.Departments = await _departmentService.GetDepartments();
            var employees = await _employeeService.GetEmployees();

            var departments = await _departmentService.GetDepartments();
            
            if (department.HasValue && department.Value > 0)
            {
                var selectedDepartment = departments.FirstOrDefault(d => d.DepartmentID == department.Value).DepartmentName;
                if (!string.IsNullOrEmpty(selectedDepartment))
                {
                    employees = employees.Where(e => e.DepartmentName == selectedDepartment).ToList();
                }
            }
            return View(employees);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetEmployees(string? search,int? department)
        {

            var employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _departmentService.GetDepartments();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeRequestDTO employeeDto)
        {
            await _employeeService.AddEmployee(employeeDto);
            return RedirectToAction("Index");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee != null)
            {
                return View(employee);
            }
            return View(new EmployeeResponseDTO());
         
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employeeDto = await _employeeService.GetEmployeeById(id);
            if (employeeDto == null) return NotFound();

            ViewBag.Departments = await _departmentService.GetDepartments();

            return View(employeeDto);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(int id, EmployeeRequestDTO employeeDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _departmentService.GetDepartments();
                return View(employeeDto);
            }

            var result = await _employeeService.UpdateEmployee(id, employeeDto);

            if (result != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to update employee.");
                ViewBag.Departments = await _departmentService.GetDepartments();
                return View(employeeDto);
            }
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            if(result != null)
            {
                return RedirectToAction("Index");
            }
            return View();
            
        }
    }
}
