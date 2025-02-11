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

		[HttpGet("GetAllPendingVacationRequests")]
		public async Task<IActionResult> GetPendingVacationRequests()
		{
			var pendingRequests = await _vacationService.GetAllPendingVacationRequestsAsync();
			return Ok(pendingRequests);
		}
		[HttpGet("GetPendingVacationRequestsForSubordinates/{managerEmpNum}")]
		public async Task<IActionResult> GetPendingVacationRequestsForSubordinatesAsync(string managerEmpNum)
		{
			var pendingRequests = await _vacationService.GetPendingVacationRequestsForSubordinatesAsync(managerEmpNum);
			return Ok(pendingRequests);
		}

		[HttpPost("Approve/{requestId}")]
		public async Task<IActionResult> ApproveVacationRequest(int requestId)
		{
			var result = await _vacationService.ApproveVacationRequestAsync(requestId);
			if (result)
			{
				return Ok("Vacation request approved");
			}
			return BadRequest("Failed to approve vacation request");
		}

		[HttpPost("Decline/{requestId}")]
		public async Task<IActionResult> DeclineVacationRequest(int requestId)
		{
			var result = await _vacationService.DeclineVacationRequestAsync(requestId);
			if (result)
			{
				return Ok("Vacation request declined");
			}
			return BadRequest("Failed to decline vacation request");
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
			var result = await _vacationService.SeedVacationTypesAsync(vacationTypes);
			if (result.Success)
			{
				return Ok("Vacation types seeded successfully.");
			}
			return StatusCode(500, $"Failed to seed vacation types: {result.ErrorMessage}");
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
			var result = await _vacationService.SeedRequestStatesAsync(requestStates);
			if (result.Success)
			{
				return Ok("Request states seeded successfully.");
			}
			return StatusCode(500, $"Failed to seed request states: {result.ErrorMessage}");
		}

		[HttpGet("GetAllApprovedRequests")]
		public async Task<IActionResult> GetAllApprovedRequests()
		{
			var approvedRequests = await _vacationService.GetAllApprovedRequestsAsync();
			return Ok(approvedRequests);
		}
	}
}
