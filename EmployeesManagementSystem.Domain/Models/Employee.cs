using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EmployeesManagementSystem.Domain.Models
{
	public class Employee
	{
		[Key]
		[Required, MaxLength(6)]
		[Column(TypeName = "varchar(6)")]
		public string EmployeeNumber { get; set; }

		[Required, MaxLength(20)]
		public string EmployeeName { get; set; }

		[ForeignKey(nameof(DepartmentId))]
		public int DepartmentId { get; set; }
		public Department Department { get; set; }
		[ForeignKey(nameof(PositionId))]
		public int PositionId { get; set; }
		public Position Position { get; set; }

		[Required, MaxLength(1)]
		[Column(TypeName = "char(1)")]
		public string GenderCode { get; set; }

		[MaxLength(6)]
		[ForeignKey("ReportedToEmployeeNumber")]
		public string? ReportedToEmployeeNumber { get; set; }
		public Employee? ReportedToEmployee { get; set; }

		public ICollection<Employee>? Subordinates { get; set; }

		[Required]
		[Range(0, 24)]
		public int VacationDaysLeft { get; set; } = 24;

		[Required]
		[Column(TypeName = "decimal(10,2)")]
		public decimal Salary { get; set; }

		public ICollection<VacationRequest> ApprovedVacationRequests { get; set; }
		public ICollection<VacationRequest> DeclinedVacationRequests { get; set; }

		[ForeignKey("UserId")]
		public string? UserId { get; set; }
		public ApplicationUser? ApplicationUser { get; set; }

		public Employee()//alt
		{
			VacationDaysLeft = 24;
		}
	}
}
