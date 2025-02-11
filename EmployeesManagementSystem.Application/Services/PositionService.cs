using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Domain.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Application.Services
{
	public class PositionService : IPositionService
	{
		private readonly IGenericRepository<Position> _positionRepository;
		private readonly IUnitOfWork _unitOfWork;

		public PositionService(IGenericRepository<Position> positionRepository, IUnitOfWork unitOfWork)
		{
			_positionRepository = positionRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<(bool Success, string ErrorMessage)> AddNewPositionAsync(Position position)
		{
			try
			{
				await _positionRepository.AddAsync(position);
				await _unitOfWork.SaveChangesAsync();

				return (true, null);
			}
			catch (Exception ex)
			{
				return (false, ex.Message);
			}
		}

		public async Task<IEnumerable<Position>> GetAllPositionsAsync()
		{
			var all = await _positionRepository.GetAllAsync();
			return all.ToList();
		}

		public async Task<(bool Success, string ErrorMessage)> SeedPositionsAsync(IEnumerable<Position> positions)
		{
			try
			{
				var existingPositions = await _positionRepository.GetAllAsync();
				if (existingPositions.Any())
				{
					return (false, "Positions have already been seeded");
				}
				await _positionRepository.AddRangeAsync(positions);
				await _unitOfWork.SaveChangesAsync();
				return (true, "Positions seeded successfully");
			}
			catch (Exception ex)
			{
				return (false, $"An error occurred while seeding positions: {ex.Message}");
			}
		}
	}
}
