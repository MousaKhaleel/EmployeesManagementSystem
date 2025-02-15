using EmployeesManagementSystem.Application.Dtos;
using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Application.Services;
using EmployeesManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeesManagementSystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VacationController : ControllerBase
	{
		private readonly IVacationService _vacationService;

		public VacationController(IVacationService vacationService)
		{
			_vacationService = vacationService;
		}
		[HttpPost("SubmitVacationRequest")]
		public async Task<IActionResult> SubmitVacationRequest(NewVacationRequestDto newVacationRequestDto)
		{
			try
			{
				//var overlapWithinDepartment = await _vacationService.IsVacationOverlappingWithinDepartmentAsync(newVacationRequestDto.EmployeeNumber, newVacationRequestDto.StartDate, newVacationRequestDto.EndDate);
				//if (overlapWithinDepartment)
				//{
				//	return BadRequest("Too many employees on leave in your department.");
				//}
				var isOverlapping = await _vacationService.IsVacationOverlappingAsync(newVacationRequestDto.EmployeeNumber, newVacationRequestDto.StartDate, newVacationRequestDto.EndDate);
				if (isOverlapping)
				{
					return BadRequest("Vacation request overlaps with an existing request");
				}

				var result = await _vacationService.SubmitVacationRequestAsync(newVacationRequestDto);
				if (result)
				{
					return Ok("Vacation request submitted successfully");
				}
				return StatusCode(500, "Failed to submit vacation request");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("GetAllPendingVacationRequests")]
		public async Task<IActionResult> GetPendingVacationRequests()
		{
			try
			{
				var pendingRequests = await _vacationService.GetAllPendingVacationRequestsAsync();
				return Ok(pendingRequests);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("GetPendingVacationRequestsForSubordinates/{managerEmpNum}")]
		public async Task<IActionResult> GetPendingVacationRequestsForSubordinatesAsync(string managerEmpNum)
		{
			try
			{
				var pendingRequests = await _vacationService.GetPendingVacationRequestsForSubordinatesAsync(managerEmpNum);
				return Ok(pendingRequests);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("Approve/{requestId}")]
		public async Task<IActionResult> ApproveVacationRequest(int requestId)
		{
			try
			{
				var result = await _vacationService.ApproveVacationRequestAsync(requestId);
				if (!result.Success)
				{
					return BadRequest(result.ErrorMessage);
				}
				return Ok("Vacation request approved");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("Decline/{requestId}")]
		public async Task<IActionResult> DeclineVacationRequest(int requestId)
		{
			try
			{
				var result = await _vacationService.DeclineVacationRequestAsync(requestId);
				if (!result.Success)
				{
					return Ok("Vacation request declined");
				}
				return BadRequest("Failed to decline vacation request");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//[Authorize(Roles = "Admin")]
		[HttpPost("SeedVacationTypes")]
		public async Task<IActionResult> SeedVacationTypesAsync()
		{

			var vacationTypes = new List<VacationType>
			{
				new VacationType
				{
				VacationTypeCode= "S",
				VacationTypeName= "Sick"
				},
				new VacationType{
				VacationTypeCode= "U",
				VacationTypeName= "Unpaid"
				},
				new VacationType{
				VacationTypeCode= "A",
				VacationTypeName= "Annual"
				},
				new VacationType{
				VacationTypeCode= "O",
				VacationTypeName= "Day Off"
				},
				new VacationType{
				VacationTypeCode= "B",
				VacationTypeName= "Business Trip"
				},
			};
			try
			{
				var result = await _vacationService.SeedVacationTypesAsync(vacationTypes);
				if (result.Success)
				{
					return Ok("Vacation types seeded successfully.");
				}
				return StatusCode(500, $"Failed to seed vacation types: {result.ErrorMessage}");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//[Authorize(Roles = "Admin")]
		[HttpPost("SeedRequestStates")]
		public async Task<IActionResult> SeedRequestStatesAsync()
		{
			var requestStates = new List<RequestState>
			{
				new RequestState{ StateName = "Pending" },
				new RequestState{ StateName = "Approved" },
				new RequestState{ StateName = "Declined" },
			};
			try
			{
				var result = await _vacationService.SeedRequestStatesAsync(requestStates);
				if (result.Success)
				{
					return Ok("Request states seeded successfully.");
				}
				return StatusCode(500, $"Failed to seed request states: {result.ErrorMessage}");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("GetAllApprovedRequests")]
		public async Task<IActionResult> GetAllApprovedRequests()
		{
			try
			{
				var approvedRequests = await _vacationService.GetAllApprovedRequestsAsync();
				return Ok(approvedRequests);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
