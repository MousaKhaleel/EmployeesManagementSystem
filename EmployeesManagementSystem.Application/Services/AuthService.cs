using EmployeesManagementSystem.Application.Dtos;
using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Domain.Interfaces;
using EmployeesManagementSystem.Domain.Models;
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
		private readonly IGenericRepository<Employee> _employeeRepository;//TODO: replace with the unit of work (will need to?)
		private readonly UserManager<Employee> _userManager;
		private readonly SignInManager<Employee> _signInManager;

		public AuthService(IGenericRepository<Employee> employeeRepository, UserManager<Employee> userManager, SignInManager<Employee> signInManager)
		{
			_employeeRepository = employeeRepository;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<(bool Success, string ErrorMessage)> RegisterEmployeeAsync(EmployeeRegisterDto employeeRegisterDto)
		{
			Employee employee = new()
			{
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
			await _employeeRepository.SaveChangesAsync();
			return (true, string.Empty);
		}

		public async Task<(bool Success, string ErrorMessage)> LoginEmployeeAsync(EmployeeLoginDto employeeLoginDto)
		{
			var result = await _signInManager.PasswordSignInAsync(employeeLoginDto.EmployeeName, employeeLoginDto.Password, false, false);
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
			var userId = _userManager.GetUserId(User);
			var result = await _employeeRepository.GetByIdAsync(userId);
			throw new NotImplementedException();
		}
	}
}
