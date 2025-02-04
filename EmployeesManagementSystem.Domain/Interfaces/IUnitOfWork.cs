using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Domain.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<>
	}
}
