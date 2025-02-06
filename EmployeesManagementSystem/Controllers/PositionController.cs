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
	public class PositionController : ControllerBase
	{
		private readonly IPositionService _positionService;

		public PositionController(IPositionService positionService)
		{
			_positionService = positionService;
		}
		[HttpGet("SeedPositions")]
		public async Task SeedPositionsAsync(IEnumerable<Position> positions)
		{
			var result = await _positionService.SeedPositionsAsync(positions);
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
