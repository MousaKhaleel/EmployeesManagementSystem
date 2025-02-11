using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Application.Interfaces
{
	public interface IDepartmentService
	{
		Task<(bool Success, string ErrorMessage)> SeedDepartmentsAsync(IEnumerable<Department> departments);
		Task<(bool Success, string ErrorMessage)> AddNewDepartmentAsync(Department department);
		Task<IEnumerable<Department>> GetAllDepartmentsAsync();
	}
}
