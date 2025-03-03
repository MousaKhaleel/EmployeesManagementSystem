﻿using EmployeesManagementSystem.Application.Dtos;
using EmployeesManagementSystem.Application.Interfaces;
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
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public VacationService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IGenericRepository<VacationType> vacationTypesRepository, IGenericRepository<RequestState> requestStatesRepository)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
			_vacationTypesRepository = vacationTypesRepository;
			_requestStatesRepository = requestStatesRepository;
		}

		public async Task<(bool Success, string ErrorMessage)> AddNewVacationTypeAsync(VacationTypeDto vacationTypeDto)
		{
			var vacationType = new VacationType
			{
				VacationTypeCode = vacationTypeDto.VacationTypeCode.ToUpper(),
				VacationTypeName = vacationTypeDto.VacationTypeName,
			};
			try
			{
				await _vacationTypesRepository.AddAsync(vacationType);
				await _unitOfWork.SaveChangesAsync();

				return (true, null);
			}
			catch (Exception ex)
			{
				return (false, ex.Message);
			}
		}

		public async Task<(bool Success, string ErrorMessage)> ApproveVacationRequestAsync(int requestId)
		{
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			if (userId == null)
			{
				return (false, "Login first");
			}
			var reviwer = await _unitOfWork.employeeRepository.GetEmployeeByIdAsync(userId);

			var request = await _unitOfWork.vacationRepository.GetByIdAsync(requestId);
			if (request == null)
			{
				return (false, "request does not exist.");
			}

			var employeeRequesting = await _unitOfWork.employeeRepository.GetEmployeeByNumberAsync(request.EmployeeNumber);
			if (employeeRequesting.ReportedToEmployeeNumber != reviwer.EmployeeNumber)
			{
				return (false, "Can not approve if the employee is not your subordante");
			}
			var vacationDaysTaken = (request.EndDate - request.StartDate).Days;
			if (vacationDaysTaken > employeeRequesting.VacationDaysLeft)
			{
				return (false, "no enough vacation days");
			}
			var approvedState = await _unitOfWork.vacationRepository.GetRequestStateByNameAsync("Approved");
			request.RequestState = approvedState;
			request.ApprovedByEmployeeNumber = reviwer.EmployeeNumber;
			//TODO:		Create method to update vacation days balance after approve any vacation request
			//which the logic of this method is to decrease employee vacation days left.
			employeeRequesting.VacationDaysLeft -= vacationDaysTaken;
			await _unitOfWork.vacationRepository.UpdateAsync(request);
			await _unitOfWork.employeeRepository.UpdateAsync(employeeRequesting);
			await _unitOfWork.SaveChangesAsync();
			return (true, null);
		}

		public async Task<(bool Success, string ErrorMessage)> DeclineVacationRequestAsync(int requestId)
		{
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			if (userId == null)
			{
				return (false, "Login first");
			}
			var reviwer = await _unitOfWork.employeeRepository.GetEmployeeByIdAsync(userId);
			var request = await _unitOfWork.vacationRepository.GetByIdAsync(requestId);
			if (request == null)
			{
				return (false, "request does not exist.");
			}

			var employeeRequesting = await _unitOfWork.employeeRepository.GetEmployeeByNumberAsync(request.EmployeeNumber);

			if (employeeRequesting.ReportedToEmployeeNumber != reviwer.EmployeeNumber)
			{
				return (false, "Can not approve if the employee is not your subordante");
			}
			var declinedState = await _unitOfWork.vacationRepository.GetRequestStateByNameAsync("Declined");
			request.RequestState = declinedState;
			request.DeclinedByEmployeeNumber = reviwer.EmployeeNumber;
			await _unitOfWork.vacationRepository.UpdateAsync(request);
			await _unitOfWork.SaveChangesAsync();
			return (true, null);
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
		public async Task<bool> IsVacationOverlappingWithinDepartmentAsync(string empNum, DateTime startDate, DateTime endDate)
		{
			var emp = await _unitOfWork.employeeRepository.GetEmployeeByNumberAsync(empNum);
			var overlap = await _unitOfWork.vacationRepository.CheckVacationOverlapWithinDepartmentAsync(emp.DepartmentId, startDate, endDate);
			if (overlap)
			{
				return true;
			}
			return false;
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
				return (false, $"An error occurred while seeding employees: {ex.Message} {ex.InnerException?.Message}");
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

		public async Task<bool> SubmitVacationRequestAsync(NewVacationRequestDto newVacationRequestDto)
		{
			if (await IsVacationOverlappingAsync(newVacationRequestDto.EmployeeNumber, newVacationRequestDto.StartDate, newVacationRequestDto.EndDate))
				return false;
			var request = new VacationRequest
			{
				Description = newVacationRequestDto.Description,
				EmployeeNumber = newVacationRequestDto.EmployeeNumber,
				VacationTypeCode = newVacationRequestDto.VacationTypeCode,
				StartDate = newVacationRequestDto.StartDate,
				EndDate = newVacationRequestDto.EndDate,
				TotalVacationDays = (newVacationRequestDto.EndDate - newVacationRequestDto.StartDate).Days + 1,
				RequestStateId = 1,
			};
			await _unitOfWork.vacationRepository.AddAsync(request);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}
	}
}
