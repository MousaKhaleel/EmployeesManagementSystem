﻿using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Domain.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Application.Services
{
	public class VacationService : IVacationService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<VacationType> _vacationTypesRepository;
		private readonly IGenericRepository<RequestState> _requestStatesRepository;
		private readonly UserManager<Employee> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public VacationService(IUnitOfWork unitOfWork, UserManager<Employee> userManager, IHttpContextAccessor httpContextAccessor, IGenericRepository<VacationType> vacationTypesRepository, IGenericRepository<RequestState> requestStatesRepository)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
			_vacationTypesRepository = vacationTypesRepository;
			_requestStatesRepository = requestStatesRepository;
		}
		//TODO:
		public async Task<bool> ApproveVacationRequestAsync(int requestId)
		{
			//TODO: check if superviser
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			var request = await _unitOfWork.vacationRepository.GetByIdAsync(requestId);
			var vacationDaysTaken = (request.EndDate - request.StartDate).Days;
			var approvedForEmp = await _unitOfWork.employeeRepository.GetByIdAsync(request.EmployeeNumber);
			if (vacationDaysTaken < approvedForEmp.VacationDaysLeft)
			{
				return false;
			}
			request.RequestState.StateName = "Approved";
			request.ApprovedByEmployeeNumber = userId;
			approvedForEmp.VacationDaysLeft -= vacationDaysTaken;
			await _unitOfWork.vacationRepository.UpdateAsync(request);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeclineVacationRequestAsync(int requestId)
		{
			//TODO: check if superviser
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			var request = await _unitOfWork.vacationRepository.GetByIdAsync(requestId);
			request.RequestState.StateName = "Declined";
			request.DeclinedByEmployeeNumber = userId;
			await _unitOfWork.vacationRepository.UpdateAsync(request);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<VacationRequest>> GetAllApprovedRequestsAsync()
		{
			return await _unitOfWork.vacationRepository.GetApprovedVacationRequestsAsync();
		}

		public async Task<IEnumerable<VacationRequest>> GetAllPendingVacationRequestsAsync()
		{
			return await _unitOfWork.vacationRepository.GetPendingVacationRequestsAsync();
		}

		public async Task<IEnumerable<VacationRequest>> GetPendingVacationRequestsForSubordinatesAsync(string managerEmpNum)
		{
			var subordinates = await _unitOfWork.vacationRepository.GetPendingVacationRequestsForSubordinatesAsync(managerEmpNum);
			return subordinates;
		}

		public async Task<bool> IsVacationOverlappingAsync(string empNum, DateTime startDate, DateTime endDate)
		{
			return await _unitOfWork.vacationRepository.CheckVacationOverlapAsync(empNum, startDate, endDate);
		}

		public async Task<(bool Success, string ErrorMessage)> SeedRequestStatesAsync(IEnumerable<RequestState> requestStates)
		{
			try
			{
				var existingRequestStates = await _requestStatesRepository.GetAllAsync();
				if (existingRequestStates.Any())
				{
					return (false, "Request States have already been seeded");
				}
				await _requestStatesRepository.AddRangeAsync(requestStates);
				await _unitOfWork.SaveChangesAsync();
				return (true, "Request States seeded successfully");
			}
			catch (Exception ex)
			{
				return (false, $"An error occurred while seeding Request States: {ex.Message}");
			}
		}

		public async Task<(bool Success, string ErrorMessage)> SeedVacationTypesAsync(IEnumerable<VacationType> vacationTypes)
		{
			try
			{
				var existingVacationTypes = await _vacationTypesRepository.GetAllAsync();
				if (existingVacationTypes.Any())
				{
					return (false, "Vacation Types have already been seeded");
				}
				await _vacationTypesRepository.AddRangeAsync(vacationTypes);
				await _unitOfWork.SaveChangesAsync();
				return (true, "acation Types seeded successfully");
			}
			catch (Exception ex)
			{
				return (false, $"An error occurred while seeding Vacation Types: {ex.Message}");
			}
		}

		public async Task<bool> SubmitVacationRequestAsync(VacationRequest request)
		{
			if (await IsVacationOverlappingAsync(request.EmployeeNumber, request.StartDate, request.EndDate))
				return false;

			await _unitOfWork.vacationRepository.AddAsync(request);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}
	}
}
