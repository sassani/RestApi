using EF_V0.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.Core.Models.Responses
{
	public class AccessToken
	{
		[JsonProperty(propertyName: "uid")]
		public string UserId { get; set; }

		[JsonProperty(propertyName: "aid")]
		public int UserApplicationId { get; set; }

		[JsonProperty(propertyName: "exp")]
		public long Expiration { get; set; }

		[JsonProperty(propertyName: "roles")]
		public HashSet<string> Roles { get; set; }

		public AccessToken(string userId,  HashSet<string> roles, int userApplicationId=1)
		{
			UserId = userId;
			UserApplicationId = userApplicationId;
			Roles = roles;
			Expiration = DateTimeHelper.GetUnixTimestamp(DateTime.Now.AddMinutes(50));
		}
	}
}
