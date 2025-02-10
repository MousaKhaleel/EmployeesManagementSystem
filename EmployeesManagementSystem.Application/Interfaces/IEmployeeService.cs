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
		Task<EmployeeDto> GetEmployeeByNumberAsync(string empNum);
		Task<(bool Success, string ErrorMessage)> UpdateEmployeeInfoAsync(string empNum, EmployeeUpdateDto employeeUpdateDto);
		Task<IEnumerable<Employee>> GetEmployeesWithPendingVacationRequestsAsync(); //TODO: Dto
		Task<(bool Success, string ErrorMessage)> SeedEmployeesAsync(IEnumerable<Employee> employees);
		Task<IEnumerable<ApprovedVacationDto>> GetEmployeeApprovedVacationRequestsHistoryAsync(string empNum);

	}
}
