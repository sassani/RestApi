using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.Core.Models.Requests
{
	public class LoginUser
	{
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string GrantType { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public string RefreshToken { get; set; }
		public int UserApplicationId { get; set; }
	}
}
