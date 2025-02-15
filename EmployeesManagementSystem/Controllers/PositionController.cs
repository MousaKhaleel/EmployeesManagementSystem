using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Application.Services;
using EmployeesManagementSystem.Domain.Models;
using EmployeesManagementSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagementSystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PositionController : ControllerBase
	{
		private readonly IPositionService _positionService;

		public PositionController(IPositionService positionService)
		{
			_positionService = positionService;
		}
		//[Authorize(Roles = "Admin")]
		[HttpPost("SeedPositions")]
		public async Task<IActionResult> SeedPositionsAsync()
		{
			try
			{
				var positions = PositionSeedData.GetPositions();
				var result = await _positionService.SeedPositionsAsync(positions);
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
		[HttpGet("GetAllPositions")]
		public async Task<IActionResult> GetAllPositions()
		{
			try
			{
				var allPositions = await _positionService.GetAllPositionsAsync();
				return Ok(allPositions);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
