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
		[HttpPost("SeedPositions")]
		public async Task<IActionResult> SeedPositionsAsync()
		{
			var positions = new List<Position>
				{
					new Position { PositionName = "Manager" },
					new Position { PositionName = "Senior Developer" },
					new Position { PositionName = "Junior Developer" },
					new Position { PositionName = "QA Engineer" },
					new Position { PositionName = "DevOps Engineer" },
					new Position { PositionName = "Project Manager" },
					new Position { PositionName = "Business Analyst" },
					new Position { PositionName = "HR Specialist" },
					new Position { PositionName = "System Administrator" },
					new Position { PositionName = "Network Engineer" },
					new Position { PositionName = "Technical Writer" },
					new Position { PositionName = "Product Owner" },
					new Position { PositionName = "Scrum Master" },
					new Position { PositionName = "UX Designer" },
					new Position { PositionName = "UI Designer" },
					new Position { PositionName = "Database Administrator" },
					new Position { PositionName = "Security Analyst" },
					new Position { PositionName = "Support Engineer" },
					new Position { PositionName = "Marketing Manager" },
					new Position { PositionName = "Sales Representative" }
				};
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
	}
}
