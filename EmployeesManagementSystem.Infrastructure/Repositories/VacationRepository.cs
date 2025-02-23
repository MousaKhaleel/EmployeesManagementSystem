using EmployeesManagementSystem.Domain.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using EmployeesManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Infrastructure.Repositories
{
	public class VacationRepository : GenericRepository<VacationRequest>, IVacationRepository
	{
		private readonly ApplicationDbContext _context;

		public VacationRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
		//TODO: use select
		public async Task<bool> CheckVacationOverlapAsync(string empNum, DateTime startDate, DateTime endDate)
		{
			return await _context.VacationRequests.AnyAsync(r =>
				r.EmployeeNumber == empNum &&
				r.RequestState.StateName == "Approved" &&
				r.StartDate < endDate &&
				r.EndDate > startDate);
		}

		public async Task<IEnumerable<VacationRequest>> GetPendingVacationRequestsAsync()
		{
			return await _context.VacationRequests.Where(x => x.RequestState.StateName== "Pending").Include(x=>x.Employee).ToListAsync();
		}
		public async Task<IEnumerable<VacationRequest>> GetApprovedVacationRequestsAsync()
		{
			return await _context.VacationRequests.Where(x => x.RequestState.StateName == "Approved").Include(x => x.Employee).ToListAsync();
		}
		public async Task<IEnumerable<VacationRequest>> GetPendingVacationRequestsForSubordinatesAsync(string managerEmpNum)
		{
			return await _context.VacationRequests.Where(vr =>
				vr.Employee.ReportedToEmployeeNumber == managerEmpNum &&
				vr.RequestState.StateName == "Pending").Include(x => x.Employee)
				.ToListAsync();
		}

		public async Task<RequestState> GetRequestStateByNameAsync(string stateName)
		{
			return await _context.RequestStates.Where(x=>x.StateName==stateName).FirstOrDefaultAsync();
		}

		public async Task<bool> CheckVacationOverlapWithinDepartmentAsync(int departmentId, DateTime startDate, DateTime endDate)
		{
			int overlapCount = await _context.VacationRequests
				.CountAsync(v => v.Employee.DepartmentId == departmentId &&
								 v.StartDate <= endDate &&
								 v.EndDate >= startDate);

			return overlapCount >= 3;
		}
	}
}
