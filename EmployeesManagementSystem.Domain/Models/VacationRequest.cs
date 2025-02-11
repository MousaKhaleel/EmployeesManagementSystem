using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Domain.Models
{
	public class VacationRequest
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int RequestId { get; set; }

		[Required]
		public DateTime RequestSubmissionDate { get; set; }

		[Required, MaxLength(100)]
		public string Description { get; set; }

		[MaxLength(6)]
		[ForeignKey("EmployeeNumber")]
		public string EmployeeNumber { get; set; }
		public Employee Employee { get; set; }

		[MaxLength(1)]
		[ForeignKey(nameof(VacationType))]
		public string VacationTypeCode { get; set; }
		public VacationType VacationType { get; set; }

		[Required]
		[Column(TypeName = "date")]
		public DateTime StartDate { get; set; }

		[Required]
		[Column(TypeName = "date")]
		public DateTime EndDate { get; set; }

		[Required]
		public int TotalVacationDays { get; set; }

		[Required]
		[ForeignKey("RequestStateId")]
		public int RequestStateId { get; set; }
		public RequestState RequestState { get; set; }

		[MaxLength(6)]
		[ForeignKey("ApprovedByEmployeeNumber")]
		public string? ApprovedByEmployeeNumber { get; set; }
		public Employee? ApprovedBy { get; set; }

		[MaxLength(6)]
		[ForeignKey("DeclinedByEmployeeNumber")]
		public string? DeclinedByEmployeeNumber { get; set; }
		public Employee? DeclinedBy { get; set; }

		public VacationRequest()
		{
			TotalVacationDays = (EndDate - StartDate).Days + 1;
		}
	}
}
