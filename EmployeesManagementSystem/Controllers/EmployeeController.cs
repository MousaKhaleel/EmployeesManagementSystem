using EmployeesManagementSystem.Application.Dtos;
using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Domain.Models;
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
			var allEmployees = await _employeeService.GetEmployeeByNumberAsync(empNum);
			return Ok(allEmployees);
		}

		[HttpPost("SeedEmployees")]
		public async Task<IActionResult> SeedEmployeesAsync()
		{
			//TODO: move to a saparate file
			var employees = new List<Employee>
	{
		new Employee
		{
			EmployeeNumber = "EMP001",
			EmployeeName = "John Doe",
			DepartmentId = 1,
			PositionId = 1,
			GenderCode = "M",
			Salary = 50000.00m,
			ReportedToEmployeeNumber = null
		},
		new Employee
		{
			EmployeeNumber = "EMP002",
			EmployeeName = "Jane Smith",
			DepartmentId = 2,
			PositionId = 2,
			GenderCode = "F",
			Salary = 55000.00m,
			ReportedToEmployeeNumber = "EMP001"
		},
		new Employee
		{
			EmployeeNumber = "EMP003",
			EmployeeName = "Alice Johnson",
			DepartmentId = 1,
			PositionId = 3,
			GenderCode = "F",
			Salary = 60000.00m,
			ReportedToEmployeeNumber = "EMP001"
		},
		new Employee
		{
			EmployeeNumber = "EMP004",
			EmployeeName = "Bob Brown",
			DepartmentId = 3,
			PositionId = 4,
			GenderCode = "M",
			Salary = 45000.00m,
			ReportedToEmployeeNumber = "EMP002"
		},
		new Employee
		{
			EmployeeNumber = "EMP005",
			EmployeeName = "Charlie Davis",
			DepartmentId = 2,
			PositionId = 5,
			GenderCode = "M",
			Salary = 70000.00m,
			ReportedToEmployeeNumber = "EMP002"
		},
			new Employee
	{
		EmployeeNumber = "EMP006",
		EmployeeName = "Robert Johnson",
		DepartmentId = 13,
		PositionId = 6,
		GenderCode = "M",
		Salary = 52000.00m,
		ReportedToEmployeeNumber = "EMP004"
	},
	new Employee
	{
		EmployeeNumber = "EMP007",
		EmployeeName = "Emily Davis",
		DepartmentId = 10,
		PositionId = 7,
		GenderCode = "F",
		Salary = 58000.00m,
		ReportedToEmployeeNumber = "EMP001"
	},
	new Employee
	{
		EmployeeNumber = "EMP008",
		EmployeeName = "Samuel Lee",
		DepartmentId = 1,
		PositionId = 8,
		GenderCode = "M",
		Salary = 61000.00m,
		ReportedToEmployeeNumber = "EMP003"
	},
	new Employee
	{
		EmployeeNumber = "EMP009",
		EmployeeName = "Olivia Martin",
		DepartmentId = 7,
		PositionId = 9,
		GenderCode = "F",
		Salary = 57000.00m,
		ReportedToEmployeeNumber = "EMP002"
	},
	new Employee
	{
		EmployeeNumber = "EMP010",
		EmployeeName = "Ethan Clark",
		DepartmentId = 2,
		PositionId = 10,
		GenderCode = "M",
		Salary = 63000.00m,
		ReportedToEmployeeNumber = "EMP002"
	}
	};

			//var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "employees.json");
			//var json = await File.ReadAllTextAsync(filePath);

			//var employees = JsonSerializer.Deserialize<List<Employee>>(json);

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

		//		Create method to update Employee main info and use employee number as
		//unique key to find employee then update. (such as department, position, name,
		//salary).
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

		//		Create method to update vacation days balance after approve any vacation request
		//which the logic of this method is to decrease employee vacation days left.

		//GET /api/Employee/WithPendingVacations
		[HttpGet("GetEmployeesWithPendingVacationRequests")]
		public async Task<IActionResult> GetEmployeesWithPendingVacationRequests()
		{
			var all = await _employeeService.GetEmployeesWithPendingVacationRequestsAsync();
			return Ok(all);
		}
	}
}
