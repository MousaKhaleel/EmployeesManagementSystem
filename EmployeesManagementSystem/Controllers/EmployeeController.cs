using EmployeesManagementSystem.Application.Dtos;
using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using EmployeesManagementSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;

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

		[HttpGet("GetEmployeeByNumber/{empNum}")]
		public async Task<IActionResult> GetEmployeeByNumber(string empNum)
		{
			var employee = await _employeeService.GetEmployeeByNumberAsync(empNum);
			return Ok(employee);
		}

		//[Authorize(Roles = "Admin")]
		[HttpPost("SeedEmployees")]
		public async Task<IActionResult> SeedEmployeesAsync()
		{
			var employees = EmployeeSeedData.GetEmployees();

			var result = await _employeeService.SeedEmployeesAsync(employees);
			if (result.Success)
			{
				return Ok("Success");
			}
			else
			{
				throw new Exception($"Failed to seed: {result.ErrorMessage}");
			}
		}

		[HttpPut("UpdateEmployeeInfo/{empNum}")]
		public async Task<IActionResult> UpdateEmployeeInfoAsync(string empNum, EmployeeUpdateDto employeeUpdateDto)
		{

			var result = await _employeeService.UpdateEmployeeInfoAsync(empNum, employeeUpdateDto);
			if (!result.Success)
			{
				return BadRequest(result.ErrorMessage);
			}

			return Ok("Sucsess");
		}

		[HttpGet("GetEmployeesWithPendingVacationRequests")]
		public async Task<IActionResult> GetEmployeesWithPendingVacationRequests()
		{
			var all = await _employeeService.GetEmployeesWithPendingVacationRequestsAsync();
			return Ok(all);
		}
	}
}
