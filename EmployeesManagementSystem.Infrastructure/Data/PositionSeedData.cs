using EmployeesManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManagementSystem.Infrastructure.Data
{
	public class PositionSeedData
	{
		public static List<Position> GetPositions()
		{
			var positions = new List<Position>
				{
					new Position { PositionName = "Manager" },
					new Position { PositionName = "Senior Developer" },
					new Position { PositionName = "Junior Developer" },
					new Position { PositionName = "QA Engineer" },
					new Position { PositionName = "DevOps Engineer" },
					new Position { PositionName = "Project Manager" },
					new Position { PositionName = "Business Analyst" },
					new Position { PositionName = "HR Specialist" },
					new Position { PositionName = "System Administrator" },
					new Position { PositionName = "Network Engineer" },
					new Position { PositionName = "Technical Writer" },
					new Position { PositionName = "Product Owner" },
					new Position { PositionName = "Scrum Master" },
					new Position { PositionName = "UX Designer" },
					new Position { PositionName = "UI Designer" },
					new Position { PositionName = "Database Administrator" },
					new Position { PositionName = "Security Analyst" },
					new Position { PositionName = "Support Engineer" },
					new Position { PositionName = "Marketing Manager" },
					new Position { PositionName = "Sales Representative" }
				};
			return positions;
		}
	}
}
