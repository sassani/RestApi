using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.DataBase.Core.Domain
{
	public class UserClientDb
	{
		public int Id { get; set; }
		public string RefreshToken { get; set; }

		public int UserId { get; set; }
		public virtual UserDb User { get; set; }

		public int ClientId { get; set; }
		public virtual ClientDb Client { get; set; }

		public string Platform { get; set; }
		public string Browser { get; set; }
		public string IP { get; set; }


		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
