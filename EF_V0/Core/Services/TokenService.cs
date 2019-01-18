using Castle.Core.Configuration;
using EF_V0.Core.Helpers;
using EF_V0.Core.Models;
using EF_V0.Core.Models.Responses;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace EF_V0.Core.Services
{
	public class TokenService
	{
		public IConfiguration Configuration { get; }
		private 
		TokenService(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public string GenerateToken(User user)
		{
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			var privateKey = new X509Certificate2("my-key.p12", "password", X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet).PrivateKey as RSACryptoServiceProvider;
			string refreshToken = GenerateRefreshToken(user.PublicId);
			AccessToken accessTokenPayload = new AccessToken(user.PublicId, user.Roles);
			string accessToken = Jose.JWT.Encode(accessTokenPayload, rsa, Jose.JwsAlgorithm.RS256);
			return "This is token!";
		}

		public bool ValidateToken(string token)
		{
			return true;
		}

		private string GenerateRefreshToken(string userPublicId)
		{
			return StringHelper.StringToHash(userPublicId + StringHelper.GenerateRandom(25));
		}
	}
}
