using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Application.Services;
using EmployeesManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace EmployeesManagementSystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		private readonly IDepartmentService _departmentService;

		public DepartmentController(IDepartmentService departmentService)
		{
			_departmentService = departmentService;
		}
		//[Authorize(Roles = "Admin")]
		[HttpPost("SeedDepartments")]
		public async Task<IActionResult> SeedDepartmentsAsync()
		{
			var departments = new List<Department>
		{
				new Department { DepartmentName = "Human Resources" },
				new Department { DepartmentName = "Finance" },
				new Department { DepartmentName = "IT" },
				new Department { DepartmentName = "Marketing" },
				new Department { DepartmentName = "Sales" },
				new Department { DepartmentName = "Customer Service" },
				new Department { DepartmentName = "Legal" },
				new Department { DepartmentName = "Operations" },
				new Department { DepartmentName = "Research & Development" },
				new Department { DepartmentName = "Administration" },
				new Department { DepartmentName = "Logistics" },
				new Department { DepartmentName = "Procurement" },
				new Department { DepartmentName = "Production" },
				new Department { DepartmentName = "Quality Assurance" },
				new Department { DepartmentName = "Maintenance" },
				new Department { DepartmentName = "Public Relations" },
				new Department { DepartmentName = "Business Development" },
				new Department { DepartmentName = "Training" },
				new Department { DepartmentName = "Security" },
				new Department { DepartmentName = "Facilities" }
		};

			var result = await _departmentService.SeedDepartmentsAsync(departments);
			if (result.Success)
			{
				return Ok("Success");
			}
			else
			{
				throw new Exception($"Failed to seed: {result.ErrorMessage}");
			}
		}

		[HttpGet("GetAllDepartments")]
		public async Task<IActionResult> GetAllDepartments()
		{
			var allDepartments = await _departmentService.GetAllDepartmentsAsync();
			return Ok(allDepartments);
		}
	}
}
