using EmployeesManagementSystem.Domain.Interfaces;
using EmployeesManagementSystem.Domain.Models;
using EmployeesManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Infrastructure.Repositories
{
    class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
	{
		private readonly ApplicationDbContext _context;

		public EmployeeRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
