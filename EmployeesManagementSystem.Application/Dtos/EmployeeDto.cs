using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeesManagementSystem.Application.Dtos
{
	public class EmployeeDto
	{
		public string EmployeeNumber { get; set; }
		public string EmployeeName { get; set; }
		public string DepartmentName { get; set; }
		public string PositionName { get; set; }
		public string? ReportedToEmployeeName { get; set; }
		public int VacationDaysLeft { get; set; }
	}
}
