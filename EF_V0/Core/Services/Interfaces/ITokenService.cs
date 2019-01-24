using EF_V0.Core.Entities;
using EF_V0.Core.Entities.DTOs;

namespace EF_V0.Core.Services.Interfaces
{
	public interface ITokenService
	{
		AuthTokenDto GenerateAuthToken(User user, int userClientId, string refreshToken);
		bool ValidateToken(string TokenString);
		string GenerateRefreshToken(string userPublicId);
	}
}
