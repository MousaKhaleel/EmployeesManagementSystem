using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Domain.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
	{
		Task<IEnumerable<Employee>> GetAllEmployeesAsync();
		Task<Employee> GetEmployeeByNumberAsync(string id);
		Task<Employee> GetAsync(Func<Employee, bool> predicate);
		Task<bool> UpdateEmployeeInfoAsync(Employee employee);
		Task<IEnumerable<VacationRequest>> GetEmployeesWithPendingVacationRequestsAsync();
		Task<IEnumerable<VacationRequest>> GetApprovedVacationRequestsByEmployeeNumberAsync(string empNum);

	}
}
