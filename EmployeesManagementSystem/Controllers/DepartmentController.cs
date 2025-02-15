using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Application.Services;
using EmployeesManagementSystem.Domain.Models;
using EmployeesManagementSystem.Infrastructure.Data;
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
			try
			{
				var departments = DepartmentSeedData.GetDepartments();

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
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("GetAllDepartments")]
		public async Task<IActionResult> GetAllDepartments()
		{
			try
			{
				var allDepartments = await _departmentService.GetAllDepartmentsAsync();
				return Ok(allDepartments);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
