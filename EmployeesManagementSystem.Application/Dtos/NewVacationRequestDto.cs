using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Application.Dtos
{
    public class NewVacationRequestDto
    {

		[Required, MaxLength(100)]
		public string Description { get; set; }
		public string EmployeeNumber { get; set; }
		public string VacationTypeCode { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int TotalVacationDays { get; set; }
	}
}
