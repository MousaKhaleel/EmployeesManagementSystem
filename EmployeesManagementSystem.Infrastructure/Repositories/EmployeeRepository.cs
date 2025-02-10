using EmployeesManagementSystem.Application.Dtos;
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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
	{
		private readonly ApplicationDbContext _context;

		public EmployeeRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
		//TODO: impliment using LINQ
		public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
		{
			return await (from e in _context.Employees
						  join d in _context.Departments on e.DepartmentId equals d.DepartmentId
						  join p in _context.Positions on e.PositionId equals p.PositionId
						  select new Employee
						  {
							  EmployeeNumber = e.EmployeeNumber,
							  EmployeeName = e.EmployeeName,
							  Department = d,
							  Position = p,
						  }).ToListAsync();
			//return await _context.Employees.Include(e => e.Department).Include(e => e.Position).ToListAsync();
		}

		public async Task<IEnumerable<VacationRequest>> GetApprovedVacationRequestsByEmployeeNumberAsync(string empNum)
		{
			return await (from v in _context.VacationRequests
						  where v.EmployeeNumber == empNum && v.RequestStateId == 2
						  select v).ToListAsync();
		}

		public async Task<Employee> GetAsync(Func<Employee, bool> predicate)
		{
			return await Task.Run(() => _context.Employees.FirstOrDefault(predicate));
		}

		public async Task<Employee> GetEmployeeByNumberAsync(object id)
		{
			return await (from e in _context.Employees
						  join d in _context.Departments on e.DepartmentId equals d.DepartmentId
						  join p in _context.Positions on e.PositionId equals p.PositionId
						  where e.EmployeeNumber == id.ToString()
						  select new Employee
						  {
							  EmployeeNumber = e.EmployeeNumber,
							  EmployeeName = e.EmployeeName,
							  Department = d,
							  Position = p,
						  }).FirstOrDefaultAsync();
			//return await _context.Employees.Where(x=>x.EmployeeNumber==id).Include(e => e.Department).Include(e => e.Position).FirstOrDefaultAsync();
		}

		public Task<Employee> GetEmployeeByNumberAsync(string id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<VacationRequest>> GetEmployeesWithPendingVacationRequestsAsync()
		{
			return await (from v in _context.VacationRequests
						  join e in _context.Employees on v.EmployeeNumber equals e.EmployeeNumber
						  where v.RequestStateId == 1
						  select v)
						  .ToListAsync();
		}

		public async Task<bool> UpdateEmployeeInfoAsync(Employee employee)
		{
			_context.Employees.Update(employee);
			//var result = await SaveChangesAsync();
			return true;
		}
	}
}
