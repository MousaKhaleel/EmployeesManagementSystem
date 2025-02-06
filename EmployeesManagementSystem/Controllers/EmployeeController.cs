using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.Intrinsics.X86;

namespace EmployeesManagementSystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{

		private readonly IEmployeeService _employeeService;

		public EmployeeController(IEmployeeService employeeService)
		{
			_employeeService = employeeService;
		}

		[HttpGet("GetAllEmployees")]
		public async Task<IActionResult> GetAllEmployees()
		{
			var allEmployees = await _employeeService.GetAllEmployeesAsync();
			return Ok(allEmployees);
		}

		[HttpGet("SeedEmployees")]
		public async Task SeedEmployeesAsync(IEnumerable<Employee> employees)
		{
			var result = await _employeeService.SeedEmployeesAsync(employees);
			if (result.Success)
			{
				return;
			}
			else
			{
				throw new Exception($"Failed to seed: {result.ErrorMessage}");
			}
		}

		//		Create method to update Employee main info and use employee number as
		//unique key to find employee then update. (such as department, position, name,
		//salary).
		[HttpGet("UpdateEmployeeInfo")]
		public async Task UpdateEmployeeInfoAsync()
		{
		}

			//		Create method to update vacation days balance after approve any vacation request
			//which the logic of this method is to decrease employee vacation days left.

		//approve/deny v for subs
		}
}
