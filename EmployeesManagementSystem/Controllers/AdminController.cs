using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Application.Services;
using EmployeesManagementSystem.Domain.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagementSystem.Api.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
		private readonly IPositionService _positionService;
		private readonly IDepartmentService _departmentService;
		private readonly IVacationService _vacationService;
		private readonly IEmployeeService _employeeService;

		public AdminController(IPositionService positionService, IDepartmentService departmentService, IVacationService vacationService, IEmployeeService employeeService)
		{
			_positionService = positionService;
			_departmentService = departmentService;
			_vacationService = vacationService;
			_employeeService = employeeService;
		}

		[HttpPost("AddPosition")]
		public async Task<IActionResult> AddPositionAsync(Position position)
		{
			var result = await _positionService.AddNewPositionAsync(position);
			if (result.Success)
			{
				return Ok("Success");
			}
			else
			{
				throw new Exception($"Failed to add: {result.ErrorMessage}");
			}
		}

		[HttpPost("AddDepartment")]
		public async Task<IActionResult> AddDepartmentAsync(Department department)
		{
			var result = await _departmentService.AddNewDepartmentAsync(department);
			if (result.Success)
			{
				return Ok("Success");
			}
			else
			{
				throw new Exception($"Failed to add: {result.ErrorMessage}");
			}
		}

		[HttpPost("AddVacationTypes")]
		public async Task<IActionResult> SeedVacationTypesAsync(VacationType vacationType)
		{
			var result = await _vacationService.AddNewVacationTypeAsync(vacationType);
			if (result.Success)
			{
				return Ok("add successfully.");
			}
			return StatusCode(500, $"Failed to add: {result.ErrorMessage}");
		}
		//TODO: assign sub for emps
		[HttpPost("AssignSubordinate")]
		public async Task<IActionResult> AssignSubordinate(string supervisor, string subordinate)
		{
			var result = await _employeeService.AssignSubordinateToSupervisorAsync(supervisor, subordinate);
			if (result.Success)
			{
				return Ok("Assigned successfully.");
			}
			return StatusCode(500, $"Failed to add: {result.ErrorMessage}");
		}
	}
}
