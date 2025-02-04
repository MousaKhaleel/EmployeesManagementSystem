using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Domain.Models
{
	public class RequestState
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int StateId { get; set; }

		[Required, MaxLength(10)]
		public string StateName { get; set; }

		public ICollection<VacationRequest>? VacationRequests { get; set; }
	}
}
