using System.Collections.Generic;

namespace EF_V0.DataBase.Core.Domain
{
	public class Role
	{
		public Role()
		{
			UserRole = new HashSet<UserRole>();
		}
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<UserRole> UserRole { get; set; }
	}
}
