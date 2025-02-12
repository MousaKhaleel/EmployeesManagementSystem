﻿using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Application.Dtos
{
	public class EmployeeUpdateDto
	{
		public int DepartmentId { get; set; }
		public int PositionId { get; set; }
		public string EmployeeName { get; set; }
		public decimal Salary { get; set; }
		public string ReportedToEmployeeNumber { get; set; }
		public string GenderCode { get; set; }
	}
}
