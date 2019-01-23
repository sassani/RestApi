using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.DataBase.Core.Domain
{
	public class UserRoleDb
	{
		public int Id { get; set; }

		public int UserId { get; set; }
		public virtual UserDb User { get; set; }

		public int RoleId { get; set; }
		public virtual RoleDb Role { get; set; }
	}
}
