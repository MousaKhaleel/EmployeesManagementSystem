using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Domain.Interfaces
{
	public interface IVacationRepository : IGenericRepository<VacationRequest>
	{
		Task<bool> CheckVacationOverlapAsync(string empNum, DateTime startDate, DateTime endDate);
		Task<IEnumerable<VacationRequest>> GetPendingVacationRequestsAsync();
		Task<IEnumerable<VacationRequest>> GetApprovedVacationRequestsAsync();
		Task<IEnumerable<VacationRequest>> GetPendingVacationRequestsForSubordinatesAsync(string managerEmpNum);

		Task<RequestState> GetRequestStateByNameAsync(string stateName);
	}
}
