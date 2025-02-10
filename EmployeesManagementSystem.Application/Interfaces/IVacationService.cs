using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Application.Interfaces
{
	public interface IVacationService
	{
		Task<bool> SubmitVacationRequestAsync(VacationRequest request);
		Task<bool> ApproveVacationRequestAsync(int requestId);
		Task<bool> DeclineVacationRequestAsync(int requestId);
		Task<(bool Success, string ErrorMessage)> SeedVacationTypesAsync(IEnumerable<VacationType> vacationTypes);
		Task<(bool Success, string ErrorMessage)> SeedRequestStatesAsync(IEnumerable<RequestState> requestStates);
		Task<IEnumerable<VacationRequest>> GetAllApprovedRequestsAsync();
		Task<bool> IsVacationOverlappingAsync(string empNum, DateTime startDate, DateTime endDate);
		Task<IEnumerable<VacationRequest>> GetAllPendingVacationRequestsAsync();//TODO: replace w dto
		Task<IEnumerable<VacationRequest>> GetPendingVacationRequestsForSubordinatesAsync(string managerEmpNum);//TODO: replace w dto

	}
}
