using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Application.Dtos
{
	public class VacationTypeDto
	{
		public string VacationTypeCode { get; set; }
		public string VacationTypeName { get; set; }
	}
}
