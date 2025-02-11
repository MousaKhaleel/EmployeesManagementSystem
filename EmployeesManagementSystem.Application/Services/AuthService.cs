using EmployeesManagementSystem.Application.Dtos;
using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Domain.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Domain.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_signInManager = signInManager;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<(bool Success, string ErrorMessage)> RegisterEmployeeAsync(EmployeeRegisterDto employeeRegisterDto)
		{
			ApplicationUser employee = new()
			{
				UserName = employeeRegisterDto.UserName,
			};
			var employeeEntity = new Employee
			{
				EmployeeNumber = employeeRegisterDto.EmployeeNumber,
				EmployeeName = employeeRegisterDto.EmployeeName,
				DepartmentId = employeeRegisterDto.DepartmentId,
				PositionId = employeeRegisterDto.PositionId,
				GenderCode = employeeRegisterDto.GenderCode,
				Salary = employeeRegisterDto.Salary,
				VacationDaysLeft = 24,
				UserId = employee.Id
			};
			var result = await _userManager.CreateAsync(employee, employeeRegisterDto.Password);
			if (!result.Succeeded)
			{
				return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
			}
			var roleResult = await _userManager.AddToRoleAsync(employee, "Employee");
			if (!roleResult.Succeeded)
			{
				return (false, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
			}
			await _unitOfWork.employeeRepository.AddAsync(employeeEntity);
			await _unitOfWork.SaveChangesAsync();
			return (true, string.Empty);
		}

		public async Task<(bool Success, string ErrorMessage)> LoginEmployeeAsync(EmployeeLoginDto employeeLoginDto)
		{
			var result = await _signInManager.PasswordSignInAsync(employeeLoginDto.UserName, employeeLoginDto.Password, false, false);
			if (!result.Succeeded)
			{
				return (false, string.Join(", ", result));
			}
			return (true, string.Empty);
		}

		public async Task LogoutEmployeeAsync()
		{
			await _signInManager.SignOutAsync();
		}

		public async Task<Employee> GetEmployeeProfileAsync()
		{
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			var result = await _unitOfWork.employeeRepository.GetEmployeeByIdAsync(userId);
			return result;
		}

		public async Task<(bool Success, string ErrorMessage)> ChangePasswordAsync(string password)
		{
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			var user = await _userManager.FindByIdAsync(userId);
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var result = await _userManager.ResetPasswordAsync(user, token, password!);
				if (!result.Succeeded)
				{
					return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
				}
				return (true, string.Empty);
		}
	}
}
