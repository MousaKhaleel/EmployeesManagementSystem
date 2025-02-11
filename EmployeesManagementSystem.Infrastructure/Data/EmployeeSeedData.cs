using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Infrastructure.Data
{
	public class EmployeeSeedData
	{
		public static List<Employee> GetEmployees()
		{
			var employees = new List<Employee>
	{
		new Employee
		{
			EmployeeNumber = "EMP001",
			EmployeeName = "John Doe",
			DepartmentId = 1,
			PositionId = 1,
			GenderCode = "M",
			Salary = 50000.00m,
			ReportedToEmployeeNumber = null
		},
		new Employee
		{
			EmployeeNumber = "EMP002",
			EmployeeName = "Jane Smith",
			DepartmentId = 2,
			PositionId = 2,
			GenderCode = "F",
			Salary = 55000.00m,
			ReportedToEmployeeNumber = "EMP001"
		},
		new Employee
		{
			EmployeeNumber = "EMP003",
			EmployeeName = "Alice Johnson",
			DepartmentId = 1,
			PositionId = 3,
			GenderCode = "F",
			Salary = 60000.00m,
			ReportedToEmployeeNumber = "EMP001"
		},
		new Employee
		{
			EmployeeNumber = "EMP004",
			EmployeeName = "Bob Brown",
			DepartmentId = 3,
			PositionId = 4,
			GenderCode = "M",
			Salary = 45000.00m,
			ReportedToEmployeeNumber = "EMP002"
		},
		new Employee
		{
			EmployeeNumber = "EMP005",
			EmployeeName = "Charlie Davis",
			DepartmentId = 2,
			PositionId = 5,
			GenderCode = "M",
			Salary = 70000.00m,
			ReportedToEmployeeNumber = "EMP002"
		},
			new Employee
	{
		EmployeeNumber = "EMP006",
		EmployeeName = "Robert Johnson",
		DepartmentId = 13,
		PositionId = 6,
		GenderCode = "M",
		Salary = 52000.00m,
		ReportedToEmployeeNumber = "EMP004"
	},
	new Employee
	{
		EmployeeNumber = "EMP007",
		EmployeeName = "Emily Davis",
		DepartmentId = 10,
		PositionId = 7,
		GenderCode = "F",
		Salary = 58000.00m,
		ReportedToEmployeeNumber = "EMP001"
	},
	new Employee
	{
		EmployeeNumber = "EMP008",
		EmployeeName = "Samuel Lee",
		DepartmentId = 1,
		PositionId = 8,
		GenderCode = "M",
		Salary = 61000.00m,
		ReportedToEmployeeNumber = "EMP003"
	},
	new Employee
	{
		EmployeeNumber = "EMP009",
		EmployeeName = "Olivia Martin",
		DepartmentId = 7,
		PositionId = 9,
		GenderCode = "F",
		Salary = 57000.00m,
		ReportedToEmployeeNumber = "EMP002"
	},
	new Employee
	{
		EmployeeNumber = "EMP010",
		EmployeeName = "Ethan Clark",
		DepartmentId = 2,
		PositionId = 10,
		GenderCode = "M",
		Salary = 63000.00m,
		ReportedToEmployeeNumber = "EMP002"
	}
	};
			return employees;
		}
	}
}
