using EmployeesManagementSystem.Application.Dtos;
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
	public class EmployeeService : IEmployeeService
	{
		private readonly IEmployeeRepository _employeeRepository;

		public EmployeeService(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}
		public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
		{
			var allEmployees = await _employeeRepository.GetAllAsync();//change for linq
			return allEmployees.ToList();
		}

		public async Task<IEnumerable<ApprovedVacationDto>> GetEmployeeApprovedVacationRequestsHistoryAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<EmployeeDto> GetEmployeeByNumberAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Employee>> GetEmployeesPendingVacationRequestsAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<(bool Success, string ErrorMessage)> SeedEmployeesAsync(IEnumerable<Employee> employees)
		{
			try
			{
				var existingEmployees = await _employeeRepository.GetAllAsync();
				if (existingEmployees.Any())
				{
					return (false, "Employees have already been seeded");
				}
				await _employeeRepository.AddRangeAsync(employees);
				await _employeeRepository.SaveChangesAsync();
				return (true, "Employees seeded successfully");
			}
			catch (Exception ex)
			{
				return (false, $"An error occurred while seeding employees: {ex.Message}");
			}
		}
	}
}
