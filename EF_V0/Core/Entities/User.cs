using System;
using System.Collections.Generic;

namespace EF_V0.Core.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string PublicId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; private set; }
		public string RefreshToken { get; set; }

		public bool IsEmailVerified { get; set; }
		public bool IsActive { get; set; }
		public DateTime? LastLoginAt { get; set; }

		public HashSet<string> Roles { get; set; }


		public User()
		{
			RefreshToken = "";
			Roles = new HashSet<string>();
		}

	}

}
