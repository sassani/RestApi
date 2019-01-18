using System;
using System.Collections.Generic;

namespace EF_V0.DataBase.Core.Domain
{
	public class User
	{
		public User()
		{
			UserRole = new HashSet<UserRole>();
		}
		public int Id { get; set; }
		public string PublicId { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public bool IsEmailVerified { get; set; }
		public bool IsActive { get; set; }
		public DateTime? LastLoginAt { get; set; }

		public virtual ICollection<UserRole> UserRole { get; set; }
	}
}
