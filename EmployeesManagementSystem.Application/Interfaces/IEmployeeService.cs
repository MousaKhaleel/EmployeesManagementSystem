using EmployeesManagementSystem.Application.Dtos;
using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Application.Interfaces
{
    public interface IEmployeeService
    {
		Task<IEnumerable<Employee>> GetAllEmployeesAsync();
		Task<EmployeeDto> GetEmployeeByNumberAsync();
		Task<IEnumerable<Employee>> GetEmployeesPendingVacationRequestsAsync();
		Task<(bool Success, string ErrorMessage)> SeedEmployeesAsync(IEnumerable<Employee> employees);
		Task<IEnumerable<ApprovedVacationDto>> GetEmployeeApprovedVacationRequestsHistoryAsync();

	}
}
