﻿using EmployeesManagementSystem.Application.Dtos;
using EmployeesManagementSystem.Application.Interfaces;
using EmployeesManagementSystem.Domain.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Application.Services
{
	public class DepartmentService : IDepartmentService
	{
		private readonly IGenericRepository<Department> _departmentsRepository;
		private readonly IUnitOfWork _unitOfWork;

		public DepartmentService(IGenericRepository<Department> departmentsRepository, IUnitOfWork unitOfWork)
		{
			_departmentsRepository = departmentsRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<(bool Success, string ErrorMessage)> AddNewDepartmentAsync(DepartmentDto departmentDto)
		{
			var department = new Department
			{
				DepartmentName = departmentDto.DepartmentName
			};
			try
			{
				await _departmentsRepository.AddAsync(department);
				await _unitOfWork.SaveChangesAsync();

				return (true, null);
			}
			catch (Exception ex)
			{
				return (false, ex.Message);
			}
		}

		public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
		{
			var all = await _departmentsRepository.GetAllAsync();
			return all.ToList();
		}

		public async Task<(bool Success, string ErrorMessage)> SeedDepartmentsAsync(IEnumerable<Department> departments)
		{
			try
			{
				var existingDepartments = await _departmentsRepository.GetAllAsync();
				if (existingDepartments.Any())
				{
					return (false, "Departments have already been seeded");
				}
				await _departmentsRepository.AddRangeAsync(departments);
				await _unitOfWork.SaveChangesAsync();
				return (true, "Departments seeded successfully");
			}
			catch (Exception ex)
			{
				return (false, $"An error occurred while seeding departments: {ex.Message}");
			}
		}
	}
}
