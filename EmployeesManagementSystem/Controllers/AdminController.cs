using EmployeesManagementSystem.Application.Dtos;
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
		public async Task<IActionResult> AddPositionAsync(PositionDto positionDto)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}
			try
			{
				var result = await _positionService.AddNewPositionAsync(positionDto);
				if (result.Success)
				{
					return Ok("Position added successfully.");
				}
				else
				{
					throw new Exception($"Failed to add: {result.ErrorMessage}");
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("AddDepartment")]
		public async Task<IActionResult> AddDepartmentAsync(DepartmentDto departmentDto)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}
			try
			{
				var result = await _departmentService.AddNewDepartmentAsync(departmentDto);
				if (result.Success)
				{
					return Ok("Department added successfully.");
				}
				else
				{
					throw new Exception($"Failed to add: {result.ErrorMessage}");
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("AddVacationType")]
		public async Task<IActionResult> AddVacationTypeAsync(VacationTypeDto vacationTypeDto)
		{
			try
			{
				var result = await _vacationService.AddNewVacationTypeAsync(vacationTypeDto);
				if (result.Success)
				{
					return Ok("Added successfully.");
				}
				return StatusCode(500, $"Failed to add: {result.ErrorMessage}");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("AssignSubordinate")]
		public async Task<IActionResult> AssignSubordinate(string supervisor, string subordinate)
		{
			try
			{
				var result = await _employeeService.AssignSubordinateToSupervisorAsync(supervisor, subordinate);
				if (result.Success)
				{
					return Ok("Assigned successfully.");
				}
				return StatusCode(500, $"Failed to add: {result.ErrorMessage}");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
