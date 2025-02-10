using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeesManagementSystem.Application.Dtos
{
	public class PendingVacationDto
	{
		public string Description { get; set; }
		public string EmployeeNumber { get; set; }
		public string EmployeeName { get; set; }
		public DateTime RequestSubmissionDate { get; set; }
		public int VacationDurationInDays => (EndDate - StartDate).Days;
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public decimal Salary { get; set; }
	}
}
