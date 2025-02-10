using EmployeesManagementSystem.Application.Dtos;
using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Domain.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
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
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<Employee> _userManager;


		public EmployeeService(IUnitOfWork unitOfWork, UserManager<Employee> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}
		public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
		{
			var allEmployees = await _unitOfWork.employeeRepository.GetAllEmployeesAsync();
			return allEmployees.ToList();
		}
		//TODO:
		public async Task<IEnumerable<ApprovedVacationDto>> GetEmployeeApprovedVacationRequestsHistoryAsync(string empNum)
		{
			var approvedHistory = await _unitOfWork.employeeRepository.GetApprovedVacationRequestsByEmployeeNumberAsync(empNum);
			var approvedVacationDtos = approvedHistory.Select(request => new ApprovedVacationDto
			{
				ApprovedByEmployeeName = request.ApprovedByEmployeeNumber,
				Description = request.Description,
				StartDate = request.StartDate,
				EndDate = request.EndDate,
				TotalVacationDays = request.TotalVacationDays,
				VacationTypeCode = request.VacationTypeCode,
			}).ToList();

			return approvedVacationDtos;
		}

		public async Task<EmployeeDto> GetEmployeeByNumberAsync(string empNum)
		{
			var employee = await _unitOfWork.employeeRepository.GetEmployeeByNumberAsync(empNum);
			var empDto = new EmployeeDto
			{
				EmployeeNumber = employee.EmployeeNumber,
				EmployeeName = employee.EmployeeName,
				PositionName = employee.Position.PositionName,
				DepartmentName = employee.Department.DepartmentName,
				ReportedToEmployeeName = employee.ReportedToEmployee.EmployeeName,
				VacationDaysLeft = employee.VacationDaysLeft,
			};
			return empDto;
		}

		public async Task<IEnumerable<Employee>> GetEmployeesWithPendingVacationRequestsAsync()
		{
			var vacationRequests = await _unitOfWork.employeeRepository.GetEmployeesWithPendingVacationRequestsAsync();
			return vacationRequests.Select(vr => vr.Employee).Distinct();
		}

		public async Task<(bool Success, string ErrorMessage)> SeedEmployeesAsync(IEnumerable<Employee> employees)
		{
			try
			{
				var existingEmployees = await _unitOfWork.employeeRepository.GetAllAsync();
				if (existingEmployees.Any())
				{
					return (false, "Employees have already been seeded");
				}
				await _unitOfWork.employeeRepository.AddRangeAsync(employees);
				await _unitOfWork.SaveChangesAsync();
				return (true, "Employees seeded successfully");
			}
			catch (Exception ex)
			{
				return (false, $"An error occurred while seeding employees: {ex.Message}");
			}
		}

		public async Task<(bool Success, string ErrorMessage)> UpdateEmployeeInfoAsync(string empNum, EmployeeUpdateDto employeeUpdateDto)
		{
			var employee = await _unitOfWork.employeeRepository.GetEmployeeByNumberAsync(empNum);

			employee.Department.DepartmentId = employeeUpdateDto.DepartmentId;
			employee.Position.PositionId = employeeUpdateDto.PositionId;
			employee.ReportedToEmployee.EmployeeNumber = employeeUpdateDto.ReportedToEmployeeNumber;

			var updateResult = await _unitOfWork.employeeRepository.UpdateEmployeeInfoAsync(employee);
			await _unitOfWork.SaveChangesAsync();
			if (updateResult == false)
			{
				return (false, "Failed to update employee information.");
			}

			return (true, null);
		}
	}
}
