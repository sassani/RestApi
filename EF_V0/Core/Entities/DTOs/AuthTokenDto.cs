using Newtonsoft.Json;
using System.Linq;

namespace EF_V0.Core.Entities.DTOs
{
	public class AuthTokenDto
	{
		[JsonProperty(propertyName: "accessToken")]
		public string AccessToken { get; set; }

		[JsonProperty(propertyName: "refreshToken")]
		public string RefreshToken { get; set; }

		[JsonProperty(propertyName: "tokenType")]
		public string TokenType { get; set; }

		[JsonProperty(propertyName: "pid")]
		public string UserPublicId { get; set; }

		[JsonProperty(propertyName: "roles")]
		public string[] UserRoles { get; set; }

		public AuthTokenDto(string token, string refresh, string type, User user)
		{
			AccessToken = token;
			RefreshToken = refresh;
			TokenType = type;
			UserPublicId = user.PublicId;
			UserRoles = user.Roles.ToArray();
		}

	}
}
