using EmployeesManagementSystem.Domain.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using EmployeesManagementSystem.Infrastructure.Data;
using EmployeesManagementSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Infrastructure.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
        public IEmployeeRepository employeeRepository { get; private set; }

		public IVacationRepository vacationRepository { get; private set; }

		public UnitOfWork(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
			employeeRepository = new EmployeeRepository(_context);
			vacationRepository= new VacationRepository(_context);
		}
		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
