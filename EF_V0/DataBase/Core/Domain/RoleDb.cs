using System.Collections.Generic;

namespace EF_V0.DataBase.Core.Domain
{
	public class RoleDb
	{
		public RoleDb()
		{
			UserRole = new HashSet<UserRoleDb>();
		}
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<UserRoleDb> UserRole { get; set; }
	}
}
