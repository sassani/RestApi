using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.DataBase.Core.Domain
{
	public class UserRole
	{
		public int Id { get; set; }

		public int UserId { get; set; }
		public virtual User User { get; set; }

		public int RoleId { get; set; }
		public virtual Role Role { get; set; }
	}
}
