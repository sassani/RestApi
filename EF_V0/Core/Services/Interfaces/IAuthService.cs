using EF_V0.Core.Entities;
using EF_V0.Core.Entities.DTOs;

namespace EF_V0.Core.Services.Interfaces
{
	public interface IAuthService
	{
		bool Authenticate(LoginUserDto loginUser, ref User user);
		AuthTokenDto Login(Client client, User user);
		bool Logout(int userClientId, bool all = false);
	}
}
