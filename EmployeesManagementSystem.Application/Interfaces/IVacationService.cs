﻿using EmployeesManagementSystem.Application.Dtos;
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
		Task<bool> SubmitVacationRequestAsync(NewVacationRequestDto newVacationRequestDto);
		Task<(bool Success, string ErrorMessage)> ApproveVacationRequestAsync(int requestId);
		Task<(bool Success, string ErrorMessage)> DeclineVacationRequestAsync(int requestId);
		Task<(bool Success, string ErrorMessage)> SeedVacationTypesAsync(IEnumerable<VacationType> vacationTypes);
		Task<(bool Success, string ErrorMessage)> SeedRequestStatesAsync(IEnumerable<RequestState> requestStates);
		Task<IEnumerable<VacationRequest>> GetAllApprovedRequestsAsync();
		Task<bool> IsVacationOverlappingAsync(string empNum, DateTime startDate, DateTime endDate);
		Task<bool> IsVacationOverlappingWithinDepartmentAsync(string empNum, DateTime startDate, DateTime endDate);
		Task<IEnumerable<VacationRequest>> GetAllPendingVacationRequestsAsync();//TODO: replace w dto
		Task<IEnumerable<VacationRequest>> GetPendingVacationRequestsForSubordinatesAsync(string managerEmpNum);//TODO: replace w dto

		Task<(bool Success, string ErrorMessage)> AddNewVacationTypeAsync(VacationTypeDto vacationTypeDto);
	}
}
