using EF_V0.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace EF_V0.Core.Entities.DTOs
{
	public class AccessTokenDto
	{
		[JsonProperty(propertyName: "uid")]
		public int UserId { get; set; }

		[JsonProperty(propertyName: "aid")]
		public int UserApplicationId { get; set; }

		[JsonProperty(propertyName: "exp")]
		public long Expiration { get; set; }

		[JsonProperty(propertyName: "roles")]
		public string[] Roles { get; set; }

		public AccessTokenDto() { }

		public AccessTokenDto(User user, int userApplicationId=1)
		{
			UserId = user.Id;
			UserApplicationId = userApplicationId;
			Roles = user.Roles.ToArray();
			Expiration = DateTimeHelper.GetUnixTimestamp(DateTime.Now.AddMinutes(50));
		}
	}
}
