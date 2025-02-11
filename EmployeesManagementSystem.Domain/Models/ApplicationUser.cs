using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Domain.Models
{
    public class ApplicationUser : IdentityUser
	{
		[ForeignKey("EmployeeNumber")]
		public string? EmployeeNumber { get; set; }
		public Employee? Employee { get; set; }
	}
}
