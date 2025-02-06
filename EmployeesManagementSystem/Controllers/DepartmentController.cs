using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Application.Services;
using EmployeesManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		[HttpGet("SeedDepartments")]
		public async Task SeedDepartmentsAsync(IEnumerable<Department> departments)
		{
			var result = await _departmentService.SeedDepartmentsAsync(departments);
			if (result.Success)
			{
				return;
			}
			else
			{
				throw new Exception($"Failed to seed: {result.ErrorMessage}");
			}
		}
	}
}
