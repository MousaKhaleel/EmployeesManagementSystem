using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Application.Dtos
{
	public class EmployeeRegisterDto
	{
		[Required, MaxLength(6)]
		public string EmployeeNumber { get; set; }

		[Required, MaxLength(20)]
		public string EmployeeName { get; set; }
		public string UserName { get; set; }

		public int DepartmentId { get; set; }
		public int PositionId { get; set; }

		[Required, MaxLength(1)]
		public string GenderCode { get; set; }

		//[MaxLength(6)]
		//public string? ReportedToEmployeeNumber { get; set; }

		//public ICollection<Employee>? Subordinates { get; set; }

		[Required]
		public decimal Salary { get; set; }

		public string Password { get; set; }
	}
}
