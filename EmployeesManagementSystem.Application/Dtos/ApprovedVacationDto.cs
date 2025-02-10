using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeesManagementSystem.Application.Dtos
{
	public class ApprovedVacationDto
	{
		public string VacationTypeCode { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int TotalVacationDays { get; set; }
		public string ApprovedByEmployeeName { get; set; }
	}
}
