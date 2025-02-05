using EmployeeManagementSystem.Application.DTOs.Department;
using EmployeeManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetDepartments();
            return View(departments);
        }

        [HttpGet("create")]

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(DepartmentDTO departmentDTO)
        {
            var dept = await _departmentService.AddDepartment(departmentDTO);

            if (dept != null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
