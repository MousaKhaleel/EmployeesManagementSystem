using EmployeesManagementSystem.Domain.Interfaces;
using EmployeesManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Infrastructure.Repositories
{
	internal class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<T> _dbSet;
		public GenericRepository(ApplicationDbContext dbContext)
		{
			_context = dbContext;
			_dbSet = _context.Set<T>();

		}

		public async Task<T> GetByIdAsync(object id)
		{
			return await _dbSet.FindAsync(id);
		}
		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}

	}
}
