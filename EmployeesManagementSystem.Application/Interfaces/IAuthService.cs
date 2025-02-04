using EmployeesManagementSystem.Application.Dtos;
using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Application.Interfaces
{
	public interface IAuthService
	{
		Task<(bool Success, string ErrorMessage)> RegisterEmployeeAsync(EmployeeRegisterDto employeeRegisterDto);
		Task<(bool Success, string ErrorMessage)> LoginEmployeeAsync(EmployeeLoginDto employeeRegisterDto);
		Task LogoutEmployeeAsync();
		Task<Employee> GetEmployeeProfileAsync();//TODO: replace with dto
	}
}
