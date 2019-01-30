using EF_V0.Core.Entities;
using EF_V0.Core.Entities.DTOs;
using EF_V0.Helpers;
using EF_V0.Core.Services.Interfaces;
using EF_V0.Extensions;
using Jose;
using Microsoft.Extensions.Options;
using System.Linq;

namespace EF_V0.Core.Services
{
	public class TokenService : ITokenService
	{
		private readonly IOptions<AppSettingsModel> config;
		private readonly byte[] secretKey;
		public TokenService(IOptions<AppSettingsModel> config)
		{
			this.config = config;
			secretKey = config.Value.Token.SecretKey.Select(x => (byte)x).ToArray();
		}

		public AuthTokenDto GenerateAuthToken(User user, int userClientId, string refreshToken)
		{
			AccessTokenDto accessToken = new AccessTokenDto(user, userClientId);
			string signedAccessToken = JWT.Encode(accessToken, secretKey, JwsAlgorithm.HS256);
			return new AuthTokenDto(signedAccessToken, refreshToken, "bearer", user);
		}

		public bool ValidateToken(string accessToken)
		{
			string tokenString = accessToken.Split(' ')[1];
			var temp = JWT.Decode<AccessTokenDto>(tokenString, secretKey);
			return true;
		}

		public string GenerateRefreshToken(string userPublicId)
		{
			return userPublicId + StringHelper.GenerateRandom(37);
			//return StringHelper.StringToHash(userPublicId + StringHelper.GenerateRandom(25));
		}
	}
}
