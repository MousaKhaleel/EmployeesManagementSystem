using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Infrastructure.Data
{
	public class DepartmentSeedData
	{
		public static List<Department> GetDepartments()
		{
			var departments = new List<Department>
		{
				new Department { DepartmentName = "Human Resources" },
				new Department { DepartmentName = "Finance" },
				new Department { DepartmentName = "IT" },
				new Department { DepartmentName = "Marketing" },
				new Department { DepartmentName = "Sales" },
				new Department { DepartmentName = "Customer Service" },
				new Department { DepartmentName = "Legal" },
				new Department { DepartmentName = "Operations" },
				new Department { DepartmentName = "Research & Development" },
				new Department { DepartmentName = "Administration" },
				new Department { DepartmentName = "Logistics" },
				new Department { DepartmentName = "Procurement" },
				new Department { DepartmentName = "Production" },
				new Department { DepartmentName = "Quality Assurance" },
				new Department { DepartmentName = "Maintenance" },
				new Department { DepartmentName = "Public Relations" },
				new Department { DepartmentName = "Business Development" },
				new Department { DepartmentName = "Training" },
				new Department { DepartmentName = "Security" },
				new Department { DepartmentName = "Facilities" }
		};
			return departments;
		}
	}
}
