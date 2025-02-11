using EmployeesManagementSystem.Application.Dtos;
using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Domain.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
		private readonly IGenericRepository<Position> _positionRepository;
		private readonly IGenericRepository<Department> _departmentRepository;
		private readonly UserManager<ApplicationUser> _userManager;


		public EmployeeService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IGenericRepository<Position> positionRepository, IGenericRepository<Department> departmentRepository)
		{
			_unitOfWork = unitOfWork;
			_positionRepository = positionRepository;
			_departmentRepository = departmentRepository;
			_userManager = userManager;
		}

		public async Task<(bool Success, string ErrorMessage)> AssignSubordinateToSupervisorAsync(string supervisor, string subordinate)
		{
			try
			{
				var sub = await _unitOfWork.employeeRepository.GetEmployeeByNumberAsync(subordinate);
				var sup = await _unitOfWork.employeeRepository.GetEmployeeByNumberAsync(supervisor);
				if (sub == null || sup == null)
				{
					return (false, "could not find employees.");
				}
				sub.ReportedToEmployee = sup;
				await _unitOfWork.employeeRepository.UpdateAsync(sub);
				await _unitOfWork.SaveChangesAsync();
				return (true, null);
			}
			catch (Exception ex) { return (false, $"An error occurred: {ex.Message} {ex.InnerException?.Message}"); }
		}

		public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
		{
			var allEmployees = await _unitOfWork.employeeRepository.GetAllEmployeesAsync();
			return allEmployees.ToList();
		}
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
				ReportedToEmployeeName = employee.ReportedToEmployee != null ? employee.ReportedToEmployee.EmployeeName : "N/A",
				VacationDaysLeft = employee.VacationDaysLeft,
			};
			return empDto;
		}

		public async Task<IEnumerable<Employee>> GetEmployeesWithPendingVacationRequestsAsync()
		{
			var vacationRequests = await _unitOfWork.employeeRepository.GetEmployeesWithPendingVacationRequestsAsync();
			return vacationRequests;
		}

		public async Task<(bool Success, string ErrorMessage)> SeedEmployeesAsync(IEnumerable<Employee> employees)
		{
			try
			{
				var departments = await _departmentRepository.GetAllAsync();
				var positions = await _positionRepository.GetAllAsync();
				if (!departments.Any() || !positions.Any())
				{
					return (false, "seed departments and positions first");
				}
				var existingEmployees = await _unitOfWork.employeeRepository.GetAllAsync();
				if (existingEmployees.Any())
				{
					return (false, "Employees have already been seeded");
				}
				var usersToAdd = new List<ApplicationUser>();

				foreach (var employee in employees)
				{
					var username = employee.EmployeeName.Replace(" ", "");
					var applicationUser = new ApplicationUser
					{
						UserName = username,
						Employee = employee,
						EmployeeNumber = employee.EmployeeNumber,
					};

					var result = await _userManager.CreateAsync(applicationUser, "password123");
					if (!result.Succeeded)
					{
						return (false, $"Error creating user for {employee.EmployeeName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
					}
					employee.UserId = applicationUser.Id;
					employee.ApplicationUser = applicationUser;

					usersToAdd.Add(applicationUser);
				}
				await _unitOfWork.employeeRepository.AddRangeAsync(employees);
				await _unitOfWork.SaveChangesAsync();
				return (true, "Employees seeded successfully");
			}
			catch (Exception ex)
			{
				return (false, $"An error occurred: {ex.Message} {ex.InnerException?.Message}");
			}
		}

		public async Task<(bool Success, string ErrorMessage)> UpdateEmployeeInfoAsync(string empNum, EmployeeUpdateDto employeeUpdateDto)
		{
			var employee = await _unitOfWork.employeeRepository.GetEmployeeByNumberAsync(empNum);

			var newDepartment = await _departmentRepository.GetByIdAsync(employeeUpdateDto.DepartmentId);
			var newPosition = await _positionRepository.GetByIdAsync(employeeUpdateDto.PositionId);
			var newManager = await _unitOfWork.employeeRepository.GetEmployeeByNumberAsync(employeeUpdateDto.ReportedToEmployeeNumber);

			if (newDepartment == null || newPosition == null || newManager == null)
			{
				return (false, "Invalid department, position, or manager.");
			}
			employee.Department = newDepartment;
			employee.Position = newPosition;
			employee.ReportedToEmployee = newManager;
			employee.EmployeeName = employeeUpdateDto.EmployeeName;
			employee.Salary = employeeUpdateDto.Salary;
			employee.GenderCode = employeeUpdateDto.GenderCode ?? employee.GenderCode;

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
