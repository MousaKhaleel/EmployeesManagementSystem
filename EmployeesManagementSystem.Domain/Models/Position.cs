﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Domain.Models
{
	public class Position
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PositionId { get; set; }

		[Required, MaxLength(30)]
		public string PositionName { get; set; }
		public ICollection<Employee>? Employees { get; set; }
	}
}
