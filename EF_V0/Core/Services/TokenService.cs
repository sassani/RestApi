using Castle.Core.Configuration;
using EF_V0.Core.Helpers;
using EF_V0.Core.Entities;
using EF_V0.Core.Entities.DTOs;
using EF_V0.Core.Services.Interfaces;
using EF_V0.Extensions;
using Jose;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace EF_V0.Core.Services
{
	public class TokenService : ITokenService
	{
		private readonly IOptions<AppSettings> config;
		private readonly byte[] secretKey;
		public TokenService(IOptions<AppSettings> config)
		{
			this.config = config;
			secretKey = config.Value.Token.SecretKey.Select(x => (byte)x).ToArray();
		}

		public AuthTokenDto GenerateAuthToken(User user)
		{
			AccessTokenDto accessToken = new AccessTokenDto(user);
			string signedAccessToken = JWT.Encode(accessToken, secretKey, JwsAlgorithm.HS256);
			string refreshToken = user.RefreshToken;
			if (user.RefreshToken.Equals(""))
			{
			refreshToken = GenerateRefreshToken(user.PublicId);
			}
			AuthTokenDto authToken = new AuthTokenDto(signedAccessToken, refreshToken, "bearer", user);

			return authToken;
		}

		public AuthTokenDto GenerateAuthToken(User user, string refreshToken)
		{
			AccessTokenDto accessToken = new AccessTokenDto(user);
			string signedAccessToken = JWT.Encode(accessToken, secretKey, JwsAlgorithm.HS256);
			AuthTokenDto authToken = new AuthTokenDto(signedAccessToken, refreshToken, "bearer", user);

			return authToken;
		}

		public bool ValidateToken(string accessToken)
		{
			string tokenString = accessToken.Split(' ')[1];
			var temp = JWT.Decode<AccessTokenDto>(tokenString,secretKey);
			return true;
		}

		private string GenerateRefreshToken(string userPublicId)
		{
			return userPublicId + StringHelper.GenerateRandom(37);
			//return StringHelper.StringToHash(userPublicId + StringHelper.GenerateRandom(25));
		}
	}
}
