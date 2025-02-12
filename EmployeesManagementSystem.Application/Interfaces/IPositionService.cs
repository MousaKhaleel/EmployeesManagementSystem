﻿using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Application.Interfaces
{
	public interface IPositionService
	{
		Task<(bool Success, string ErrorMessage)> SeedPositionsAsync(IEnumerable<Position> positions);
		Task<(bool Success, string ErrorMessage)> AddNewPositionAsync(Position position);
		Task<IEnumerable<Position>> GetAllPositionsAsync();
	}
}
